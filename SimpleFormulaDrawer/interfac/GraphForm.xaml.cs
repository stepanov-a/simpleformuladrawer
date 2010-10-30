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
      
        public GraphForm()
        {

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            image1.Width = this.Width;
            image1.Height = this.Height;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }
    }
}
