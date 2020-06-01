using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CP0
{
    public delegate void ShHide(Redac Rc); 

    public partial class Redac : Form
    {
        Form1 f1;
        public Graphics dc;
        Block[,] ukb = new Block[5, 9];
        int a=0;
        int b = 0;
        protected char[,] matr = new char[5, 9];
        Point Pointer;

        public event ShHide SBlock;
        public event ShHide MovePoint;

    

        public Redac()
        {
            InitializeComponent();
            dc = pictureBox1.CreateGraphics();
            pictureBox1.BackColor = Color.Black;

        }

        public void SetP(Form1 buff)
        {
            f1 = buff;
            Pointer = new Point(a, b, this);
            SBlock(this);
        }

        private void Redac_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Show();
            f1.Enabled = true;
        }

        private void Redac_KeyPress(object sender, KeyPressEventArgs e)
        {
            MovePoint(this);
            if (a < 5)
            {
                if (b < 9)
                {
                    matr[a, b] = e.KeyChar;
                    switch (e.KeyChar)
                    {
                        case (char)49: ukb[a, b] = new Block(b, a, (char)49, this); break;
                        case (char)50: ukb[a, b] = new BonBlock(b, a, (char)50, this); break;
                    }

                    b++;
                }
                else
                {
                    b = 0; a++;
                    if (a < 5)
                    {
                        matr[a, b] = e.KeyChar;
                        switch (e.KeyChar)
                        {
                            case (char)49: ukb[a, b] = new Block(b, a, (char)49, this); break;
                            case (char)50: ukb[a, b] = new BonBlock(b, a, (char)50, this); break;
                        }
                        b++;
                    }
                }
                if (SBlock != null)
                    SBlock(this);
            }
            else textBox1.Enabled = true;
        }

        private void ResetB_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.BackColor = Color.Black;
            a = 0;
            b = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 9; j++)
                {
                    matr[i, j] = (char) 0;
                    if (ukb[i, j] != null)
                    ukb[i, j].DisagreeS(this);
                    ukb[i, j] = null;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname = textBox1.Text + ".kl";
            using (BinaryWriter writer = new BinaryWriter(File.Open(fname, FileMode.Create))) 
            {
                
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        writer.Write(matr[i, j]);
                    }
            }
            label1.Text = "Saved as " + fname +".kl";
        }

        private void Redac_Load(object sender, EventArgs e)
        {

        }
    }
}
