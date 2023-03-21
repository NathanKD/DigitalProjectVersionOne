using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DTPStudentApp
{
    [Serializable]
    class Lesson
    {
        public string lessonContent;
        public string lessonName;
        public string lessonDescription;
        public string defaultCode;
        private string functionOutputName;
        public bool completed = false;
        public bool hideMainFunction = false;
        public Lesson(string lessonName = "new Lesson", string lessonDescription = "new Lesson", string lessonContent = "new Lesson", string defaultCode = "")
        {
            this.lessonDescription = lessonDescription;
            this.lessonName = lessonName;
            this.lessonContent = lessonContent;
            this.defaultCode = defaultCode;

        }
        public void setChallangeExpectedOutput(string functionOutName, string functionType, string expectedOutput)
        {
            if (functionOutName == "Main")
                throw new Exception("Cannot Use Main Function to create challange");
            //if (!defaultCode.Contains(functionOutName))
            //    throw new Exception("Code does not contain that function");
            this.functionOutputName = functionOutName;
            if (!File.Exists("./ChallangeFramework.cs"))
                throw new Exception("ChallangeFramework.cs could not be found");
            hideMainFunction = true;
            this.defaultCode = File.ReadAllText("./ChallangeFramework.cs");
            this.defaultCode = File.ReadAllText("./ChallangeFramework.cs");
            this.defaultCode = defaultCode.Replace("FunctionName", functionOutName);
            this.defaultCode = defaultCode.Replace("ExpectedOutput", expectedOutput);
            this.defaultCode = defaultCode.Replace("Function", $"{functionType} {functionOutName}()\n        {{\n\n\n        }}");
            Console.WriteLine(defaultCode);
        }
    }
    //Extend the Default C# button class to include a refernce to a lesson
    class LessonButton : Button
    {
        public Lesson lesson;
        public LessonButton(Lesson lesson)
        {
            this.lesson = lesson;
            this.Text = lesson.lessonName;
        }
        public void completed()
        {
            this.BackColor = Color.Green;
        }
    }
}
