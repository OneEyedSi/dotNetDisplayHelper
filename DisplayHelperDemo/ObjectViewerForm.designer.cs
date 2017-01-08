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
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mainPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.labelTextBox = new System.Windows.Forms.Label();
            this.labelRichTextBox = new System.Windows.Forms.Label();
            this.ObjectTextBox = new System.Windows.Forms.TextBox();
            this.ObjectRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.rightPanel);
            this.mainPanel.Controls.Add(this.leftPanel);
            this.mainPanel.Location = new System.Drawing.Point(-2, 2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1139, 416);
            this.mainPanel.TabIndex = 7;
            this.mainPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.mainPanel_Layout);
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.ObjectTextBox);
            this.leftPanel.Controls.Add(this.labelTextBox);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(569, 416);
            this.leftPanel.TabIndex = 0;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.ObjectRichTextBox);
            this.rightPanel.Controls.Add(this.labelRichTextBox);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(570, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(569, 416);
            this.rightPanel.TabIndex = 1;
            // 
            // labelTextBox
            // 
            this.labelTextBox.AutoSize = true;
            this.labelTextBox.Location = new System.Drawing.Point(3, 7);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(49, 13);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "TextBox:";
            // 
            // labelRichTextBox
            // 
            this.labelRichTextBox.AutoSize = true;
            this.labelRichTextBox.Location = new System.Drawing.Point(3, 7);
            this.labelRichTextBox.Name = "labelRichTextBox";
            this.labelRichTextBox.Size = new System.Drawing.Size(71, 13);
            this.labelRichTextBox.TabIndex = 3;
            this.labelRichTextBox.Text = "RichTextBox:";
            // 
            // ObjectTextBox
            // 
            this.ObjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectTextBox.Location = new System.Drawing.Point(6, 23);
            this.ObjectTextBox.Multiline = true;
            this.ObjectTextBox.Name = "ObjectTextBox";
            this.ObjectTextBox.ReadOnly = true;
            this.ObjectTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ObjectTextBox.Size = new System.Drawing.Size(546, 390);
            this.ObjectTextBox.TabIndex = 2;
            // 
            // ObjectRichTextBox
            // 
            this.ObjectRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectRichTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectRichTextBox.Location = new System.Drawing.Point(6, 23);
            this.ObjectRichTextBox.Name = "ObjectRichTextBox";
            this.ObjectRichTextBox.ReadOnly = true;
            this.ObjectRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ObjectRichTextBox.Size = new System.Drawing.Size(546, 390);
            this.ObjectRichTextBox.TabIndex = 4;
            this.ObjectRichTextBox.Text = "";
            // 
            // ObjectViewerForm2
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(1134, 481);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.CloseButton);
            this.Name = "ObjectViewerForm2";
            this.Text = "Object Details via TextBox and RichTextBox";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label labelTextBox;
        private System.Windows.Forms.Label labelRichTextBox;
        private System.Windows.Forms.TextBox ObjectTextBox;
        private System.Windows.Forms.RichTextBox ObjectRichTextBox;
	}
}