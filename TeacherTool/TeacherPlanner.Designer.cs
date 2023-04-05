namespace TeacherTool
{
    partial class TeacherPlanner
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
            this.Classroom = new System.Windows.Forms.SplitContainer();
            this.studentSelect = new System.Windows.Forms.FlowLayoutPanel();
            this.errorList = new System.Windows.Forms.ListView();
            this.taskList = new System.Windows.Forms.ListView();
            this.studentName = new System.Windows.Forms.Label();
            this.studentWindow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Classroom)).BeginInit();
            this.Classroom.Panel1.SuspendLayout();
            this.Classroom.Panel2.SuspendLayout();
            this.Classroom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // Classroom
            // 
            this.Classroom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Classroom.Location = new System.Drawing.Point(0, 0);
            this.Classroom.Name = "Classroom";
            // 
            // Classroom.Panel1
            // 
            this.Classroom.Panel1.Controls.Add(this.studentSelect);
            // 
            // Classroom.Panel2
            // 
            this.Classroom.Panel2.Controls.Add(this.errorList);
            this.Classroom.Panel2.Controls.Add(this.taskList);
            this.Classroom.Panel2.Controls.Add(this.studentName);
            this.Classroom.Panel2.Controls.Add(this.studentWindow);
            this.Classroom.Size = new System.Drawing.Size(800, 450);
            this.Classroom.SplitterDistance = 400;
            this.Classroom.TabIndex = 0;
            // 
            // studentSelect
            // 
            this.studentSelect.AutoScroll = true;
            this.studentSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentSelect.Location = new System.Drawing.Point(0, 0);
            this.studentSelect.Name = "studentSelect";
            this.studentSelect.Size = new System.Drawing.Size(400, 450);
            this.studentSelect.TabIndex = 0;
            // 
            // errorList
            // 
            this.errorList.HideSelection = false;
            this.errorList.Location = new System.Drawing.Point(15, 217);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(242, 221);
            this.errorList.TabIndex = 2;
            this.errorList.UseCompatibleStateImageBehavior = false;
            // 
            // taskList
            // 
            this.taskList.HideSelection = false;
            this.taskList.Location = new System.Drawing.Point(263, 217);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(121, 221);
            this.taskList.TabIndex = 0;
            this.taskList.UseCompatibleStateImageBehavior = false;
            // 
            // studentName
            // 
            this.studentName.AutoSize = true;
            this.studentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentName.Location = new System.Drawing.Point(10, 9);
            this.studentName.Name = "studentName";
            this.studentName.Size = new System.Drawing.Size(154, 25);
            this.studentName.TabIndex = 1;
            this.studentName.Text = "Student_Name";
            // 
            // studentWindow
            // 
            this.studentWindow.Location = new System.Drawing.Point(13, 37);
            this.studentWindow.Name = "studentWindow";
            this.studentWindow.Size = new System.Drawing.Size(371, 174);
            this.studentWindow.TabIndex = 0;
            this.studentWindow.TabStop = false;
            // 
            // TeacherPlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Classroom);
            this.Name = "TeacherPlanner";
            this.Load += new System.EventHandler(this.TeacherPlanner_Load);
            this.Classroom.Panel1.ResumeLayout(false);
            this.Classroom.Panel2.ResumeLayout(false);
            this.Classroom.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Classroom)).EndInit();
            this.Classroom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.studentWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer Classroom;
        private System.Windows.Forms.ListView errorList;
        private System.Windows.Forms.ListView taskList;
        private System.Windows.Forms.Label studentName;
        private System.Windows.Forms.PictureBox studentWindow;
        private System.Windows.Forms.FlowLayoutPanel studentSelect;
    }
}

