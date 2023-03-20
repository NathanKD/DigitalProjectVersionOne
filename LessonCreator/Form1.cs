using DTPStudentApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LessonCreator
{
    public partial class LessonPlanner : Form
    {
        private List<Lesson> lessons = new List<Lesson>();
        private LessonButton selected;
        public LessonPlanner()
        {
            InitializeComponent();
        }

        private void newLesson_Click(object sender, EventArgs e)
        {
            Lesson newLesson = new Lesson();
            newLesson.lessonName = "new Lesson";
            lessons.Add(newLesson);
            LessonButton newButton = new LessonButton(newLesson);
            newButton.Size = new Size(splitContainer2.Panel2.Width, 40);
            newButton.Location = new Point(0, (lessons.Count-1) * 40);
            lessonButtons.Controls.Add(newButton);

            selected = newButton;
            newButton.Click += selectLessonClick;


            lessonContent.Enabled = true;
            LessonTitle.Enabled = true;
            lessonContent.Enabled = true;
        }
        private void selectLessonClick(object sender, EventArgs e)
        {
            selected = (LessonButton)sender;
            LessonTitle.Text = selected.lesson.lessonName;
            lessonContent.Text = selected.lesson.lessonContent;
            codeBlock.Text = selected.lesson.defaultCode;
        }
        private void lessonUpdate(object sender, EventArgs e)
        {
            selected.lesson.lessonName = LessonTitle.Text;
            selected.lesson.lessonContent = lessonContent.Text;
            selected.lesson.defaultCode = codeBlock.Text;
        }
    }
}
