using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace CP0
{
    public partial class Form1 : Form
    {
        private static Level Lvl;
        public Graphics dc;
        const int lvlc = 3;
        int currChoice = 1;
        bool newStart = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           //Graphics gr = Graphics.FromHwnd(pictureBox2.Handle);
           // gr.Clear(Color.White);
            pictureBox2.Load("./Sprites/STARTcl.png");
            Level Lvl = new Level();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Load("./Sprites/STARTon.png"); 
            //Graphics gr = Graphics.FromHwnd(pictureBox2.Handle);
             //gr.Clear(Color.White);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Load("./Sprites/START.png"); 
        }


        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Load("./Sprites/STARTcl.png");
            pictureBox2.Hide();
            LvlChoice((char)0);
        }

        //public delegate void CurLvl();
        //public event CurLvl Refresh;

        private void LvlChoice(char t)
        {
            if (newStart ==true)
            {
                label1.Visible = true;
                pictureBox1.Load("./Sprites/LvlChoiceBFG.png");
                Lvl = new Level();
                newStart = false;
            }
            if (t != 13)
            {
                if (t == (char)119)
                {
                    currChoice++;
                    if (currChoice > lvlc)
                        currChoice = 1;
                }
                else if (t == (char)115)
                {
                    currChoice--;
                    if (currChoice == 0)
                        currChoice = lvlc;
                }
                label1.Text = currChoice.ToString();
            }
            else 
            { 
                Lvl.setMatr("./Levels/"+currChoice+".kl");
                label1.Visible = false;
                pictureBox1.BackgroundImage = CP0.Properties.Resources.fontCLR;
                pictureBox1.Image = CP0.Properties.Resources.fontCLR;
                dc = pictureBox1.CreateGraphics();
                Lvl.createLvl(this);
                SetTimer();
            }
        }


        private void SetTimer()
        {
            timer1.Enabled = true;
        }

        private void Form1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // установка переменной c = коду клавиши
            if (pictureBox2.Visible == false)
            {
                Lvl.setC(e.KeyChar);
                if (timer1.Enabled == false)
                {
                    LvlChoice(e.KeyChar);
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((Lvl.status != 12) && (Lvl.status != 13))
            {
                for (int i=0; i<4; i++)
                Lvl.refresh(this);
            }
            else
            {
                timer1.Enabled = false;
                newStart = true;
            }
        }

    }
}
