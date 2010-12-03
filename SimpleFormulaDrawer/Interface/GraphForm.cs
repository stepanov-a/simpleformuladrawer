using System;
using System.ComponentModel;
using System.Windows.Forms;
using SimpleFormulaDrawer.Core;
using System.Drawing;

namespace SimpleFormulaDrawer.Interface
{
    public class GraphForm:Form
    {
        private Bitmap BMP=new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.WorkingArea.Height); //Картинка - буфер
        private Graphics GR;//Нативная графика окна
        public Graphics GBMP;//Нативная графика картинки. В нее можно рисовать.
        private bool IsNotFirstShow;
        private readonly Pictogramm Source; //Пиктограмма - родитель.

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

        public void Redraw()
        {
            if ((GR != null && BMP != null) && IsNotFirstShow)
            {
                try
                {
                    GR.DrawImage(BMP, 0, 0);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    throw;
                }
            }
        }
    }
}