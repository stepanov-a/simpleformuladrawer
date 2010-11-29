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
using SimpleFormulaDrawer.interfac;

namespace SimpleFormulaDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    /// 
    /// 
    public partial class MainForm : Window
    {
        public Pictogramm Selected;
        private double PictHeight;//высота пиктограммы. Расчитывается конструктором, чтобы не создавать иконок разных размеров
        //ширина расчитывается динамически, в зависимости от размеров Грида и Листбокса, поэтому хранить ее не надо.
        
        public MainForm()
        {
            InitializeComponent();
            this.FormulListBox1.Items.Clear();//мало ли что.Доверяй, но проверяй.
            this.PictHeight = Height / 25; //25-ЧИИСЛО НА ОДНОЙ СТРАНИЦЫ
            //Расчитывается в конструкторе, чтобы не пересчитывать его, и не делать листбоксы из штук разного размера
            this.XmaxTextbox.Text = "10";
            this.XminTextbox.Text = "-10";
            this.YmaxTextbox.Text = "10";
            this.YminTextbox.Text = "-10";
            this.ZMinTextbox.Text = "-10";
            this.ZMaxTextbox.Text = "10";
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width/5;
            AddPictogramm();//создание первой пиктограммы происходит при запуске приложения
            //во-первых без нее все приложение бессмысленно, а во-вторых - так меньше возни
        }

        private MainFormContent CreateContent()
        {
            return new MainFormContent
                       {
                           MinX = Convert.ToDouble(this.XminTextbox.Text),
                           MaxX = Convert.ToDouble(this.XmaxTextbox.Text),
                           MinY = Convert.ToDouble(this.YminTextbox.Text),
                           MaxY = Convert.ToDouble(this.YmaxTextbox.Text),
                           MinZ = Convert.ToDouble(this.ZMinTextbox.Text),
                           MaxZ = Convert.ToDouble(this.ZMaxTextbox.Text),
                           Quality = this.QualitySlider.Value,
                           Show3DBox = this.Show3DCheckBox.IsChecked.Value,
                           FormulListBox = this.FormulListBox1
                       };
        }

        private void AddPictogramm() //добавляет новую пиктограмму в массив(лист) пиктограмм
        {
#if DEBUG //Иф дебаг!
            MainGrid.ShowGridLines = true; //отображение линий грида. после отладки убрать.
            Forms.DF.AddMessage("Create New Pictogramm/graphform, his number is  "+ArrPictogramm.Count.ToString());//добавление в логи
#endif// енд Иф дебаг!

            //this.ArrPictogramm.Add(new Pictogramm(this.CreateContent()));//констуктор описан в модуле Pictogramm, параметры-диапазон построения, получившаяся пиктограмма добавляется в LIST, описанный выше
            var NewItem = new ListBoxItem {Content = new Pictogramm(this.CreateContent()), Height = this.PictHeight};//созданпие нового айтема для листа, в качестве контента айтема передается последний элемент списка (строчкой выше он заполняется)

#if DEBUG//Иф дебаг!
            Forms.DF.AddMessage(this.PictlistBox.Height.ToString()+"-PicrPistBox Height");
#endif// енд Иф дебаг!

            this.PictlistBox.Items.Add(NewItem);//добавление новой пиктограммы
            this.Selected =(Pictogramm) NewItem.Content;
            bool FullBar = (this.PictlistBox.Items.Count > 23);//вообще было бы неплохо это переписать, 
            //т.к. в случае смены разрешения или размеров формы (ну мало ли) на экране может находиться меньше 23 пиктограмм, 
            //и грид будет сдвигаться с задержкой
            if (FullBar) //если заполнено все видимое пространство листбокса
            {
                int ColIndex = MainGrid.ColumnDefinitions.Count-1;
                Forms.DF.AddMessage(string.Format("{0}ColumnsDef-1", ColIndex));//добавление в логи
                //предыдущие три строчки впринципе можно переписать, заменив ColIndex цифрой2,но пока пускай так будет..
                var myGridLengthConverter = new GridLengthConverter();//Для управления шириной грида ее нужно конвертировать. танцы с бубном вокруг костра
                MainGrid.ColumnDefinitions[ColIndex].Width = (GridLength)myGridLengthConverter.ConvertFromString("4*");//возня с изменением размеров, ничего сексуального
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {//расчет позиции и эффективных размеров формы
            this.Left = 0;
            this.Top = 0;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            //здесь должно быть вычисление пропорций экрана. Если он 4\3, то форма занимает такую-то часть от всего, если нет-другое число
            double width=System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width/6;
            var err = width < this.MinWidth;
            this.Width = err ? this.MinWidth : width;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {//отвечает за симпотичный эффпект появления и исчезновения содержимого текстбоксов с координатами MINIMUM\MAXIMUM X\Y
            if ((sender as TextBox) == null) return;
            if ((sender as TextBox).Text == (sender as TextBox).Tag.ToString())
            {
                (sender as TextBox).Text = "";
            }
        }//отвечает за симпотичный эффпект появления и исчезновения содержимого текстбоксов с координатами MINIMUM\MAXIMUM X\Y

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {//отвечает за симпотичный эффпект появления и исчезновения содержимого текстбоксов с координатами MINIMUM\MAXIMUM X\Y
            if ((sender as TextBox) == null) return;
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = (sender as TextBox).Tag.ToString();
            }
        }//отвечает за симпотичный эффпект появления и исчезновения содержимого текстбоксов с координатами MINIMUM\MAXIMUM X\Y

        private void AddFormul_Click(object sender, RoutedEventArgs e)
        { //добавляет формулу в формуллист. Функция выглядит как-будто ее писали с перепоя, но Павлуша сказал, что так надо :)
            var NewItem = new ListBoxItem //создает новый Айтем
                                       {//задает ему цвета и текст
                                           Foreground = new SolidColorBrush(ColorPicker1.InvertedSelectedColor),
                                           Content = FormulText.Text,
                                           Background = new SolidColorBrush(ColorPicker1.SelectedColor)
                                       };
            if (NewItem.Content.ToString() == "Formula") return;

            var Errors = Selected.AddFunction(NewItem);//ошибки при лдобавлении функции. лично мне реализация не нравится, я туда муть вбивал, видимо это временный вариант
            if (Errors.Count==0)//если ошибок нет
            {
                this.FormulListBox1.Items.Add(NewItem);//добавляем новый айтем в лист
            }
            else //если ошибки есть
            {
                MessageBox.Show("Ошибка");//ругаемся
                Selected.RemoveFunction(NewItem);
                //Тут надо на самом деле подсвечивать начало чего то неправильного и показывать рядом тултип с описанием ошибки.
            }
        }

        private void ButtNewGraphClick(object sender, RoutedEventArgs e)
        {// Обработчик нажатия на кнопку создания нового графика(формы)

            AddPictogramm();//создаем пиктограмму

        }

        private void Debugbutton_Click(object sender, RoutedEventArgs e)
        {//показывает самую страшную формочку, которую когда-либо видел пользователь)
            Forms.DF.Show();
        }

        private void PictlistBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Selected = (Pictogramm) this.FormulListBox1.SelectedItem;
        }
    }
}
