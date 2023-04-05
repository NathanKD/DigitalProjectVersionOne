using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherTool
{
    public partial class TeacherPlanner : Form
    {
        public TeacherPlanner()
        {
            InitializeComponent();
            Bitmap b = new Bitmap(100,100);
            for (int x = 0; x < 100; x++) 
                for(int y = 0; y < 100; y++)
                    b.SetPixel(x, y, Color.Red);
            
            for(int i = 0; i < 100; i++)
            {
                StudentUIPanel test = new StudentUIPanel("Test_User", b);
                studentSelect.Controls.Add(test);
                test.profilePicture.Click += studentClick;
                //test.BackColor = Color.Red;
                test.setLocation(new Point(80 * i, 0));
            }
            
            
        }
        
        private void studentClick(object sender, EventArgs e)
        {
            PictureBox selected = (PictureBox)sender;
            Console.WriteLine("Guh");
        }
        private void TeacherPlanner_Load(object sender, EventArgs e)
        {

        }
    }
    class StudentUIPanel : Panel
    {
        public PictureBox profilePicture;
        public Label studentName;
        public StudentUIPanel(string username, Bitmap profileBitmap)
        {
            studentName = new Label();
            studentName.Text = username;
            studentName.Parent = this;
            studentName.Anchor = AnchorStyles.Bottom;
            studentName.Location = new Point(50, 85);
            studentName.TextAlign = ContentAlignment.TopCenter;

            profilePicture = new PictureBox();
            profilePicture.Image = profileBitmap;
            profilePicture.Size = new Size(70,70);
            profilePicture.Parent = this;
            profilePicture.Dock = DockStyle.Fill;

            this.Size = new Size(70,70);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(studentName);
            this.Controls.Add(profilePicture);
        }
        public void setLocation(Point newLocation)
        {
            this.Location = newLocation;
        }
    }
}
