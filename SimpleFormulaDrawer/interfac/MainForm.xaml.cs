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

namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    /// 
    /// 
    public partial class MainForm : Window
    {
        public List<Pictogramm> ArrPictogramm = new List<Pictogramm>();
        private int SelectedPictogram=0; //Текущая выбранная пиктограмма.
        private double PictWidth;
        private double PictHeight;

        public MainForm()
        {
            InitializeComponent();
            this.FormulListBox1.Items.Clear();
            this.FormulListBox1.Items.Clear();
            this.PictHeight = Height / 25; //25-ЧИИСЛО НА ОДНОЙ СТРАНИЦЫ
            //Расчитывается в конструкторе, чтобы не пересчитывать его, и не делать листбоксы из штук разного размера

            AddPictogramm();
        }


        private void AddPictogramm() //добавляет новую пиктограмму в массив(лист) пиктограмм
        {
#if DEBUG
            MainGrid.ShowGridLines = true; //отображение линий грида. после отладки убрать.
            Forms.DF.AddMessage("Create New Pictogramm/graphform, his number is  "+ArrPictogramm.Count.ToString());//добавление в логи
#endif
            this.ArrPictogramm.Add(new Pictogramm(-10, 10, -10, 10, 10));
            var NewItem = new ListBoxItem { Content = this.ArrPictogramm.Last() };
#if DEBUG
            Forms.DF.AddMessage(this.PictlistBox.Height.ToString()+"-PicrPistBox Height");
#endif
            NewItem.Height = this.PictHeight;
            this.PictlistBox.Items.Add(NewItem);
            bool FullBar = (this.ArrPictogramm.Count > 23);
            if (FullBar)
            {
                int ColIndex;
                ColIndex = MainGrid.ColumnDefinitions.Count-1 ;
                Forms.DF.AddMessage(ColIndex.ToString()+"ColumnsDef-1");//добавление в логи
                GridLengthConverter myGridLengthConverter = new GridLengthConverter();//Для управления шириной грида ее нужно конвертировать. танцы с бубном вокруг костра
                MainGrid.ColumnDefinitions[ColIndex].Width = (GridLength)myGridLengthConverter.ConvertFromString("4*");
            }
            
         //   Core.Forms.DF.AddMessage(GridRow.ToString()+"-GridRow");
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width =System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width/5;
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
            ListBoxItem NewItem = new ListBoxItem
                                       {
                                           Foreground = new SolidColorBrush(ColorPicker1.InvertedSelectedColor),
                                           Content = FormulText.Text,
                                           Background = new SolidColorBrush(ColorPicker1.SelectedColor)
                                       },
                        TempNewItem = new ListBoxItem
                                          {
                                              Foreground = new SolidColorBrush(ColorPicker1.InvertedSelectedColor),
                                              Content = FormulText.Text,
                                              Background = new SolidColorBrush(ColorPicker1.SelectedColor)
                                          };
            if (NewItem.Content.ToString() == "Formula") return;

            var Errors=this.ArrPictogramm[SelectedPictogram].AddFunction(NewItem);
            if (Errors.Count==0)
            {
                this.FormulListBox1.Items.Add(TempNewItem);
            }
            else
            {
                MessageBox.Show("Ошибка");
                this.ArrPictogramm[SelectedPictogram].RemoveFunction(NewItem);
                //Тут надо на самом деле подсвечивать начало чего то неправильного и показывать рядом тултип с описанием ошибки.
            }
        }

        private void ButtNewGraphClick(object sender, RoutedEventArgs e)
        {

            AddPictogramm();
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
