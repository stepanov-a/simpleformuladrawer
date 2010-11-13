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

namespace SimpleFormulaDrawer.interfac
{
    public partial class GraphForm : Form
    {
        private bool Is3DRender;
        private Pictogramm Parent;
        public int FormState = 0;
        private Pen AxisColor = new Pen(Color.Black);
        private Graphics GR;
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
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.GR = this.CreateGraphics();
        }

        private void GraphForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Parent.ClosePictogramm();
        }

        public void Set3DRendering(bool How)
        {
            Is3DRender = How;
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
            if (Is3DRender)
            {
                
            }
            else
            {
                GR.DrawLine(AxisColor, 0, ZeroY, this.Width, ZeroY);
                GR.DrawLine(AxisColor,ZeroX,0,ZeroX,this.Height);
            }
        }

        private void GraphForm_Resize(object sender, EventArgs e)
        {
            GR = this.CreateGraphics();
            this.Depth = (this.Width + this.Height)/2; //Я так сказал.
        }

    }
}