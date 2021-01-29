using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ImGui_Visual_Tool
{
    public partial class Form1 : Form
    {
        Point previousPoint;
        bool clicked = false;

        public string code = "// Generated with ImGui Visual Tool v1\n";

        public List<Rectangle> boxesToDraw = new List<Rectangle>();

        public Form1()
        {
            InitializeComponent();

            this.pictureBox1.Paint += pictureBox1_Paint;
            this.pictureBox1.MouseDown += pictureBox1_MouseDown;
            this.pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            this.pictureBox1.MouseUp += pictureBox1_MouseUp;
            this.pictureBox1.MouseMove += pictureBox1_MouseMove;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            previousPoint = e.Location;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            clicked = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(clicked == true)
            {
                Pen pen = new Pen(Brushes.Red, 1);
                pictureBox1.CreateGraphics().DrawRectangle(pen, previousPoint.X, previousPoint.Y, Cursor.Position.X, Cursor.Position.Y);
                pictureBox1.Invalidate();
            }
            foreach (Rectangle box in boxesToDraw)
            {
                if(e.Location.X == box.X && e.Location.Y == box.Y)
                {
                    deleteButton = new System.Windows.Forms.Button();
                    deleteButton.Location = new Point(box.X, box.Y);
                    deleteButton.BackColor = Color.Red;
                    deleteButton.Text = "x";
                    deleteButton.Width = 40;
                    deleteButton.Height = 40;
                    deleteButton.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;

            boxesToDraw.Add(new Rectangle(previousPoint.X, previousPoint.Y, e.Location.X, e.Location.Y));
            
            if(checkBox2.Checked == true) code += $"ImGui::GetOverlayDrawList()->AddRectFilled(ImVec2(LeftArm.x-{previousPoint.X}, Head.y), ImVec2(LeftArm.x + {e.Location.X}, RightFoot.y), ImGui::GetColorU32({{255.f, 0.f, 0.f, 1.f}}), 1);\n";
            if (checkBox1.Checked == true)
            {
                if (checkBox3.Checked == true) 
                    code += $"char DistanceStr[10];\nsprintf(DistanceStr, \" [%im]\", Distance);\nPlayerName.append(DistanceStr);\n";
                code += $"ImGui::GetOverlayDrawList()->AddText(ImVec2({previousPoint.X} - Head.x, Head.y), ImGui::GetColorU32({{255.f, 0.f, 0.f, 1.f}}), PlayerName);\n";
            }

            string msg = $"Added rectangle ({previousPoint.X}, {previousPoint.Y}) ({e.Location.X}, {e.Location.Y})\n";
            richTextBox1.Text += msg;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Rectangle box in boxesToDraw)
            {
                Pen pen = new Pen(Brushes.Red, 1);
                string NameString = textBox1.Text;
                Random random = new Random();
                if (checkBox3.Checked) NameString += $" [{random.Next(200)}m]";
                if (checkBox1.Checked) e.Graphics.DrawString(NameString, new Font("Verdana", 12), Brushes.Red, new Point(box.X, box.Y));
                if (checkBox2.Checked) e.Graphics.DrawRectangle(pen, box.X, box.Y, box.Width, box.Height);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "player1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            code = "// Generated with ImGui Visual Tool v1\n";
            boxesToDraw.Clear();
            pictureBox1.Invalidate();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(code);
        }
    }
}
