using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP0
{
    public partial class HelpWindow : Form
    {
        char curr = '1';
        string fname = "./Help/";
        const char cmax = '8';

        public HelpWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curr--;
            if (curr < '1')
                curr = cmax;
            pictureBox1.Load(fname+curr+".png"); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            curr++;
            if (curr > cmax)
                curr = '1';
            pictureBox1.Load(fname + curr + ".png"); 
        }


    }
}
