using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using SocketIOClient;
using Woof.SystemEx;

namespace DTPStudentApp
{
    public partial class StudentApp : Form
    {
        private string defaultCode = "";
        private LessonButton selected = null;
        private List<Lesson> loadedLessonFile;
        private string loadedLessonFileName;
        private string token;
        public StudentApp()
        {
            FileStream cache = new FileStream("./cache", FileMode.OpenOrCreate);
            byte[] buffer = new byte[12];
            cache.Read(buffer, 0, 12);
            
            if (buffer[11] == '\0')
            {
                buffer = genNewToken();
                cache.Write(buffer, 0, 12);
            }
            token = String.Concat(buffer);


            SocketIO socket = new SocketIO("https://learningappserver.nathankleine1.repl.co");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("token",token);

            socket.Options.ExtraHeaders = headers;
            socket.OnConnected += connectionEstablished;
            socket.ConnectAsync();

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
            loadedLessonFile[0].lessonContent = "For this test lesson please make the \nfunction test return the string \"test\"";

        }
        private void connectionEstablished(object Sender, EventArgs e)
        {
            SocketIO socket = (SocketIO)Sender;
            socket.On("getUserProfile", res =>
            {
                //https://stackoverflow.com/questions/5570113/c-sharp-how-to-get-current-user-picture
                Bitmap userPfp = new Bitmap(SysInfo.GetUserPicturePath());
                //use JPG in later version 
                //https://stackoverflow.com/questions/7350679/convert-a-bitmap-into-a-byte-array
                ImageConverter converter = new ImageConverter();    
                socket.EmitAsync("returnUserProfile", new object[]{ Environment.UserName, (byte[])converter.ConvertTo(userPfp, typeof(byte[])) });
            });
            Console.WriteLine("Connected");
        }
        private byte[] genNewToken()
        {
            byte[] rnd = new byte[12];
            Random r = new Random();
            r.NextBytes(rnd);
            return rnd;
        }
        private void saveLessonFile(string path, List<Lesson> lessons)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(fileStream, lessons);
            fileStream.Close();
        }
        private List<Lesson> loadLessonFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Lesson> serializedLessons = (List<Lesson>)formatter.Deserialize(fileStream);
            fileStream.Close();

            foreach (Lesson les in serializedLessons)
            {
                LessonButton b = new LessonButton(les);
                b.Size = new Size(infoLessonContainer.Panel2.Width, 40);
                b.MouseClick += lessonSelect;
                b.Text = les.lessonName;

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
                compilerBlock.Text += processOutput.stdOut;
                if (selected == null)
                    return;
                if (processOutput.exitCode == 1)
                {
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