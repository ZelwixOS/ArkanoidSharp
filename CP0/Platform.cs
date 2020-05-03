using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    class Platform
    {
       private Bitmap platf;
       public int x, w;
       public Platform(int x, int w)
       {
           this.x = x;
           this.w = w;
       }

       public int getX()
       {
           return x;
       }

       public int getW()
       {
           return w;
       }

       public void setX(int a)
       {
           x=a;
       }

       public void setW(int a)
       {
           w = a;
       }

	   public void move(char key, Form1 f1, int k)
       {
           if ((key == 100) && (x < 512))
           {
               hide(f1);
               x += k;
               show(f1);
           }
           if ((key == 97) && (x > 0))
           {
               hide(f1);
               x -= k;
               show(f1);
           }
       }

       public void show(Form1 f1)
       {
           platf = new Bitmap(CP0.Properties.Resources.Pltfs);
           f1.dc.DrawImage(platf, (int)x , (int)w);
       }

       public void hide(Form1 f1)
       {
           platf = new Bitmap(CP0.Properties.Resources.Pltfs0);
           f1.dc.DrawImage(platf, (int)x, (int)w);
       }
    }
}
