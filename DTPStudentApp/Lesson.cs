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
        private string expectedOutput;
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
        public void setChallangeExpectedOutput(string exptectedOutput, string functionOutName, string functionType)
        {
            if (functionOutName == "Main")
                throw new Exception("Cannot Use Main Function to create challange");
            //if (!defaultCode.Contains(functionOutName))
            //    throw new Exception("Code does not contain that function");
            this.expectedOutput = exptectedOutput;
            this.functionOutputName = functionOutName;
            if (!File.Exists("./ChallangeFramework.cs"))
                throw new Exception("ChallangeFramework.cs could not be found");
            hideMainFunction = true;
            this.defaultCode = File.ReadAllText("./ChallangeFramework.cs");
            this.defaultCode = defaultCode.Replace("FunctionName", functionOutName);
            this.defaultCode = defaultCode.Replace("ExpectedOutput", exptectedOutput);
            this.defaultCode = defaultCode.Replace("Function", $"{functionType} {functionOutName}()\n        {{\n\n\n        }}");
        }
    }
    //Extend the Default C# button class to include a refernce to a lesson
    class LessonButton : Button
    {
        public Lesson lesson;
        public LessonButton(Lesson lesson)
        {
            this.lesson = lesson;
        }
        public void completed()
        {
            this.BackColor = Color.Green;
        }
    }
}
