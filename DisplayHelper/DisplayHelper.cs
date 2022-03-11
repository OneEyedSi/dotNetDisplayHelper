using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DisplayHelper
{
	/// <summary>
	/// Helper methods for displaying text and details of objects.
	/// </summary>
	public abstract class DisplayHelper
	{
		#region Nested Classes and Structs ********************************************************

		/// <summary>
		/// Helper structure for displaying objects.
		/// </summary>
		public struct ObjectArguments
		{
			public object Object;
			public int RootIndentLevel;
			public string Title;
			public object[] TitleArgs;
			public string ObjectName;
			public Dictionary<Type, int> RecursionTypeCount;
            public Dictionary<Type, List<object>> ReferenceTypeObjectRegister;
            public bool SimpleDataTypesOnly;

			public ObjectArguments(object obj, int rootIndentLevel,
                Dictionary<Type, List<object>> referenceTypeObjectRegister,
                string title, params object[] titleArgs)
				: this(obj, rootIndentLevel, null, new Dictionary<Type, int>(), false,
                      referenceTypeObjectRegister, 
                      title, titleArgs)
			{ }

			public ObjectArguments(object obj, int rootIndentLevel, string objectName, 
                Dictionary<Type, int> recursionTypeCount, bool simpleTypesOnly,
                Dictionary<Type, List<object>> referenceTypeObjectRegister, 
                string title, params object[] titleArgs)
			{
				Object = obj;
				RootIndentLevel = rootIndentLevel;
				ObjectName = objectName;
				RecursionTypeCount = recursionTypeCount;
                SimpleDataTypesOnly = simpleTypesOnly;
                ReferenceTypeObjectRegister = referenceTypeObjectRegister;
                Title = title;
                TitleArgs = titleArgs;
            }
		}

		/// <summary>
		/// Structure that supports DisplayObjectMember.
		/// </summary>
		private struct MemberDetails
		{
			public string Name;
			public MemberTypes MemberType;
			public Type Type;
			public object Value;
			public Dictionary<Type, int> RecursionTypeCount;

			public MemberDetails(string name, MemberTypes memberType, Type type, object value,
				Dictionary<Type, int> recursionTypeCount)
			{
				Name = name;
				MemberType = memberType;
				Type = type;
				Value = value;
				RecursionTypeCount = recursionTypeCount;
			}
		}

		#endregion

		#region Data Members **********************************************************************

		private const string _nullDisplayText = "[NULL]";
		private const string _emptyStringDisplayText = "[EMPTY STRING]";
		private const string _blankStringDisplayText = "[BLANK STRING]";
		private const string _emptyEnumerableDisplayText = "[NO ITEMS]";
		private const string _maxRecursionDepthText = "[MAX RECURSION DEPTH REACHED]";
        private const string _simpleTypesOnlyText = "[DISPLAY OF COMPLEX TYPES DISABLED]";
        private const string _referenceObjectAlreadyDisplayedText = "[CIRCULAR REFERENCE - OBJECT PREVIOUSLY DISPLAYED]";
        private const int _maxRecursionDepth = 5;
		private const int _tabWidth = 4;

        #endregion

        #region Constructors, Destructors / Finalizers and Dispose Methods ************************

        #endregion

        #region Properties ************************************************************************

        private Dictionary<Type, List<object>> _referenceTypeObjectRegister;

        public Dictionary<Type, List<object>> ReferenceTypeObjectRegister
        {
            get 
            {
                if (_referenceTypeObjectRegister == null)
                {
                    _referenceTypeObjectRegister = new Dictionary<Type, List<object>>();
                }
                return _referenceTypeObjectRegister; 
            }
        }


        #endregion

        #region Public Methods ********************************************************************

        /// <summary>
        /// Appends the specified text to the last line of text.
        /// </summary>
        /// <param name="text"></param>
        public abstract void DisplayAppendedText(string text, bool addLeadingSpace,
			bool includeNewLine);

		/// <summary>
		/// Displays the specified text indented by the specified number of tabs.  Arguments may 
		/// be inserted into the text, as in string.Format() and Console.WriteLine().
		/// </summary>
		public abstract void DisplayIndentedText(int indentLevel, string text, bool wrapText,
			bool includeNewLine, params object[] args);

		/// <summary>
		/// Displays the specified text indented by the specified number of tabs.  Similar to 
		/// DisplayIndentedText but if the text is of the form "header: text" then the header may 
		/// be formatted differently from the remaining text.
		/// </summary>
		public virtual void DisplayHeadedText(int indentLevel, string text, bool wrapText,
			bool includeNewLine, params object[] args)
		{
			this.DisplayIndentedText(indentLevel, text, wrapText, includeNewLine, args);
		}

		/// <summary>
		/// Displays the specified text as a numbered paragraph, of the form "n) text", where n 
		/// is the paragraph number.
		/// </summary>
		public virtual void DisplayNumberedText(int number, int indentLevel, string text, 
			bool wrapText, params object[] args)
		{

			text = string.Format("{0}) {1}", number, text);
			this.DisplayIndentedText(indentLevel, text, wrapText, true, args);
		}

		/// <summary>
		/// Displays the specified text with a double underline.
		/// </summary>
		public virtual void DisplayTitle(string titleText)
		{
			this.DisplayTitle(titleText, '=');
		}

		/// <summary>
		/// Displays the specified text with a single underline.
		/// </summary>
		public virtual void DisplaySubTitle(string titleText)
		{
			this.DisplayTitle(titleText, '-');
		}

		/// <summary>
		/// Displays the specified text with the specified underline.
		/// </summary>
		public virtual void DisplayTitle(string titleText, char underlineChar)
		{
			int titleLength = titleText.Length;
			bool wrapText = false;
			bool includeNewLine = true;
			this.DisplayIndentedText(0, titleText, wrapText, includeNewLine);
			this.DisplayIndentedText(0, new string(underlineChar, titleLength), 
				wrapText, includeNewLine);
		}

		/// <summary>
		/// Converts a string into text suitable for display.  
		/// </summary>
		/// <returns>
		/// "[NULL]" if the input string is null; 
		/// "[EMPTY STRING]" if the input string is an empty string; 
		/// "[BLANK STRING]" if the input string contains only white space; 
		/// otherwise returns the input string unchanged.
		/// </returns>
		public static string GetDisplayText(string inputString)
		{
			if (inputString == null) return _nullDisplayText;
			if (inputString.Length == 0) return _emptyStringDisplayText;
			if (inputString.Trim().Length == 0) return _blankStringDisplayText;
			
			return inputString;
		}

		/// <summary>
		/// Returns the exception type and message, recursively including inner exceptions.
		/// </summary>
		public static string GetExceptionDetails(Exception exception)
		{
			if (exception == null)
			{
				return "EXCEPTION IS NULL";
			}

			string message = $"{exception.GetType().Name}: {exception.Message}.";

			if (exception.InnerException != null)
			{
				message += "  [Inner Exception - " + GetExceptionDetails(exception.InnerException) + "]";
			}

			return message;
		}

		#endregion

		#region Protected Methods *****************************************************************

		/// <summary>
		/// Displays the details of an object - either a single object or an enumeration of 
		/// objects.
		/// </summary>
		/// <param name="simpleDataTypesOnly">If set then only displays the values of 
		/// properties or fields which are value types or strings.  If cleared then displays the 
		/// details of all properties and fields of the object.
		/// </param>
		/// <remarks>If simpleDataTypesOnly is set then properties and fields which are reference 
		/// types will still be listed.  However, their members will not be displayed.</remarks>
		protected virtual void DisplayObject(object obj, int rootIndentLevel,
            bool simpleDataTypesOnly,
            string title, params object[] titleArgs)
		{
            Dictionary<Type, List<object>> referenceTypeObjectRegister = 
                new Dictionary<Type, List<object>>();

            if (!(obj is string) && obj is IEnumerable)
			{
				this.DisplayEnumerableObjects((IEnumerable)obj, rootIndentLevel, simpleDataTypesOnly,
                    referenceTypeObjectRegister, title, titleArgs);
				return;
			}
			this.DisplaySingleObject(obj, rootIndentLevel, simpleDataTypesOnly,
                referenceTypeObjectRegister, title, titleArgs);
		}

        /// <summary>
        /// Displays the details of each value or object in an Enumerable collection.
        /// </summary>
        /// <param name="simpleDataTypesOnly">If set then only displays the values of 
        /// properties or fields which are value types or strings.  If cleared then displays the 
        /// details of all properties and fields of an object in the enumeration.
        /// </param>
        /// <remarks>If simpleDataTypesOnly is set then properties and fields which are reference 
        /// types will still be listed.  However, their members will not be displayed.</remarks>
        protected virtual void DisplayEnumerableObjects(IEnumerable enumerableObjects,
			int rootIndentLevel, bool simpleDataTypesOnly,
            Dictionary<Type, List<object>> referenceTypeObjectRegister,
            string title, params object[] titleArgs)
		{
			int indentLevel = rootIndentLevel;

			bool listIsNull = (enumerableObjects == null);

			if (title != null && title.Trim().Length > 0)
			{
				// Do not write newline if list is null - will append to line.
				bool includeNewLine = !listIsNull;
				this.DisplayHeadedText(indentLevel, title, false, includeNewLine,
					titleArgs);
				indentLevel++;
			}

			if (listIsNull)
			{
				// Only add leading space if a title was written.
				bool addLeadingSpace = (indentLevel > rootIndentLevel);
				this.DisplayAppendedText(_nullDisplayText, addLeadingSpace, true);
				return;
			}

			bool noObjects = true;
			foreach (object obj in enumerableObjects)
			{
				noObjects = false;
				break;
			}
			if (noObjects)
			{
				this.DisplayAppendedText(_emptyEnumerableDisplayText, true, true);
				return;
			}

			int i = 0;
			foreach (object obj in enumerableObjects)
			{
				string itemTitle = string.Format("{0}[{1}]:", obj.GetType().Name, i);
				if (!this.ObjectIsReferenceType(obj))
				{
					this.DisplayHeadedText(indentLevel, itemTitle + " " + obj.ToString(),
						false, true);
				}
				else
				{
					this.DisplaySingleObject(obj, indentLevel, simpleDataTypesOnly,
                        referenceTypeObjectRegister, 
                        "{0}[{1}]:", obj.GetType().Name, i);
				}
				i++;
			}
		}

		/// <summary>
		/// Displays the details of a single object.
		/// </summary>
		protected virtual void DisplaySingleObject(object obj, int rootIndentLevel,
            bool simpleDataTypesOnly,
            Dictionary<Type, List<object>> referenceTypeObjectRegister, 
            string title, params object[] titleArgs)
		{
            DisplayHelper.ObjectArguments objectArguments =
				new DisplayHelper.ObjectArguments(obj, rootIndentLevel, null,
                new Dictionary<Type, int>(), simpleDataTypesOnly,
                referenceTypeObjectRegister, title, titleArgs);
			this.DisplaySingleObject(objectArguments);
        }

        /// <summary>
        /// Displays text formatted as XML.
        /// </summary>
        /// <remarks>This would be trivial in .NET 3.5 or later, where we could use 
        /// System.Xml.Linq.XDocument.Parse(xmlText).ToString().  If we're sticking to .NET 2.0 
        /// then it's messier.</remarks>
        protected void DisplayXmlText(string xmlText, int rootIndentLevel,
            string title, params object[] titleArgs)
        {
            int indentLevel = rootIndentLevel;
            bool wrapText = true;
            bool includeNewLine = true;

            if (title != null && title.Trim().Length > 0)
            {
                this.DisplayHeadedText(indentLevel, title, wrapText, includeNewLine,
                    titleArgs);
                indentLevel++;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlText);
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            try
            {
                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    xmlDoc.Save(writer);
                }

                // Can't handle text wrapping if just writing the whole string builder string 
                //  out in one go.  So write each line separately.  Much slower but this is likely 
                //  to only be used in development, not production code.
                using (StringReader reader = new StringReader(sb.ToString()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        this.DisplayIndentedText(indentLevel, line, wrapText, includeNewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayIndentedText(indentLevel,
                    "Exception occurred while trying to display XML text:", wrapText,
                    includeNewLine);
                this.DisplayException(indentLevel + 1, ex);

                // Since the problem occurred while trying to display the XML string as formatted 
                //  text display the unformatted text so the user has some output.
                this.DisplayIndentedText(indentLevel, xmlText, wrapText, includeNewLine);
            }
        }

        /// <summary>
        /// Displays text formatted as JSON.
        /// </summary>
        protected void DisplayJsonText(string jsonText, int rootIndentLevel,
            string title, params object[] titleArgs)
        {
            int indentLevel = rootIndentLevel;
            bool wrapText = true;
            bool includeNewLine = true;

            if (title != null && title.Trim().Length > 0)
            {
                this.DisplayHeadedText(indentLevel, title, wrapText, includeNewLine,
                    titleArgs);
                indentLevel++;
            }

            try
            {
                string formattedIndentedText = new JsonFormatter.JsonFormatter(jsonText).Format();
                // Line breaks already added, don't add more.
                wrapText = false;
                this.DisplayIndentedText(0, formattedIndentedText, wrapText, includeNewLine);
            }
            catch (Exception ex)
            {
                wrapText = true;
                this.DisplayIndentedText(indentLevel,
                    "Exception occurred while trying to display JSON text:", wrapText,
                    includeNewLine);
                this.DisplayException(indentLevel + 1, ex);

                // Since the problem occurred while trying to display the JSON string as formatted 
                //  text display the unformatted text so the user has some output.
                this.DisplayIndentedText(indentLevel, jsonText, wrapText, includeNewLine);
            }
        }

		/// <summary>
		/// Displays the values in a data table.
		/// </summary>
		protected void DisplayDataTable(DataTable dataTable, bool displayRowState)
		{
			int columnSpacing = 3;	// Spaces between each column.
			bool wrapText = false;
			bool includeNewLine = true;
			if (dataTable == null)
			{
				this.DisplayIndentedText(0, "[DATATABLE IS NULL]", wrapText, includeNewLine);
				return;
			}

			string rowStateHeaderText = "Row State";
			int numberColumns = dataTable.Columns.Count;

			// Work out maximum column widths needed for each column.
			// ASSUMPTION: Monospaced font will be used, so each character or space will 
			//	occupy the same width.
			Dictionary<int, int> columnWidths = new Dictionary<int, int>();
			for (int i = 0; i < numberColumns; i++)
			{
				columnWidths[i] = dataTable.Columns[i].ColumnName.Length;
			}
			if (displayRowState)
			{
				columnWidths[numberColumns] = rowStateHeaderText.Length;
			}
			
			foreach (DataRow row in dataTable.Rows)
			{
				for (int i = 0; i < numberColumns; i++)
				{
					if (row[i].ToString().Length > columnWidths[i])
					{
						columnWidths[i] = row[i].ToString().Length;
					}
				}
				if (displayRowState && row.RowState.ToString().Length > columnWidths[numberColumns])
				{
					columnWidths[numberColumns] = row.RowState.ToString().Length;
				}
			}

			// Display column names and row values.
			StringBuilder sb = new StringBuilder();
			string displayText;
			for (int i = 0; i < numberColumns; i++)
			{
				displayText = 
					dataTable.Columns[i].ColumnName.PadRight(columnWidths[i] + columnSpacing);
				sb.Append(displayText);
			}
			if (displayRowState)
			{
				displayText = 
					rowStateHeaderText.PadRight(columnWidths[numberColumns] + columnSpacing);
				sb.Append(displayText);
			}
			string headerText = sb.ToString();
			this.DisplaySubTitle(headerText);

			if (dataTable.Rows.Count <= 0)
			{
				this.DisplayIndentedText(0, "[NO ROWS IN DATATABLE]", wrapText, includeNewLine);
				return;
			}

			foreach (DataRow row in dataTable.Rows)
			{
				sb = new StringBuilder();
				for (int i = 0; i < numberColumns; i++)
				{
					displayText =
						row[i].ToString().PadRight(columnWidths[i] + columnSpacing);
					sb.Append(displayText);
				}
				if (displayRowState)
				{
					displayText =
						row.RowState.ToString().PadRight(columnWidths[numberColumns] + columnSpacing);
					sb.Append(displayText);
				}
				this.DisplayIndentedText(0, sb.ToString().TrimEnd(), wrapText, includeNewLine);
			}		
		}

		/// <summary>
		/// Displays the details of an exception.
		/// </summary>
		protected void DisplayException(int indentLevel, Exception exception)
		{
			this.DisplayException(indentLevel, exception, false);
		}

		#endregion

		#region Private Methods *******************************************************************

		/// <summary>
		/// Displays the details of a single object.
		/// </summary>
		private void DisplaySingleObject(ObjectArguments objectArgs)
		{
			object obj = objectArgs.Object;
			int rootIndentLevel = objectArgs.RootIndentLevel;
			string objectName = objectArgs.ObjectName;
			Dictionary<Type, int> recursionTypeCount = objectArgs.RecursionTypeCount;
            Dictionary<Type, List<object>> referenceTypeObjectRegister = 
                objectArgs.ReferenceTypeObjectRegister;
            bool simpleDataTypesOnly = objectArgs.SimpleDataTypesOnly;
            string title = objectArgs.Title;
            object[] titleArgs = objectArgs.TitleArgs;

			int indentLevel = rootIndentLevel;

			bool objectIsNull = (obj == null);
            
			if (title != null && title.Trim().Length > 0)
			{
                // Do not write newline if object is null - will append to line.
                bool includeNewLine = !objectIsNull;
                this.DisplayHeadedText(indentLevel, title, false, includeNewLine,
					titleArgs);
				indentLevel++;
			}

			if (objectIsNull)
			{
				// Only add leading space if a title was written.
				bool addLeadingSpace = (indentLevel > rootIndentLevel);
                this.DisplayAppendedText(_nullDisplayText, addLeadingSpace, true);
				return;
			}

            if (this.ObjectIsReferenceType(obj))
            {
                if (!this.ObjectHasPreviouslyBeenDisplayed(referenceTypeObjectRegister, obj))
                {
                    this.AddObjectToRegister(referenceTypeObjectRegister, obj);
                }
            }

			List<MemberDetails> memberDetails = new List<MemberDetails>();
			PropertyInfo[] properties = obj.GetType().GetProperties();
			foreach (PropertyInfo property in properties)
			{
                if (!property.GetGetMethod().IsStatic)
                {
					object value = null;
					try
					{
						value = property.GetValue(obj, null);
					}
					catch (Exception ex)
					{
						value = string.Format("[UNABLE TO READ VALUE] {0}: {1}", ex.GetType().Name, ex.Message);
					}

					memberDetails.Add(new MemberDetails(property.Name, property.MemberType,
							property.PropertyType, value,
							new Dictionary<Type, int>(recursionTypeCount)));
				}
			}

			FieldInfo[] fields = obj.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
                if (!field.IsStatic)
			    {
					object value = null;
					try
					{
						value = field.GetValue(obj);
					}
					catch (Exception ex)
					{
						value = string.Format("[UNABLE TO READ VALUE] {0}: {1}", ex.GetType().Name, ex.Message);
					}

					memberDetails.Add(new MemberDetails(field.Name, field.MemberType,
						field.FieldType, value,
						new Dictionary<Type, int>(recursionTypeCount)));
				}
			}

			for (int i = 0; i < memberDetails.Count; i++)
			{
				MemberDetails member = memberDetails[i];
				DisplayObjectMember(member, obj, indentLevel, simpleDataTypesOnly,
                    referenceTypeObjectRegister);
			}
		}

		/// <summary>
		/// Displays the details of a property or field of an object.
		/// </summary>
		/// <remarks>Helper method for DisplaySingleObject.</remarks>
		private void DisplayObjectMember(MemberDetails memberDetails, object parentObj,
			int rootIndentLevel, bool simpleDataTypesOnly,
            Dictionary<Type, List<object>> referenceTypeObjectRegister)
		{
			int indentLevel = rootIndentLevel;

			string memberName = memberDetails.Name;
			object memberValue = memberDetails.Value;
			Type memberType = memberDetails.Type;
			bool memberValueIsNull = (memberValue == null);
			bool memberIsEnumerable = !(memberValue is string) && memberValue is IEnumerable;
			bool memberIsEmptyEnumerable = false;
            bool memberIsReferenceType = this.TypeIsReferenceType(memberType);
            if (!memberValueIsNull && memberIsEnumerable)
			{
				memberIsEmptyEnumerable = true;
				IEnumerable enumerable = memberValue as IEnumerable;
				foreach (object item in enumerable)
				{
					memberIsEmptyEnumerable = false;
					break;
				}
			}
			Dictionary<Type, int> recursionTypeCount = memberDetails.RecursionTypeCount;
			if (recursionTypeCount.ContainsKey(memberType))
			{
				recursionTypeCount[memberType]++;
			}
			else
			{
				recursionTypeCount[memberType] = 1;
			}
			bool maxRecursionDepthReached = (recursionTypeCount[memberType] >= _maxRecursionDepth);

			string displayValue = (memberValue == null) ? _nullDisplayText : memberValue.ToString();
			string displayMemberType =
				(memberDetails.MemberType == MemberTypes.Field) ? " (field)" : string.Empty;

            // Value tyeps and strings - just write value then stop because the member can't be 
            //  an object with its own members.
			if (!memberIsReferenceType)
			{
				this.DisplayHeadedText(indentLevel, "{0}{1}: {2}", false, true,
					memberName, displayMemberType, displayValue);
				return;
			}
            
            bool memberObjectAlreadyDisplayed = false;

            object previousReferenceToObject = null;

            List<object> previouslyDisplayedObjectsOfSameType = null;
            if (referenceTypeObjectRegister.TryGetValue(memberType,
                out previouslyDisplayedObjectsOfSameType))
            {
                previousReferenceToObject =
                    previouslyDisplayedObjectsOfSameType.Find(item => (item == memberValue));
            }
            else
            {
                previouslyDisplayedObjectsOfSameType = new List<object>();
                referenceTypeObjectRegister.Add(memberType, previouslyDisplayedObjectsOfSameType);
            }

            if (previousReferenceToObject != null)
            {
                memberObjectAlreadyDisplayed = true;
            }
            else
            {
                previouslyDisplayedObjectsOfSameType.Add(memberValue);
            }

            // Do not write newline in the following cicumstances (will want to add further text 
            //  to the same line): 
            //      Member value is null; or
            //      Member is an empty collection; or 
            //      We don't want to display the details of complex members; or
            //      Member object has already been displayed because of circular references; or
            //      Maximum recursion depth has been reached.
            bool includeNewLine = (!memberValueIsNull && !memberIsEmptyEnumerable 
                                && !simpleDataTypesOnly && !memberObjectAlreadyDisplayed 
                                && !maxRecursionDepthReached);

			string displayFormat;
			if (displayMemberType.ToLower().Contains("field"))
			{
				displayFormat = "{0} (field, type: {1}):";
			}
			else
			{
				displayFormat = "{0} (type: {1}):";
			}

			this.DisplayHeadedText(indentLevel, displayFormat, false, includeNewLine,
				memberName, memberType.FullName);

			indentLevel++;

            if (memberValueIsNull)
            {
                this.DisplayAppendedText(_nullDisplayText, true, true);
                return;
            }

            if (memberIsEmptyEnumerable)
            {
                this.DisplayAppendedText(_emptyEnumerableDisplayText, true, true);
                return;
            }

            // We would have exited above if the member was a value type or string so the member 
            //  must be a complex type.
            if (simpleDataTypesOnly)
            {
                this.DisplayAppendedText(_simpleTypesOnlyText, true, true);
                return;
            }

            if (memberObjectAlreadyDisplayed)
            {
                this.DisplayAppendedText(_referenceObjectAlreadyDisplayedText, true, true);
                return;
            }

            if (maxRecursionDepthReached)
            {
                this.DisplayAppendedText(_maxRecursionDepthText, true, true);
                return;
            }

            if (memberIsEnumerable)
			{
				int i = 0;
				IEnumerable enumerable = memberValue as IEnumerable;
				foreach (object item in enumerable)
				{
					if (!this.ObjectIsReferenceType(item))
					{
						this.DisplayHeadedText(indentLevel, "{0}[{1}]: {2}", false, true,
							memberName, i, item.ToString());
					}
					else
					{
						this.DisplayHeadedText(indentLevel, "{0}[{1}] (type: {2}):", false, true,
							memberName, i, item.GetType().FullName);

						ObjectArguments itemObjectArguments =
							new ObjectArguments(item, indentLevel + 1, referenceTypeObjectRegister, null);
                        itemObjectArguments.SimpleDataTypesOnly = simpleDataTypesOnly;
                        DisplaySingleObject(itemObjectArguments);
					}
					i++;
				}

				return;
			}

            ObjectArguments objectArguments =
				new ObjectArguments(memberValue, indentLevel, null, recursionTypeCount, 
                simpleDataTypesOnly, referenceTypeObjectRegister, memberName);
			DisplaySingleObject(objectArguments);
		}

		/// <summary>
		/// Displays the details of an exception.
		/// </summary>
		private void DisplayException(int indentLevel, Exception exception, bool isInnerException)
		{
			string textToDisplay = string.Empty;
			if (isInnerException)
			{
				textToDisplay += "Inner Exception - ";
			}
			textToDisplay += string.Format("{0}: {1}", exception.GetType().Name, exception.Message);

			this.DisplayHeadedText(indentLevel, textToDisplay, true, true);

			if (exception.InnerException != null)
			{
				this.DisplayException((indentLevel + 1), exception.InnerException, true);
			}
		}

        /// <summary>
        /// Indicates whether the object is a reference type or not.
        /// </summary>
        /// <remarks>For the purpose of this DisplayHelper a string is treated as a value type.</remarks>
        private bool ObjectIsReferenceType(object obj)
        {
            if (obj == null)
            {
                // Could be nullable type or string; can't say definitely it's a reference type.
                return false; 
            }

            return TypeIsReferenceType(obj.GetType());
        }

        /// <summary>
        /// Indicates whether the Type is a reference type or not.
        /// </summary>
        private bool TypeIsReferenceType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return (!type.IsValueType && (type != typeof(string)));
        }

        private bool ObjectHasPreviouslyBeenDisplayed(Dictionary<Type, List<object>> referenceTypeObjectRegister, 
            object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (referenceTypeObjectRegister == null)
            {
                return false;
            }

            Type objectType = obj.GetType();
            object previousReferenceToObject = null;
            List<object> previouslyDisplayedObjectsOfSameType = null;

            // Have any objects of the same type previously been displayed?
            if (referenceTypeObjectRegister.TryGetValue(objectType,
                out previouslyDisplayedObjectsOfSameType))
            {
                previousReferenceToObject =
                    previouslyDisplayedObjectsOfSameType.Find(item => (item == obj));

                return (previousReferenceToObject != null);
            }

            return false;
        }

        private void AddObjectToRegister(Dictionary<Type, List<object>> referenceTypeObjectRegister,
            object obj)
        {
            if (obj == null)
            {
                return;
            }
            if (referenceTypeObjectRegister == null)
            {
                return;
            }

            Type objectType = obj.GetType();
            List<object> previouslyDisplayedObjectsOfSameType = null;

            // Have any objects of the same type previously been displayed?
            if (referenceTypeObjectRegister.TryGetValue(objectType,
                out previouslyDisplayedObjectsOfSameType))
            {
                previouslyDisplayedObjectsOfSameType.Add(obj);
            }
            else
            {
                List<object> objectsOfSameType = new List<object>();
                objectsOfSameType.Add(obj);
                referenceTypeObjectRegister.Add(objectType, objectsOfSameType);
            }
        }

        #endregion
    }
}
