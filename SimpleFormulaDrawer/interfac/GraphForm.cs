using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void GraphForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Parent.ClosePictogramm();
        }

        public void Set3DRendering(bool How)
        {
            if (Is3DRender == How) return;
            Is3DRender = How;
        }
        
        

    }
}