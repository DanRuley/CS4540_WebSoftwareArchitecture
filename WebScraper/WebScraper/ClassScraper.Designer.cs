namespace WebScraper
{
    partial class ClassScraper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(Driver != null)
                Driver.Quit();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassScraper));
            this.Query = new System.Windows.Forms.Button();
            this.CourseList = new System.Windows.Forms.ListBox();
            this.YearInput = new System.Windows.Forms.TextBox();
            this.SemesterInput = new System.Windows.Forms.TextBox();
            this.YearLabel = new System.Windows.Forms.Label();
            this.SemesterLabel = new System.Windows.Forms.Label();
            this.YearError = new System.Windows.Forms.Label();
            this.SemesterError = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.ConnectedError = new System.Windows.Forms.Label();
            this.DetailsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CourseNumber = new System.Windows.Forms.TextBox();
            this.Department = new System.Windows.Forms.TextBox();
            this.DescrGrp = new System.Windows.Forms.GroupBox();
            this.DetailsDisplay = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MaxInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.WelcomeGrp = new System.Windows.Forms.GroupBox();
            this.ShowAllButton = new System.Windows.Forms.Button();
            this.ShowAllLabel = new System.Windows.Forms.Label();
            this.DescrGrp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.WelcomeGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(460, 40);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(123, 32);
            this.Query.TabIndex = 3;
            this.Query.Text = "Search for Courses";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_Click);
            // 
            // CourseList
            // 
            this.CourseList.BackColor = System.Drawing.SystemColors.Control;
            this.CourseList.FormattingEnabled = true;
            this.CourseList.ItemHeight = 15;
            this.CourseList.Location = new System.Drawing.Point(24, 87);
            this.CourseList.Name = "CourseList";
            this.CourseList.Size = new System.Drawing.Size(560, 394);
            this.CourseList.TabIndex = 6;
            this.CourseList.TabStop = false;
            // 
            // YearInput
            // 
            this.YearInput.Location = new System.Drawing.Point(155, 46);
            this.YearInput.Name = "YearInput";
            this.YearInput.Size = new System.Drawing.Size(80, 23);
            this.YearInput.TabIndex = 2;
            // 
            // SemesterInput
            // 
            this.SemesterInput.Location = new System.Drawing.Point(45, 46);
            this.SemesterInput.Name = "SemesterInput";
            this.SemesterInput.Size = new System.Drawing.Size(79, 23);
            this.SemesterInput.TabIndex = 1;
            // 
            // YearLabel
            // 
            this.YearLabel.AutoSize = true;
            this.YearLabel.Location = new System.Drawing.Point(154, 27);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(32, 15);
            this.YearLabel.TabIndex = 5;
            this.YearLabel.Text = "Year:";
            // 
            // SemesterLabel
            // 
            this.SemesterLabel.AutoSize = true;
            this.SemesterLabel.Location = new System.Drawing.Point(43, 27);
            this.SemesterLabel.Name = "SemesterLabel";
            this.SemesterLabel.Size = new System.Drawing.Size(58, 15);
            this.SemesterLabel.TabIndex = 6;
            this.SemesterLabel.Text = "Semester:";
            // 
            // YearError
            // 
            this.YearError.AutoSize = true;
            this.YearError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.YearError.Location = new System.Drawing.Point(155, 70);
            this.YearError.Name = "YearError";
            this.YearError.Size = new System.Drawing.Size(0, 15);
            this.YearError.TabIndex = 7;
            // 
            // SemesterError
            // 
            this.SemesterError.AutoSize = true;
            this.SemesterError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SemesterError.Location = new System.Drawing.Point(41, 71);
            this.SemesterError.Name = "SemesterError";
            this.SemesterError.Size = new System.Drawing.Size(0, 15);
            this.SemesterError.TabIndex = 8;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(509, 487);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(76, 26);
            this.Save.TabIndex = 4;
            this.Save.Text = "SaveData";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Visible = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.StartButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.StartButton.Location = new System.Drawing.Point(115, 27);
            this.StartButton.Margin = new System.Windows.Forms.Padding(0);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(90, 45);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start Selenium";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ConnectedError
            // 
            this.ConnectedError.AutoSize = true;
            this.ConnectedError.ForeColor = System.Drawing.Color.DarkRed;
            this.ConnectedError.Location = new System.Drawing.Point(109, 9);
            this.ConnectedError.Name = "ConnectedError";
            this.ConnectedError.Size = new System.Drawing.Size(0, 15);
            this.ConnectedError.TabIndex = 11;
            // 
            // DetailsButton
            // 
            this.DetailsButton.Location = new System.Drawing.Point(149, 267);
            this.DetailsButton.Name = "DetailsButton";
            this.DetailsButton.Size = new System.Drawing.Size(85, 26);
            this.DetailsButton.TabIndex = 19;
            this.DetailsButton.Text = "Get Details";
            this.DetailsButton.UseVisualStyleBackColor = true;
            this.DetailsButton.Click += new System.EventHandler(this.DetailsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Department";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Course Number";
            // 
            // CourseNumber
            // 
            this.CourseNumber.Location = new System.Drawing.Point(143, 47);
            this.CourseNumber.Name = "CourseNumber";
            this.CourseNumber.Size = new System.Drawing.Size(66, 23);
            this.CourseNumber.TabIndex = 18;
            // 
            // Department
            // 
            this.Department.Location = new System.Drawing.Point(46, 47);
            this.Department.Name = "Department";
            this.Department.Size = new System.Drawing.Size(66, 23);
            this.Department.TabIndex = 17;
            // 
            // DescrGrp
            // 
            this.DescrGrp.BackColor = System.Drawing.SystemColors.Control;
            this.DescrGrp.Controls.Add(this.DetailsDisplay);
            this.DescrGrp.Controls.Add(this.CourseNumber);
            this.DescrGrp.Controls.Add(this.DetailsButton);
            this.DescrGrp.Controls.Add(this.Department);
            this.DescrGrp.Controls.Add(this.label2);
            this.DescrGrp.Controls.Add(this.label1);
            this.DescrGrp.Location = new System.Drawing.Point(28, 246);
            this.DescrGrp.Name = "DescrGrp";
            this.DescrGrp.Size = new System.Drawing.Size(265, 320);
            this.DescrGrp.TabIndex = 18;
            this.DescrGrp.TabStop = false;
            this.DescrGrp.Text = "Get Individual Course Description";
            // 
            // DetailsDisplay
            // 
            this.DetailsDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.DetailsDisplay.Location = new System.Drawing.Point(19, 85);
            this.DetailsDisplay.Name = "DetailsDisplay";
            this.DetailsDisplay.Size = new System.Drawing.Size(215, 175);
            this.DetailsDisplay.TabIndex = 18;
            this.DetailsDisplay.TabStop = false;
            this.DetailsDisplay.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.ShowAllLabel);
            this.groupBox2.Controls.Add(this.ShowAllButton);
            this.groupBox2.Controls.Add(this.MaxInput);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Query);
            this.groupBox2.Controls.Add(this.YearInput);
            this.groupBox2.Controls.Add(this.SemesterInput);
            this.groupBox2.Controls.Add(this.YearLabel);
            this.groupBox2.Controls.Add(this.Save);
            this.groupBox2.Controls.Add(this.SemesterLabel);
            this.groupBox2.Controls.Add(this.CourseList);
            this.groupBox2.Controls.Add(this.SemesterError);
            this.groupBox2.Controls.Add(this.YearError);
            this.groupBox2.Location = new System.Drawing.Point(348, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 541);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Get Semester Course Enrollments";
            // 
            // MaxInput
            // 
            this.MaxInput.Location = new System.Drawing.Point(390, 46);
            this.MaxInput.Name = "MaxInput";
            this.MaxInput.Size = new System.Drawing.Size(54, 23);
            this.MaxInput.TabIndex = 10;
            this.MaxInput.TabStop = false;
            this.MaxInput.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Max. Courses:";
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.WelcomeLabel.Location = new System.Drawing.Point(24, 81);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(275, 105);
            this.WelcomeLabel.TabIndex = 20;
            this.WelcomeLabel.Text = resources.GetString("WelcomeLabel.Text");
            // 
            // WelcomeGrp
            // 
            this.WelcomeGrp.BackColor = System.Drawing.SystemColors.Control;
            this.WelcomeGrp.Controls.Add(this.ConnectedError);
            this.WelcomeGrp.Controls.Add(this.WelcomeLabel);
            this.WelcomeGrp.Controls.Add(this.StartButton);
            this.WelcomeGrp.Location = new System.Drawing.Point(12, 25);
            this.WelcomeGrp.Name = "WelcomeGrp";
            this.WelcomeGrp.Size = new System.Drawing.Size(321, 199);
            this.WelcomeGrp.TabIndex = 21;
            this.WelcomeGrp.TabStop = false;
            this.WelcomeGrp.Text = "Welcome";
            // 
            // ShowAllButton
            // 
            this.ShowAllButton.Location = new System.Drawing.Point(24, 487);
            this.ShowAllButton.Name = "ShowAllButton";
            this.ShowAllButton.Size = new System.Drawing.Size(75, 26);
            this.ShowAllButton.TabIndex = 5;
            this.ShowAllButton.Text = "Show All";
            this.ShowAllButton.UseVisualStyleBackColor = true;
            this.ShowAllButton.Visible = false;
            this.ShowAllButton.Click += new System.EventHandler(this.ShowAllButton_Click);
            // 
            // ShowAllLabel
            // 
            this.ShowAllLabel.AutoSize = true;
            this.ShowAllLabel.Location = new System.Drawing.Point(24, 516);
            this.ShowAllLabel.Name = "ShowAllLabel";
            this.ShowAllLabel.Size = new System.Drawing.Size(215, 15);
            this.ShowAllLabel.TabIndex = 11;
            this.ShowAllLabel.Text = "Click to show all courses scraped so far.";
            this.ShowAllLabel.Visible = false;
            // 
            // ClassScraper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(984, 590);
            this.Controls.Add(this.WelcomeGrp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DescrGrp);
            this.MaximumSize = new System.Drawing.Size(1000, 629);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ClassScraper";
            this.Text = "ClassScraper";
            this.DescrGrp.ResumeLayout(false);
            this.DescrGrp.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.WelcomeGrp.ResumeLayout(false);
            this.WelcomeGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Query;
        private System.Windows.Forms.ListBox CourseList;
        private System.Windows.Forms.TextBox YearInput;
        private System.Windows.Forms.TextBox SemesterInput;
        private System.Windows.Forms.Label YearLabel;
        private System.Windows.Forms.Label SemesterLabel;
        private System.Windows.Forms.Label YearError;
        private System.Windows.Forms.Label SemesterError;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label ConnectedError;
        private System.Windows.Forms.Button DetailsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CourseNumber;
        private System.Windows.Forms.TextBox Department;
        private System.Windows.Forms.GroupBox DescrGrp;
        private System.Windows.Forms.RichTextBox DetailsDisplay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.GroupBox WelcomeGrp;
        private System.Windows.Forms.TextBox MaxInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ShowAllButton;
        private System.Windows.Forms.Label ShowAllLabel;
    }
}

