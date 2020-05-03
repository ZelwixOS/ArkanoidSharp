using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    class Bonus
    {
        protected float x, y;
        protected char ID;
        private Bitmap bonus;

        public Bonus()
        {

        }

        public Bonus(float x, float y, char ID)
        {
            this.x = x;
            this.y = y;
            this.ID = ID;

        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public char GetID()
        {
            return ID;
        }

        public void move(int k, Form1 f1)
        {
            hide(f1);
            y = y + k;
            show(f1);
        }
        public void hide(Form1 f1)
        {
            bonus = new Bitmap(CP0.Properties.Resources.ball0);
            f1.dc.DrawImage(bonus, (int)x, (int)y);
        }
        public void show(Form1 f1)
        {
            bonus = new Bitmap(CP0.Properties.Resources.FBBonus20);
            f1.dc.DrawImage(bonus, (int)x, (int)y);
        }
    }
}
