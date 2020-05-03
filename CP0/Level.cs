using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace CP0
{
    public delegate void Move(Form1 f1, int v);
    public delegate void ShowHide(Form1 f1);

    class Level
    {
        //public event controlKey controlKeyPressed;
        public event Move MoveBall;
        public event ShowHide SBlock;
        public event ShowHide HBlock;

        protected char[,] matr = new char[5, 9];
        public int status;
        protected int timer;
        protected char c;
        Block[,] ukb = new Block[5, 9];
        Platform platf;
        Ball ball;
        Bonus [] bon;

        int bonc, actb, pasb;
        const int lvlc = 3;

        /* Статусы
        13 -победа
        12 - проигрыш
        2 - ожидание старта
        1 - игра
        5 - мяч коснулся платформы (краткое прилипание) 
        */
        protected const int tik = 10;
        bool time = false;
        int l = 0;
        bool st5 = false;
        bool fd = true;

        public int getStatus()
        {
            return status;
        }

        public Level()
        {
           
        }

        public Level(Form1 f1)
        {
            f1.stringGotten += setMatr;
            f1.LvlCreate += createLvl;
        }

        protected void setC(char kb)
        {
            c = kb;
        }


        public void setMatr(string fileName)
        {         
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        matr[i, j] = reader.ReadChar();
                    }
            }

        }

        public void refresh(Form1 f1) 
        {
            
            if (fd == true)
            {
                SBlock(f1);
                fd = false;
            }
            // отрисовка при первом обновлении
            if ((status != 13) && (status != 12))
            {
                l++;
                if (l == tik+1)
                    l = 0;
                if (c != (char)0)
                {

                    if ((c == 32) && (status == 2))
                    {
                        status = 1;
                        ball.startZap(f1); 
                    } // запуск игры при нажатии на пробел

                    if (status == 5)
                    {
                        if (c == 100) // вправо
                        {
     
                            ball.changeDir((char)5, 1, 0);
                        }
                        else if (c == 97)
                        {
                       
                            ball.changeDir((char)5, -1, 0);
                        }
                    } // отклонение при прилипании


                    platf.move(c, f1, tik);
    

                    if (status == 2)
                    {
                      
                        ball.changeDir(c, 0, 0);

                        MoveBall(f1, tik);
                    } //движение при ожидании старта


                    c = (char)0;

                }  // обработка нажатой клавиши
                if (l == tik)
                    provStolk(l, f1);
                if ((status == 5) && (st5 == false))
                    st5 = true;
                else if (status == 5) { status = 1; st5 = false; } // отключение прилипания
                if ((status == 1) && (l == tik))
                {

                    MoveBall(f1, tik);
                } // движение мяча в игре
                if (l == tik)
                    for (int i = pasb; i < (pasb + actb); i++)
                    {
                        bon[i].move(l, f1);
                          // !!!!!
                    } // движение бонусов вниз
                if (timer > 0)
                    timer--; // таймер действия бонуса

                if (timer == 1)
                    time = true;
                if ((timer <= 0) && (time == true))
                {
                    Ball ext;
                    ext = ball;
                    ball = new Ball(this);
                    ball.setX(ext.getX());
                    ball.setY(ext.getY());
                    // установка координат
                    ball.setDy(ext.getDy());
                    ball.setDx(ext.getDx());
                     // установка напрвления
                    ext.StopDraw(this);
                    time = false;
                } //смена огненного мяча на обычный

            } //обновление игры
            if (status == 13) showWin(f1);
            else if (status == 12) showLose(f1);
        }

        public void createLvl(Form1 f1)
        {
            bonc = 0;
            actb = 0;
            pasb = 0;
            timer = 0;
            for (int i = 0; i < 5; i++)
                for (int t = 0; t < 9; t++)
                {
                    switch (matr[i, t])
                    {
                        case (char)49: ukb[i, t] = new Block(t, i, (char)49, this);
                            break;
                        case (char)50: ukb[i, t] = new BonBlock( t, i, (char)50, this);
                            bonc++;
                            break;
                    }

                }
            bon = new Bonus[bonc];
            platf = new Platform(268, 520, this);
            ball= new Ball(this);
            ball.startConf(288, 500);
            SBlock(f1);
            f1.controlKeyPressed += setC;
            f1.TimeTickUpdate += refresh;
            status = 2;
        }

        protected void stolkn(bool napr, bool st)
        {
            char vib;
            if (st == true)
                vib = (char)11;
            else
                vib = (char)1;
                if (napr == true)
                {
 
                    ball.changeDir(vib, 0, 0);
                }
                else
                {

                    ball.changeDir(vib, 1, 0);
                }
        } // обработка столкновения

        protected void provStolk(int v, Form1 f1)
        {
            float a =ball.getX()+v*ball.getDx();
            float b = ball.getY() + v * ball.getDy();
            float pdx=0;


            if (status == 1)
            {
                if ((a <= 0) || (a >= 556))
                {
                    MoveBall(f1, v);
                    stolkn(false, true);
                } // столкновение с боковыми стенками
                if (b <= 0)
                {
                    MoveBall(f1, v);
                    stolkn(true, true);
                } // столкновение с потолком
                else if (b >= 659) status = 12; // падение на дно

                pdx = platf.getX();

                if ((pdx - 20 <= a) && (pdx + 64 >= a) && (b >= 500) && (b <= 508) && (status == 1))
                {
                    MoveBall(f1, v);
                    stolkn(true, true);
                    status = 5;
                } // столкновение мяча с верхом платформы
                else if ((((pdx - 18 >= a) && (pdx - 22 <= a)) || ((pdx + 62 <= a) && (pdx + 66 >= a))) && (b > 500) && (b < 530) && (status == 1))
                {
                    MoveBall(f1, v);
                    stolkn(false, true);
                } // столкновение мяча с боком платформы
                bool st = false;
                int o=0;
                int p=0;
                int tl = 0;  // l

                bool dob = false;
                bool prev = false;
                for (int i = 0; i < 5; i++)
                {
                    for (int t = 0; t < 9; t++)
                    {
                        if (ukb[i, t] != null)
                        {

                            tl++;
                            if (tl == 2)
                                if (prev)
                                    dob = true;
                                else dob = false;
                            prev = true;

                            int blx = ukb[i, t].getX()*64;
                            int bly = ukb[i, t].getY()*16;

                            if ((blx - 20 <= a) && (blx + 64 >= a) && (b >= bly - 20) && (b <= bly + 16)) // 
                            {
                                o = i;
                                p = t;
                                st = true;
                                ukb[i, t].AgreeH(this);
                                char typeb = ukb[i, t].GetID();
                                
                                if ( typeb == (char)50)
                                {
                                    bon[actb + pasb] = new Bonus((float)(blx + 8), (float)(bly + 8), (char)50, this);
                                    bon[actb + pasb].show(f1);
                                    actb++;
                                }
                                ukb[i, t] = null;
                            }

                        }
                    }
                    if (HBlock != null)
                         HBlock(f1);
                    prev = false;
                    if (st) break;
                } // проверка столкновения с блоками и вызов соответсвующих бонусов


                for (int i = pasb; i < pasb + actb; i++)
                {
                    float bonix = bon[i].GetX();
                    float boniy = bon[i].GetY();
                    char boniID = bon[i].GetID();
                    if ((boniID==(char)50)&&(boniy >= 500) && (boniy <= 530) && (bonix > pdx - 20) && (bonix < pdx + 64))
                    {

                        bon[i].hide(f1);
                        bon[i] = null;
                        actb--;
                        pasb++;
                        Ball ex = new Ball(this);
                        ex = ball;
                        ball = new FireBall(this);

                        ball.setX(ex.getX());
                        ball.setY(ex.getY()); // установка координат
                        ball.setDx(ex.getDx());
                        ball.setDy(ex.getDy()); // установка напрвления

                        ex.StopDraw(this);

                        timer = 1000;
                    }
                    if (boniy >= 659)
                    {
                        bon[i].hide(f1);
                        bon[i] = null;
                        actb--;
                        pasb++;
                    }
                } //проверка столкновения бонуса ID==50 с платформой, запуск FireBall


                if (st)
                {
                    MoveBall(f1, v);
                    o *= 30;  //  высота
                    p *= 120; // длина

                    //Msg.id=2;
                    //ball->event(Msg); 
                    a = a - v * ball.getDx();
                    b = b - v * ball.getDy();
                    // координаты мяча до удара




                    if (l > 1)
                        if (dob)
                            stolkn(true, false);
                        else
                            stolkn(false, false);
                    else
                    {
                        if ((a>o-20)&&(a<o+64)&&((b<=p-20) || (b>=p+16)))
                            stolkn(true, false);
                        else
                            //if ((b<p-20)&&(b>p+16)&&((a<=o-20)||(a>=o+64)))
                            stolkn(false, false);

                    }

                } // если столе=кновение с блоком было, то оно обрабатывается для мяча
                if (l == 0)
                    status = 13;
            }

        }
        protected void showLose(Form1 f1)
        {
            Bitmap LoseScreen;
            LoseScreen = new Bitmap(CP0.Properties.Resources.SL2);
            f1.dc.DrawImage(LoseScreen, 0, 0);

        }
        protected void showWin(Form1 f1)
        {
            Bitmap WinScreen;
            WinScreen = new Bitmap(CP0.Properties.Resources.SW2);
            f1.dc.DrawImage(WinScreen, 0, 0);

        }
    }
}
