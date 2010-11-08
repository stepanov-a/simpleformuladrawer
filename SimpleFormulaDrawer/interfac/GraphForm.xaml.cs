using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ListBox = System.Windows.Controls.ListBox;

namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для GraphForm.xaml
    /// </summary>
    public partial class GraphForm : Window
    {
      
        public GraphForm(double MinX,double MaxX, double MinY,double MaxY, int Quality)
        {
            InitializeComponent();
        }
    }
}
