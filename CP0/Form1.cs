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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CP0
{
    public delegate void controlKey(char C);
    public delegate void ObjSend(Form1 obj);
    public delegate void LvlChoosen(string x);

    
    public partial class Form1 : Form
    {
        private static Level Lvl;
        public Graphics dc;
        public Redac RedF;

        const int lvlc = 4;
        int currChoice = 1;
        bool newStart = true;
        bool GS = false;
        bool pause = false;
        

        public event controlKey controlKeyPressed;
        public event ObjSend TimeTickUpdate;
        public event LvlChoosen stringGotten;
        public event ObjSend LvlCreate;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e) // Play
        {

            pictureBox2.Load("./Sprites/STARTcl.png");
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e) // мышка на Play
        {
            pictureBox2.Load("./Sprites/STARTon.png"); 

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e) // мышка не на Play
        {
            pictureBox2.Load("./Sprites/START.png"); 
        }


        private void pictureBox2_MouseDown(object sender, MouseEventArgs e) // мышка нажата Play
        {
            pictureBox2.Load("./Sprites/STARTcl.png");
            pictureBox2.Hide();
            LvlChoice((char)0);
        }



        private void LvlChoice(char t) // выбор уровня
        {
            if (newStart ==true) // начальная отрисовка
            {
                if (Lvl != null)
                {
                    Lvl.LevelOff(this);
                }
                label1.Visible = true;
                pictureBox3.Visible = true;
                pictureBox1.Load("./Sprites/LvlChoiceBFG.png");
                Lvl = new Level(this);
                newStart = false;
            }
            if (t != 13)        // переключение
            {
                if ((t == 'w') || (t == 'W') || (t == 'ц') || (t == 'Ц') || (t == 'd') || (t == 'в') || (t == 'D') || (t == 'В'))
                {
                    currChoice++;
                    if (currChoice > lvlc)
                        currChoice = 1;
                }
                else if ((t == 's') || (t == 'Ы') || (t == 'ы') || (t == 'S') || (t == 'a') || (t == 'A') || (t == 'ф') || (t == 'Ф'))
                {
                    currChoice--;
                    if (currChoice == 0)
                        currChoice = lvlc;
                }
                label1.Text = currChoice.ToString();
            }
            else  // выбор
            {
                GS = true;
                stringGotten("./Levels/"+currChoice+".kl");
                label1.Visible = false;
                pictureBox1.BackgroundImage = CP0.Properties.Resources.fontCLR;
                pictureBox1.Image = CP0.Properties.Resources.fontCLR;
                dc = pictureBox1.CreateGraphics();
                pictureBox3.Hide();
                LvlCreate(this);
                SetTimer();
            }
        }


        private void SetTimer()
        {
            timer1.Enabled = true;
        }

        private void Form1_KeyPress_1(object sender, KeyPressEventArgs e) //  нажати на клавишу
        {
            // установка переменной c = коду клавиши
            if (pictureBox2.Visible == false)
            {


                if ((timer1.Enabled == false) && (GS == false))  // для выбора уровня
                    LvlChoice(e.KeyChar);

                else        
                {
                    switch (e.KeyChar)                         // при игре
                    {
                        case 'p': if (!pause) { timer1.Enabled = false; pause = true; } else { timer1.Enabled = true; pause = false; } break;
                        case 'r': pictureBox1.BackgroundImage = CP0.Properties.Resources.fontCLR;
                            pictureBox1.Image = CP0.Properties.Resources.fontCLR;
                            Lvl.LevelOff(this);
                            Lvl = null;
                            Lvl = new Level(this);
                            stringGotten("./Levels/" + currChoice + ".kl");
                            LvlCreate(this); break;
                        case 'h': SavePoint(); break;
                        case 'm': LoadPoint(); break;
                        default: controlKeyPressed(e.KeyChar); break;
                    }
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e) // тик таймера
        {
            if ((Lvl.getStatus() != 12) && (Lvl.getStatus() != 13))
            {
                for (int i=0; i<4; i++)
                 TimeTickUpdate(this);
            }
            else
            {
                GS = false;
                timer1.Enabled = false;
                newStart = true;
            }
        }

        private void SavePoint() // сохранение
        {
            timer1.Enabled = false; 
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("./Save/LvlSavePoint.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Lvl);
            stream.Close();
            timer1.Enabled = true; 
        }

        private void LoadPoint() // загрузка
        {
            timer1.Enabled = false;

            Lvl.LevelOff(this);

            pictureBox1.BackgroundImage = CP0.Properties.Resources.fontCLR;
            pictureBox1.Image = CP0.Properties.Resources.fontCLR;

          

            IFormatter format = new BinaryFormatter();
            Stream stream = new FileStream("./Save/LvlSavePoint.bin", FileMode.Open);
            Lvl = (Level)format.Deserialize(stream);
            Lvl.LvlInter(this);
            stream.Close();
            
            Lvl.LoadLevel(this);
            timer1.Enabled = true; 
        }

        private void pictureBox3_Click(object sender, EventArgs e) // Конструктор
        {
            RedF = new Redac();
            RedF.Show();
            RedF.Activate();
            this.Hide();
            this.Enabled = false;
            RedF.SetP(this); 
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e) // Наведение на Конструктор
        {
            pictureBox3.Load("./Sprites/ConstIconNewOn2.png"); 
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e) // мышка не на Конструкторе
        {
            pictureBox3.Load("./Sprites/ConstIcon1.png"); 
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e) // Help
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) //About
        {

        }
    }
}
