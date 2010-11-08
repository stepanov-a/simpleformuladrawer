using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleFormulaDrawer.interfac
{
    public partial class GraphForm : Form
    {
        public GraphForm(double Minx,double Maxx,double Miny,double Maxy,int Quality)
        {
            InitializeComponent();
        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width/5;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width/5*4;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
    }
}
