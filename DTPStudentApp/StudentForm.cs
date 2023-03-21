using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DTPStudentApp
{
    public partial class StudentApp : Form
    {
        private string defaultCode = "";
        private LessonButton selected = null;
        private List<Lesson> loadedLessonFile;
        private string loadedLessonFileName;
        public StudentApp()
        {
            InitializeComponent();
            //Lesson testLesson = new Lesson("lessonName", "Test Lesson", "This is a test lesson", defaultCode);
            //List<Lesson> lessons = new List<Lesson>();
            //lessons.Add(testLesson);
            //saveLessonFile("testLes.les",lessons);
            if (File.Exists("./DefaultCode.cs"))
                defaultCode = File.ReadAllText("./DefaultCode.cs");
            codeBlock.Text = defaultCode;


            loadedLessonFile = loadLessonFile("./testLes_StudentCpy.les");
            loadedLessonFileName = "./testLes.les".Replace(".les", "");
            //In future this should be set b4 hand
            loadedLessonFile[0].setChallangeExpectedOutput("\"test\"", "test", "string");
            loadedLessonFile[0].hideMainFunction = true;
            loadedLessonFile[0].completed = false;

        }
        private void saveLessonFile(string path, List<Lesson> lessons)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(fileStream, lessons);
            fileStream.Close();
        }
        private List<Lesson> loadLessonFile(string path, bool initButtons = true)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Lesson> serializedLessons = (List<Lesson>)formatter.Deserialize(fileStream);
            fileStream.Close();
            if (!initButtons)
                return serializedLessons;
            foreach (Lesson les in serializedLessons)
            {
                LessonButton b = new LessonButton(les);
                b.Size = new Size(infoLessonContainer.Panel2.Width, 40);
                b.MouseClick += lessonSelect;

                if (les.completed)
                    b.completed();
                buttonList.Controls.Add(b);
            }
            return serializedLessons;
        }
        private void lessonSelect(object sender, EventArgs e)
        {
            selected = (LessonButton)sender;
            lessonTitle.Text = selected.lesson.lessonName;
            lessonContent.Text = selected.lesson.lessonContent;

            //ToDo write a function that deletes the main function if the \

            codeBlock.Text = selected.lesson.defaultCode;

        }
        private void hideLessonButton_Click(object sender, EventArgs e)
        {
            Button self = (Button)sender;
            infoLessonContainer.Panel2Collapsed = !infoLessonContainer.Panel2Collapsed;
            self.Location = new Point(infoLessonContainer.Panel1.Width - self.Width, self.Location.Y);
            self.Text = self.Text == "<" ? ">" : "<";

        }
        private void Run_Click(object sender, EventArgs e)
        {
            File.Delete("code.exe");
            compilerBlock.Text = compileCode();
            compilerBlock.Text += "\n";
            if (File.Exists("code.exe"))
            {
                (string stdOut, int exitCode) processOutput = runProcess("code.exe", "");
                compilerBlock.Text += "\n"+processOutput.stdOut;
                if (selected == null)
                    return;
                if (processOutput.exitCode == 1)
                {
                    compilerBlock.Text += "\nWell Done you've completed this challenge";
                    selected.lesson.completed = true;
                    selected.completed();
                }

            }
        }
        private string compileCode()
        {
            File.Delete("code.cs");
            File.WriteAllText("./code.cs", codeBlock.Text);
            (string stdOut, int exitCode) processOutput = runProcess("csc.exe", "code.cs");

            return processOutput.stdOut;
        }
        private (string stdOut, int exitCode) runProcess(string path, string args)
        {
            Process Complier = Process.Start(path, args);
            Complier.StartInfo.UseShellExecute = false;
            Complier.StartInfo.RedirectStandardOutput = true;
            Complier.StartInfo.CreateNoWindow = true;
            Complier.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Complier.EnableRaisingEvents = true;

            string recived = "";
            Complier.OutputDataReceived += new DataReceivedEventHandler((s, ev) =>
            {
                if (!String.IsNullOrEmpty(ev.Data))
                    recived += ev.Data;
            });
            Complier.Start();
            Complier.BeginOutputReadLine();
            Complier.WaitForExit();
            return (recived, Complier.ExitCode);
        }

        private void StudentApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveLessonFile($"./{loadedLessonFileName}_StudentCpy.les", loadedLessonFile);
        }

        private void StudentApp_Load(object sender, EventArgs e)
        {

        }
    }
}