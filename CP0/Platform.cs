using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    [Serializable]
    class Platform
    {
       private Bitmap platf;
       public int x, w;
       public Platform(int x, int w, Level lv)
       {
           this.x = x;
           this.w = w;
           lv.SBlock += show;
           lv.PlatfMove += move;
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

	   protected void move(char key, Form1 f1, int k)
       {
           if (((key == 'в') || (key == 'В') || (key == 'd') || (key == 'D')) && (x < 512))
           {
               hide(f1);
               x += k;
               show(f1);
           }
           if (((key == 'ф') || (key == 'Ф') || (key == 'a') || (key == 'A')) && (x > 0))
           {
               hide(f1);
               x -= k;
               show(f1);
           }
       }

       protected void show(Form1 f1)
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
