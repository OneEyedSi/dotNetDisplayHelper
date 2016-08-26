using System;
using System.Collections.Generic;
using System.Data;
using DisplayHelper;
using MenuLibrary;

namespace DisplayHelperDemo
{
	/// <summary>
	/// Contains the methods for displaying objects via the Console and a form.
	/// </summary>
	[MenuClass("Main Menu")]
	public class MainMenu
	{
		[MenuMethod("Display the properties of a simple object", DisplayOrder = 1)]
		public static void DisplayASimpleObject()
		{
			try
			{
				MyLittleObject obj = new MyLittleObject(1, "String property");
				ConsoleDisplayHelper.ShowObject(obj, 0, "{0} to display:", obj.GetType());
				DisplayObjectViaForm(obj);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display the properties of a complex object", DisplayOrder = 2)]
		public static void DisplayAComplexObject()
		{
			try
			{
				DemoClass1 obj = new DemoClass1();
				ConsoleDisplayHelper.ShowObject(obj, 0, "{0} to display:", obj.GetType());
				DisplayObjectViaForm(obj);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display the properties of a recursive object", DisplayOrder = 3)]
		public static void DisplayRecursiveObject()
		{
			try
			{
				RecursiveObject obj = new RecursiveObject(11);
				ConsoleDisplayHelper.ShowObject(obj, 0, "{0} to display:", obj.GetType());
				DisplayObjectViaForm(obj);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display the properties of each object in a list of objects", DisplayOrder = 4)]
		public static void DisplayObjectEnumeration()
		{
			try
			{
				List<MyLittleObject> objectList =
					new List<MyLittleObject>(new MyLittleObject[] 
													{	new MyLittleObject(1, "ListObject1"), 
														new MyLittleObject(2, "ListObject2") });
				ConsoleDisplayHelper.ShowObject(objectList, 0, "{0} to display:", 
					objectList.GetType());
				DisplayObjectViaForm(objectList);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display the properties of each object in a list of values", DisplayOrder = 5)]
		public static void DisplayValueEnumeration()
		{
			try
			{
				List<int> valueList = new List<int>();
				valueList.Add(10);
				valueList.Add(20);
				ConsoleDisplayHelper.ShowObject(valueList, 0, "{0} to display:", 
					valueList.GetType());
				DisplayObjectViaForm(valueList);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display details of an exception", DisplayOrder = 6)]
		public static void DisplayException()
		{
			try
			{
				InvalidOperationException exception3 =
					new InvalidOperationException("Third level exception.");
				AccessViolationException exception2 =
					new AccessViolationException("Second level exception.", exception3);
				throw new ApplicationException("Top level exception.", exception2);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
				ObjectViewerForm viewerForm = new ObjectViewerForm(xcp);
				viewerForm.ShowDialog();
			}
		}

		[MenuMethod("Display data table", DisplayOrder = 7)]
		public static void DisplayDataTable()
		{
			try
			{
				DataTable table = new DataTable("MyTable");
				table.Columns.Add(new DataColumn("Col1", typeof(int)));
				table.Columns.Add(new DataColumn("Col2", typeof(string)));
				table.Rows.Add(new object[2] { 1, "First row" });
				table.Rows.Add(new object[2] { 2, "Second row" });
				ConsoleDisplayHelper.ShowDataTable(table, false);
				ObjectViewerForm viewerForm = new ObjectViewerForm(table);
				viewerForm.ShowDialog();
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display empty data table", DisplayOrder = 8)]
		public static void DisplayEmptyDataTable()
		{
			try
			{
				DataTable table = new DataTable("MyTable");
				table.Columns.Add(new DataColumn("Col1", typeof(int)));
				table.Columns.Add(new DataColumn("Col2", typeof(string)));
				ConsoleDisplayHelper.ShowDataTable(table, false);
				ObjectViewerForm viewerForm = new ObjectViewerForm(table);
				viewerForm.ShowDialog();
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display null data table", DisplayOrder = 9)]
		public static void DisplayNullDataTable()
		{
			try
			{
				DataTable table = null;
				ConsoleDisplayHelper.ShowDataTable(table, false);
				ObjectViewerForm viewerForm = new ObjectViewerForm(table);
				viewerForm.ShowDialog();
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display Title", DisplayOrder = 10)]
		public static void DisplayTitle()
		{
			try
			{
				string title = "This is the title";
				string secondaryText = "This is some body text";
				ConsoleDisplayHelper.ShowTitle(title);
				ConsoleDisplayHelper.ShowIndentedText(0, secondaryText, false, true);
				DisplayTextViaForm(ObjectViewerForm.TextToDisplay.Title);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display Sub-title", DisplayOrder = 11)]
		public static void DisplaySubTitle()
		{
			try
			{
				string title = "This is the sub-title";
				string secondaryText = "This is some body text";
				ConsoleDisplayHelper.ShowSubTitle(title);
				ConsoleDisplayHelper.ShowIndentedText(0, secondaryText, false, true);
				DisplayTextViaForm(ObjectViewerForm.TextToDisplay.SubTitle);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display Numbered Text", DisplayOrder = 12)]
		public static void DisplayNumberedText()
		{
			try
			{
				string text = "This is the numbered text";
				ConsoleDisplayHelper.ShowNumberedText(3, 2, text, false);
				DisplayTextViaForm(ObjectViewerForm.TextToDisplay.NumberedText);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display Indented Text", DisplayOrder = 13)]
		public static void DisplayIndentedText()
		{
			try
			{
				string text = "This is the indented text";
				ConsoleDisplayHelper.ShowIndentedText(2, text, false, true);
				DisplayTextViaForm(ObjectViewerForm.TextToDisplay.IndentedText);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		[MenuMethod("Display Headed Text", DisplayOrder = 14)]
		public static void DisplayHeadedText()
		{
			try
			{
				string mainText;
				mainText = "Header: Normal text";
				ConsoleDisplayHelper.ShowHeadedText(2, mainText, false, true);
				mainText = "Header (type: MyType): Normal text";
				ConsoleDisplayHelper.ShowHeadedText(2, mainText, false, true);
				mainText = "All normal text";
				ConsoleDisplayHelper.ShowHeadedText(2, mainText, false, true);

				DisplayTextViaForm(ObjectViewerForm.TextToDisplay.HeadedText);
			}
			catch (Exception xcp)
			{
				ConsoleDisplayHelper.ShowException(1, xcp);
			}
		}

		private static void DisplayObjectViaForm(object obj)
		{
			ObjectViewerForm viewerForm = new ObjectViewerForm(obj);
			viewerForm.ShowDialog();
		}

		private static void DisplayTextViaForm(ObjectViewerForm.TextToDisplay textToDisplay)
		{
			ObjectViewerForm viewerForm = new ObjectViewerForm(textToDisplay);
			viewerForm.ShowDialog();
		}
	}
}
