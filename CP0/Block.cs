using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CP0
{
    class Block
    {
        private Bitmap blocks;
       // private Bitmap block;
        protected int x, y;
        protected char ID;

        public Block()
        {
        }

        public Block(int x, int y, char ID)
        {
            this.x=x;
            this.y=y;
            this.ID = ID;
        }

        public char GetID()
        {
            return ID;
        }
        public int getX() 
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public virtual void show(Form1 f1)
        {
            blocks = new Bitmap(CP0.Properties.Resources.block);
            f1.dc.DrawImage(blocks, (int)(x*64), (int)(y*16));
        }
        public void hide(Form1 f1)
        {
            blocks = new Bitmap(CP0.Properties.Resources.block0);
            f1.dc.DrawImage(blocks, (int)(x * 64), (int)(y * 16));
        }
    }
}
