using DTPStudentApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LessonCreator
{
    public partial class LessonPlanner : Form
    {
        private List<Lesson> lessons = new List<Lesson>();
        private LessonButton selected;
        private string defaultCode;
        private string savePath = "./savedLessons";
        public LessonPlanner()
        {
            InitializeComponent();
            if (!Directory.Exists("./savedLessons"))
                Directory.CreateDirectory("./savedLessons");
            if (File.Exists("./DefaultCode.cs"))
                defaultCode = File.ReadAllText("./DefaultCode.cs");
        }

        private void newLesson_Click(object sender, EventArgs e)
        {
            Lesson newLesson = new Lesson();
            newLesson.lessonName = "new Lesson";
            newLesson.defaultCode = defaultCode;

            lessons.Add(newLesson);
            LessonButton newButton = new LessonButton(newLesson);
            newButton.Size = new Size(splitContainer2.Panel2.Width, 40);
            newButton.Location = new Point(0, (lessons.Count-1) * 40);
            lessonButtons.Controls.Add(newButton);

            selected = newButton;
            newButton.Click += selectLessonClick;


            lessonContent.Enabled = true;
            lessonContent.Text = newLesson.lessonContent;
            LessonTitle.Enabled = true;
            LessonTitle.Text = newLesson.lessonName;
            lessonContent.Enabled = true;
            lessonContent.Text = newLesson.lessonContent;

            codeBlock.Enabled = true;
            codeBlock.Text = newLesson.defaultCode;

            saveLessonFile(lessons);
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

        private void LessonPlanner_Load(object sender, EventArgs e)
        {

        }
        private void saveLessonFile(List<Lesson> lessons)
        {
            //This is not safe
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(fileStream, lessons);
            fileStream.Close();
        }
    }
}
