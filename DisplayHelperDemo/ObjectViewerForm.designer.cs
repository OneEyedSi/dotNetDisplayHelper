namespace DisplayHelperDemo
{
	partial class ObjectViewerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.CloseButton = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.ObjectTextBox = new System.Windows.Forms.TextBox();
			this.ObjectRichTextBox = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// CloseButton
			// 
			this.CloseButton.CausesValidation = false;
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(1038, 440);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(75, 23);
			this.CloseButton.TabIndex = 6;
			this.CloseButton.Text = "&Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 13);
			this.label12.TabIndex = 0;
			this.label12.Text = "TextBox:";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// ObjectTextBox
			// 
			this.ObjectTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ObjectTextBox.Location = new System.Drawing.Point(12, 25);
			this.ObjectTextBox.Multiline = true;
			this.ObjectTextBox.Name = "ObjectTextBox";
			this.ObjectTextBox.ReadOnly = true;
			this.ObjectTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ObjectTextBox.Size = new System.Drawing.Size(539, 400);
			this.ObjectTextBox.TabIndex = 1;
			// 
			// ObjectRichTextBox
			// 
			this.ObjectRichTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ObjectRichTextBox.Location = new System.Drawing.Point(574, 25);
			this.ObjectRichTextBox.Name = "ObjectRichTextBox";
			this.ObjectRichTextBox.ReadOnly = true;
			this.ObjectRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.ObjectRichTextBox.Size = new System.Drawing.Size(539, 400);
			this.ObjectRichTextBox.TabIndex = 3;
			this.ObjectRichTextBox.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(571, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "RichTextBox:";
			// 
			// ObjectViewerForm
			// 
			this.AcceptButton = this.CloseButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CloseButton;
			this.ClientSize = new System.Drawing.Size(1134, 481);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ObjectRichTextBox);
			this.Controls.Add(this.ObjectTextBox);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.CloseButton);
			this.Name = "ObjectViewerForm";
			this.Text = "Object Details via TextBox and RichTextBox";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox ObjectRichTextBox;
		private System.Windows.Forms.TextBox ObjectTextBox;
	}
}