using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleFormulaDrawer.Core;
using System.Threading;

namespace SimpleFormulaDrawer.interfac
{
    public partial class GraphForm : Form
    {
        private bool Is3DRender;
        private new Pictogramm Parent;
        private double Rotation, Altitude; //Для рендера 3Д. Поворот относительно OZ и поворот относительно OX 

        public int FormState = 0;
        private Pen AxisColor = new Pen(Color.Black);
        private Brush AxisFont = new SolidBrush(Color.Black);
        private Graphics GR; //Графический контекст формы
        private Bitmap BMP=new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height); //Модель в памяти на весь экран. Форма не сможет быть больше :)
        private Graphics GBMP; //Отображение BMP на обьект Graphics. Нужно для рисования на нем.
        public int Depth;//Глубина формы. Да, я не курил.
        public GraphForm(Pictogramm Parent)
        {
            InitializeComponent();
            this.Parent = Parent;
        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width / 5;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width / 5 * 4;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Depth = (this.Width + this.Height) / 2;
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.GR = this.CreateGraphics();
            this.GBMP = Graphics.FromImage(BMP);
        }

        private void GraphForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Parent.ClosePictogramm();
        }

        public void Set3DRendering(bool How)
        {
            Is3DRender = How;
        }
        
        public static PointF ConvertTo2DPoint(Point3DF What)
        {
            var toRet = new PointF();
            //NOTE:Need Alexander Igorevitch
            return toRet;
        }

        public void DrawAxis(params double [] AxisMarks)
        {
            /*Эта функция принимает 6 аргументов и обрабатывает 4 или 6 аргументов в зависимоти от флага IS3DRender.
             * Порядок следования аргуменов:
             * Xmin,Xmax,Ymin,YMax,Zmin,Zmax
             */
            float ZeroX = Shared.WhereZero(AxisMarks[0], AxisMarks[1], this.Width);
            float ZeroY = Shared.WhereZero(AxisMarks[2], AxisMarks[3], this.Height);
            float ZeroZ = Shared.WhereZero(AxisMarks[4], AxisMarks[5], this.Depth);
#if DEBUG
            MessageBox.Show(string.Format("{0}||{1}||{2}||{3}", ZeroX, ZeroY, ZeroZ,Is3DRender));
#endif
            if ((Is3DRender && AxisMarks.Length<4) || (!Is3DRender && AxisMarks.Length<6)) throw new InvalidDataException();

            this.Is3DRender = false;//NOTE:Do not render 3d graphics. I Need GLUSKERR.

            if (Is3DRender)
            {
                //NOTE:Incomplete.
            }
            else
            {
                GBMP.DrawLine(AxisColor, 0, ZeroY, this.Width, ZeroY); //OX
                GBMP.DrawLine(AxisColor, ZeroX, 0, ZeroX, this.Height); //OY
            }
            MarkAxis(AxisMarks);
        }

        private void MarkAxis(params double[] AxisMarks)
        {
            /*Количество марок расчитывается так: обязательно рисуется 0, если есть.
             * Если есть, обязательно рисуется +-1 и далее по единице.
             * если 0 не присутствует, то делается так: на каждую марку приходится 50 пикселей и вперед без песни :)
             * Если марки не попадают по целым числам, хотя они есть то предыдущая строчка нарушается в пользу целых чисел.
             * И кажется, это большой кусок логического кода :)
             * */
            if (Is3DRender)
            {
                #region 3D
                //NOTE:Incomplete
                #endregion
            }
            else
            {
                #region 2D
                int Marks = CheckIfSpecialMarksExists(AxisMarks[0], AxisMarks[1]);
                Marks = 2;
                switch (Marks) //X
                {
                    case 0: //Рисуем 0, а потом в обе стороны по 50 пикселей , если нет целых
                        {

                            break;
                        }
                    case 1: //Рисуем целое и потом если не других целых то в обе стороны по 50 пикселей
                        {

                            break;
                        }
                    case 2: //Рисуем по 50 пикселей
                        {
                            int i = 0;
                            while (i<this.Width)
                            {
                                GBMP.DrawString(Math.Round(Shared.NumOnPosition(i, AxisMarks[0], AxisMarks[1], this.Width), 2).ToString(), new Font("Sans Serif", 8), AxisFont, i, Shared.WhereZero(AxisMarks[2], AxisMarks[3], this.Height));
                                i += 50;
                            }
                            break;
                        }
                }
                #endregion
            }
            Redraw();
        }

        private int CheckIfSpecialMarksExists(double Min, double Max)
        {
            if (Min <= 0 && Max >= 0) return 0; //Если там есть 0
            if (Max-Min>=1) return 1; // Если там есть целое
            return 2; //Если ни нуля ни целого
        }

        private void Redraw()
        {
            Monitor.Enter(this);
            GR.DrawImageUnscaled(BMP,0,0);
            Monitor.Exit(this);
        }

        private void GraphForm_Resize(object sender, EventArgs e)
        {
            GR = this.CreateGraphics();
            this.Depth = (this.Width + this.Height)/2; //Я так сказал.
        }

        private void GraphForm_Activated(object sender, EventArgs e)
        {
            Redraw();
        }

        private void GraphForm_MouseUp(object sender, MouseEventArgs e)
        {
            Redraw();
        }

    }
}