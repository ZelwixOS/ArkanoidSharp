using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    class Point
    {

        protected int x, y;
        private Bitmap point;


        public Point(int x, int y, Redac Rc)
        {
            this.x = x;
            this.y = y;
            Rc.SBlock += show;
            Rc.MovePoint += move;
        }

        protected void move(Redac f1)
        {
            if (x < 5)
            {
                    hide(f1);
                    y++;
                    
                    if (y >= 9)
                    {
                        y = 0; x++;
                        if (x >= 5)
                        {
                            hide(f1);
                            f1.SBlock -= show;
                        }

                        else
                            show(f1);
                        
                    }
            }
        }

        public void SetCoord(int a, int b, Redac f1)
        {
            f1.SBlock += show;
            hide(f1);
            x = a;
            b = y;
            show(f1);
        }


        public void show(Redac f1)
        {
            point = new Bitmap(CP0.Properties.Resources.PointPix);
            f1.dc.DrawImage(point, (int)(y * 64), (int)(x * 16));
        }
        public void hide(Redac f1)
        {
            point = new Bitmap(CP0.Properties.Resources.Hpoint);
            f1.dc.DrawImage(point, (int)(y * 64), (int)(x * 16));
        }
    }
}
