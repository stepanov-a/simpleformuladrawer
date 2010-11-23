using System;
using System.ComponentModel;
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
        private bool IsNotFirstShow = false;
        private Pictogramm Source; //Пиктограмма - родитель.

        #region UserIteractions

        public GraphForm(Pictogramm Parent)
        {
            this.InitializeComponent();
            this.Shown += FShown;
            this.Closing += FClosing;
            this.Activated += FActivate;
            this.GBMP = Graphics.FromImage(BMP);
            this.GR = this.CreateGraphics();
            this.ShowInTaskbar = false;
            this.Source = Parent;
        }

        private void FActivate(object sender, EventArgs e)
        {
            if (IsNotFirstShow) Forms.MF.Selected = Source;
        }

        private void FShown(object sender, EventArgs e)
        {
            IsNotFirstShow = true;
        }

        private void FClosing(object sender, CancelEventArgs e)
        {
            Source.ClosePictogramm();
            IsNotFirstShow = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 5, 0);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - this.Left,
                                                Screen.PrimaryScreen.WorkingArea.Height);
            this.Name = "Форма графиков";
            this.ResumeLayout(false);
        }

        #endregion

        #region NativeFunctions

        private void Redraw()
        {
            if (GR!=null && BMP!=null && IsNotFirstShow) GR.DrawImageUnscaled(BMP,0,0);
        }

        #endregion

        #region Drawing functions

        public void FlushBuffer()
        {
            this.GBMP.Clear(Color.White);
        }

        public int AddGraphic()
        {
            throw new NotImplementedException("AddGraphic");
        }

        public int AddGraphic(ObjectList Graph)
        {
            throw new NotImplementedException("AddGraphic");
        }

        public void AddNextPoint(ObjectList Pnt)
        {
            throw new NotImplementedException("AddNextPoint");
        }
        #endregion
    }
}
