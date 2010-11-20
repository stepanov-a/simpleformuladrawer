using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleFormulaDrawer.Core;
using System.Drawing;

namespace SimpleFormulaDrawer.interfac
{
    public class GraphForm:Form
    {
        private Bitmap BMP=new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.WorkingArea.Height); //Картинка - буфер
        private Graphics GR, GBMP;//Нативная графика окна и картинки соответственно
        private ObjectList[] Points; //Массив графиков.
        #region UserIteractions

        public GraphForm()
        {
            this.InitializeComponent();
            this.Activated+=GRActivated;
            this.GBMP = Graphics.FromImage(BMP);
            this.GR = this.CreateGraphics();
        }

        private void GRActivated(object sender, System.EventArgs e)
        {
            this.Redraw();
        }

        #endregion

        #region NativeFunctions
        private void Redraw()
        {
            if (GR!=null && BMP!=null) GR.DrawImageUnscaled(BMP,0,0);
        }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.StartPosition =FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 5,0);
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width - this.Left,
                                                Screen.PrimaryScreen.WorkingArea.Height);
            this.Name = "GraphForm";
            this.ResumeLayout(false);

        }
    }
}
