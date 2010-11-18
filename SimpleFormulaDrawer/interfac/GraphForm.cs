﻿using System;
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

        #region UserIteractions
        public GraphForm()
        {
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width/5;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width - this.Left;
            this.Activated+=GRActivated;
            this.GBMP = Graphics.FromImage(BMP);
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
    }
}
