namespace ClassScheduler
{
    partial class CourseSelectionForm
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
            this.AddCourseButton = new System.Windows.Forms.Button();
            this.CourseSelectionLabel = new System.Windows.Forms.Label();
            this.RemoveCourseButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SingleCourseGroup = new System.Windows.Forms.GroupBox();
            this.CourseButton = new System.Windows.Forms.Button();
            this.MainToResultButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SingleCourseGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddCourseButton
            // 
            this.AddCourseButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.AddCourseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.AddCourseButton.ForeColor = System.Drawing.Color.White;
            this.AddCourseButton.Location = new System.Drawing.Point(74, 90);
            this.AddCourseButton.Name = "AddCourseButton";
            this.AddCourseButton.Size = new System.Drawing.Size(146, 43);
            this.AddCourseButton.TabIndex = 0;
            this.AddCourseButton.Text = "Add Course";
            this.AddCourseButton.UseVisualStyleBackColor = false;
            this.AddCourseButton.Click += new System.EventHandler(this.AddCourseButton_Click);
            // 
            // CourseSelectionLabel
            // 
            this.CourseSelectionLabel.AutoSize = true;
            this.CourseSelectionLabel.Font = new System.Drawing.Font("Minion Pro", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CourseSelectionLabel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CourseSelectionLabel.Location = new System.Drawing.Point(64, 40);
            this.CourseSelectionLabel.Name = "CourseSelectionLabel";
            this.CourseSelectionLabel.Size = new System.Drawing.Size(312, 37);
            this.CourseSelectionLabel.TabIndex = 8;
            this.CourseSelectionLabel.Text = "Please Select Your Courses...";
            this.CourseSelectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RemoveCourseButton
            // 
            this.RemoveCourseButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RemoveCourseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RemoveCourseButton.ForeColor = System.Drawing.Color.White;
            this.RemoveCourseButton.Location = new System.Drawing.Point(74, 139);
            this.RemoveCourseButton.Name = "RemoveCourseButton";
            this.RemoveCourseButton.Size = new System.Drawing.Size(146, 43);
            this.RemoveCourseButton.TabIndex = 9;
            this.RemoveCourseButton.Text = "Remove Course";
            this.RemoveCourseButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(223, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 453);
            this.panel1.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.SingleCourseGroup);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-1, -1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(722, 453);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // SingleCourseGroup
            // 
            this.SingleCourseGroup.Controls.Add(this.CourseButton);
            this.SingleCourseGroup.Location = new System.Drawing.Point(0, 0);
            this.SingleCourseGroup.Margin = new System.Windows.Forms.Padding(0);
            this.SingleCourseGroup.Name = "SingleCourseGroup";
            this.SingleCourseGroup.Size = new System.Drawing.Size(722, 122);
            this.SingleCourseGroup.TabIndex = 1;
            this.SingleCourseGroup.TabStop = false;
            // 
            // CourseButton
            // 
            this.CourseButton.BackColor = System.Drawing.Color.Red;
            this.CourseButton.FlatAppearance.BorderSize = 0;
            this.CourseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CourseButton.Font = new System.Drawing.Font("Minion Pro", 18F);
            this.CourseButton.ForeColor = System.Drawing.Color.White;
            this.CourseButton.Location = new System.Drawing.Point(0, 1);
            this.CourseButton.Margin = new System.Windows.Forms.Padding(0);
            this.CourseButton.Name = "CourseButton";
            this.CourseButton.Size = new System.Drawing.Size(726, 62);
            this.CourseButton.TabIndex = 0;
            this.CourseButton.Text = "[COURSE NAME]";
            this.CourseButton.UseVisualStyleBackColor = false;
            // 
            // MainToResultButton
            // 
            this.MainToResultButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainToResultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MainToResultButton.ForeColor = System.Drawing.Color.White;
            this.MainToResultButton.Location = new System.Drawing.Point(71, 500);
            this.MainToResultButton.Name = "MainToResultButton";
            this.MainToResultButton.Size = new System.Drawing.Size(146, 43);
            this.MainToResultButton.TabIndex = 11;
            this.MainToResultButton.Text = "View Results";
            this.MainToResultButton.UseVisualStyleBackColor = false;
            this.MainToResultButton.Click += new System.EventHandler(this.MainToResultButton_Click);
            // 
            // CourseSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.MainToResultButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RemoveCourseButton);
            this.Controls.Add(this.CourseSelectionLabel);
            this.Controls.Add(this.AddCourseButton);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "CourseSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Course Selection";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CourseSelectionForm_FormClosed);
            this.Load += new System.EventHandler(this.CourseSelectionForm_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.SingleCourseGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddCourseButton;
        private System.Windows.Forms.Label CourseSelectionLabel;
        private System.Windows.Forms.Button RemoveCourseButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button CourseButton;
        private System.Windows.Forms.GroupBox SingleCourseGroup;
        private System.Windows.Forms.Button MainToResultButton;
    }
}