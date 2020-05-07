using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    [Serializable]
    class FireBall : Ball
    {
        public FireBall(Level lv)
        {
            lv.MoveBall += move;
            lv.SBlock += show;
            lv.BallDChange += changeDir;
            lv.BallStart += startZap;
        }

        public override bool changeDir(char r, float c, float s)
        {
            bool logic = false;
            if (r == 11)
            {
                if (c == 1)
                    dx = dx * (-1);
                else
                    dy = dy * (-1);

            }
            return logic;
        }

        public override void show(Form1 f1)
        {
            balls = new Bitmap(CP0.Properties.Resources.fireball20);
            f1.dc.DrawImage(balls, (int)x, (int)y);
        }
    }
}
