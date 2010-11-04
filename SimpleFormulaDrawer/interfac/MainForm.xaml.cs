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
using SimpleFormulaDrawer.Core;
using System.CodeDom.Compiler;

namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    /// 
    /// 
    public partial class MainForm : Window
    {
        private Pictogramm[] ArrPictogramm;
        private Int64 CountPictogramm; //Нранит индекс последнго элемента в ArrPicrogramm

        public MainForm()
        {
            InitializeComponent();
            this.FormulListBox1.Items.Clear();
            Boolean FLAG;
            double countxmin = -1;
            double countxmax = 1;
            double countymin = -1;
            double countymax = 1;
            this.CountPictogramm = 0;
            //   this.ArrPictogramm[0] = new Pictogramm(countxmin, countymin, countxmax, countymax,this.FormulListBox1);
        }
     
        //добавляет в массив из пиктограммок новый элемент.
      //  первый (нулевой) элемент добавляется при создании MainForm из конструктора MainForm
            private void AddPictogramm()
        {
        //сначала выполняется проверка верности значений в текстбоксах, потом вызывается
        //конструктор 
          //      this.ArrPictogramm[CountPictogramm+1]=new Pictogramm();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            int Wsize = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width / 5;//ширина 
            int Hsize = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Height;//высота. надо.
            this.Left = 5;
            this.Top = 5;
            this.Height = Hsize - this.Top;
            this.Width = Wsize;
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
            ListBoxItem NewItemm = new ListBoxItem
                                       {
                                           Foreground = new SolidColorBrush(ColorPicker1.InvertedSelectedColor),
                                           Content = FormulText.Text,
                                           Background = new SolidColorBrush(ColorPicker1.SelectedColor)
                                       };
            if (NewItemm.Content.ToString() == "Formula") return;
            /*var Errors = (NewItemm.Content.ToString());}
            if (Errors.Count==0)
            {
                this.FormulListBox1.Items.Add(NewItemm);
            }
            else
            {
                MessageBox.Show("Ошибка");
                //Тут надо на самом деле подсвечивать начало чего то неправильного и показывать рядом тултип с описанием ошибки.
            }*/
        }

        private void ButtNewGraphClick(object sender, RoutedEventArgs e)
        {
        /*    GraphForm Graph = new GraphForm();
            Graph.Top = this.Top;
            Graph.Left = this.Left + this.Width + 5; //позиция нвоой граф.формы слева =сумме начальной позиции  главной формы и ее ширины +5 -отступ
            Graph.Height = this.Height; //высота графа равна высоте MainForm
            Graph.Width = Screen.AllScreens[0].WorkingArea.Width - this.Width - this.Left - 5;//Ширина Графа =ширине экрана за вычетом положения и размера главной формы
            Graph.Show();*/
        }

        private void Debugbutton_Click(object sender, RoutedEventArgs e)
        {
            Forms.StartupWindow.Show();
        }
    }
}
