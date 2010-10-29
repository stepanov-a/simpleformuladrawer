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
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

//для скрина

namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        public class GraphArray
        {
        
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            var Wsize= Screen.AllScreens[0].WorkingArea.Width /5;//ширина 
            var Hsize = Screen.AllScreens[0].WorkingArea.Height;//высота. надо.
            Left = 5;
            Top = 5;
            Height = Hsize-Top;
            Width = Wsize;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
  /*          this.progressBar1.Width = this.Width;
            this.listBox1.Width = this.Width;
            double heiff = (double) (this.Height/3);
            heiff = Math.Round(heiff);
            int H = (int) heiff;
            this.listBox1.Height = H;
          //  this.progressBar1.top=this.Top; */

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox) == null) return;
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
            {
                (sender as TextBox).Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox) == null) return;
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
            }
        }

        private void AddFormul_Click(object sender, RoutedEventArgs e)
        {
            string formule = this.FormulText.Text;
            this.FormulListBox1.Items.Add(formule);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

   
}
