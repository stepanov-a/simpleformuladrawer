using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms; //для скрина
namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            int Wsize= System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width;//ширина 
            int Hsize = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Height;//высота. надо.
            this.Left = 5;
            this.Top = 5;
            this.Height = Hsize-this.Top;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
  /*          this.progressBar1.Width = this.Width;
            this.listBox1.Width = this.Width;
            double heiff = (double) (this.Height/3);
            heiff = Math.Round(heiff);
            int H = (int) heiff;
            this.listBox1.Height = H;*/
          //  this.progressBar1.top=this.Top; 

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
}
