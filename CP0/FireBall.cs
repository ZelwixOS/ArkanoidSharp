using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    class FireBall : Ball
    {

        public override void changeDir(char r, float c, float s)
        {
            if (r == 11)
            {
                if (c == 1)
                    dx = dx * (-1);
                else
                    dy = dy * (-1);

            }

        }

        public override void show(Form1 f1)
        {
            balls = new Bitmap(CP0.Properties.Resources.fireball20);
            f1.dc.DrawImage(balls, (int)x, (int)y);
        }
    }
}
