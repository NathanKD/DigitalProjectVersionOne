using DTPStudentApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        private string ChallangeCodeFramework;
        private string savePath = "./savedLessons";
        public LessonPlanner()
        {
            InitializeComponent();
            defaultCode = codeBlock.Text;
            if (!Directory.Exists("./savedLessons"))
                Directory.CreateDirectory("./savedLessons");
            if (File.Exists("./ChallangeFramework.cs"))
                ChallangeCodeFramework = File.ReadAllText("./ChallangeFramework.cs");
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

            ContextMenu lessonRightClick = new ContextMenu();
            lessonRightClick.MenuItems.Add(new MenuItem("Challenge Lesson", makeLessonchallenge));

            newButton.ContextMenu = lessonRightClick;

            selected = newButton;
            newButton.Click += selectLessonClick;

            
            lessonContent.Enabled = true;
            LessonTitle.Enabled = true;
            codeBlock.Enabled = true;

            saveLessonFile(lessons);
        }
        private void makeLessonchallenge(object sender, EventArgs e)
        {
            MenuItem self = (MenuItem)sender;
            LessonButton selfButton = (self.Parent as ContextMenu).SourceControl as LessonButton;
            codeBlock.Text = ChallangeCodeFramework;
            saveLessonFile(lessons);
            //selfButton.lesson.setChallangeExpectedOutput();
        }
        private void selectLessonClick(object sender, EventArgs e)
        {
            selected = (LessonButton)sender;
            LessonTitle.Text = selected.lesson.lessonName;
            lessonContent.Text = selected.lesson.lessonContent;
            codeBlock.Text = selected.lesson.defaultCode;
            saveLessonFile(lessons);
        }
        private void lessonUpdate(object sender, EventArgs e)
        {
            selected.lesson.lessonName = LessonTitle.Text;
            selected.lesson.lessonContent = lessonContent.Text;
            selected.lesson.defaultCode = codeBlock.Text;
            saveLessonFile(lessons);
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
