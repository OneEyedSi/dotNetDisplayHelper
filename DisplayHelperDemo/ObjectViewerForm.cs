using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using DisplayHelper;

namespace DisplayHelperDemo
{
	/// <summary>
	/// Displays the details of an object on a TextBox and a RichTextBox.
	/// </summary>
	public partial class ObjectViewerForm : Form
	{
		#region Nested Classes, Enums, etc ********************************************************

		public enum TextToDisplay
		{
			Nothing = 0,
			Title, 
			SubTitle,
			NumberedText,
			IndentedText,
			HeadedText
		}

		#endregion

		#region Data Members **********************************************************************

		#endregion

		#region Constructors, Destructors / Finalizers and Dispose Methods ************************

		public ObjectViewerForm(object objectToView)
		{
			InitializeComponent();

			this.DisplayObject(objectToView);
		}

		public ObjectViewerForm(TextToDisplay textToDisplay)
		{
			InitializeComponent();

			this.DisplayText(textToDisplay);
		}

		public ObjectViewerForm(Exception xcp)
		{
			InitializeComponent();

			this.DisplayException(xcp);
		}

		public ObjectViewerForm(DataTable table)
		{
			InitializeComponent();

			this.DisplayDataTable(table);
		}

		#endregion

		#region Public Methods ********************************************************************

		#endregion

		#region Event Handlers ********************************************************************

		private void CloseButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Private and Protected Methods *****************************************************

        private void ClearResultControls()
		{
			this.ObjectTextBox.Clear();
			this.ObjectRichTextBox.Clear();
		}

		private void DisplayObject(object objectToView)
		{
			string title;
			bool objectIsEnumerable = !(objectToView is string) && objectToView is IEnumerable;
			if (objectIsEnumerable)
			{
				title = "Enumerable objects to display:";
				TextBoxDisplayHelper.ShowObject(this.ObjectTextBox, objectToView, 0, title);
				RichTextBoxDisplayHelper.ShowObject(this.ObjectRichTextBox, objectToView, 0, title);
			}
			else
			{
				Type objectType = objectToView.GetType();
				title = "{0} object to display:";
				TextBoxDisplayHelper.ShowObject(this.ObjectTextBox, objectToView, 0, title, objectType);
				RichTextBoxDisplayHelper.ShowObject(this.ObjectRichTextBox, objectToView, 0, 
					title, objectType);
			}

			// By default, TextBox stays scrolled at start of text while RichTextBox scrolls to 
			//	end of text (due to the way the text in the RichTextBox is formatted in the 
			//	RichTextBoxObjectViewer).  Ensure they both end up scrolled to start.
			this.ObjectTextBox.Select(0, 0);
			this.ObjectRichTextBox.Select(0, 0);
			this.ObjectRichTextBox.ScrollToCaret();
			this.CloseButton.Focus();
		}

		private void DisplayText(TextToDisplay textToDisplay)
		{
			string mainText;
			string secondaryText;
			switch (textToDisplay)
			{
				case TextToDisplay.Nothing:
				default:
					this.ObjectTextBox.Text = "[No text to display]";
					this.ObjectRichTextBox.Text = "[No text to display]";
					break;
				case TextToDisplay.Title:
					mainText = "This is the title";
					secondaryText = "This is some body text";
					TextBoxDisplayHelper.ShowTitle(this.ObjectTextBox, mainText);
					TextBoxDisplayHelper.ShowIndentedText(this.ObjectTextBox, 0,
						 secondaryText, false, true);
					RichTextBoxDisplayHelper.ShowTitle(this.ObjectRichTextBox, mainText);
					RichTextBoxDisplayHelper.ShowIndentedText(this.ObjectRichTextBox, 0,
						 secondaryText, false, true);
					break;
				case TextToDisplay.SubTitle:
					mainText = "This is the sub-title";
					secondaryText = "This is some body text";
					TextBoxDisplayHelper.ShowSubTitle(this.ObjectTextBox, mainText);
					TextBoxDisplayHelper.ShowIndentedText(this.ObjectTextBox, 0,
						 secondaryText, false, true);
					RichTextBoxDisplayHelper.ShowSubTitle(this.ObjectRichTextBox, mainText);
					RichTextBoxDisplayHelper.ShowIndentedText(this.ObjectRichTextBox, 0,
						secondaryText, false, true);
					break;
				case TextToDisplay.NumberedText:
					mainText = "This is the numbered text";
					TextBoxDisplayHelper.ShowNumberedText(this.ObjectTextBox, 3, 2,
						mainText, false);
					RichTextBoxDisplayHelper.ShowNumberedText(this.ObjectRichTextBox, 3, 2,
						mainText, false);
					break;
				case TextToDisplay.IndentedText:
					mainText = "This is the indented text";
					TextBoxDisplayHelper.ShowIndentedText(this.ObjectTextBox, 2,
						mainText, false, true);
					RichTextBoxDisplayHelper.ShowIndentedText(this.ObjectRichTextBox, 2,
						mainText, false, true);
					break;
				case TextToDisplay.HeadedText:
					mainText = "Header: Normal text";
					TextBoxDisplayHelper.ShowHeadedText(this.ObjectTextBox, 2, mainText, false, true);
					RichTextBoxDisplayHelper.ShowHeadedText(this.ObjectRichTextBox, 2, mainText, 
						false, true);
					mainText = "Header (type: MyType): Normal text";
					TextBoxDisplayHelper.ShowHeadedText(this.ObjectTextBox, 2, mainText, false, true);
					RichTextBoxDisplayHelper.ShowHeadedText(this.ObjectRichTextBox, 2, mainText, 
						false, true);
					mainText = "All normal text";
					TextBoxDisplayHelper.ShowHeadedText(this.ObjectTextBox, 2, mainText, false, true);
					RichTextBoxDisplayHelper.ShowHeadedText(this.ObjectRichTextBox, 2, mainText, 
						false, true);
					break;
			}

			this.ObjectRichTextBox.ScrollToCaret();
			this.CloseButton.Focus();
		}

		private void DisplayException(Exception xcp)
		{
			TextBoxDisplayHelper.ShowException(this.ObjectTextBox, 1, xcp);
			RichTextBoxDisplayHelper.ShowException(this.ObjectRichTextBox, 1, xcp);

			this.ObjectRichTextBox.ScrollToCaret();
			this.CloseButton.Focus();
		}

		private void DisplayDataTable(DataTable table)
		{
			TextBoxDisplayHelper.ShowDataTable(this.ObjectTextBox, table, true);
			RichTextBoxDisplayHelper.ShowDataTable(this.ObjectRichTextBox, table, true);

			this.ObjectRichTextBox.ScrollToCaret();
			this.CloseButton.Focus();
		}

		#endregion

        private void mainPanel_Layout(object sender, LayoutEventArgs e)
        {
            int halfWidth = mainPanel.Width / 2;
            leftPanel.Width = halfWidth;
            rightPanel.Width = halfWidth;
        }
	}
}
