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

namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для DebugForm.xaml
    /// </summary>
    public partial class DebugForm : Window
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        public void AddMessage(string What)
        {//метод добавления нового элемента. кэп.
            this.listBox1.Items.Add(What);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {//Размеры текстбокса совпадают с размерами формы. кэп.
            this.listBox1.Height=this.Height;
            this.listBox1.Width=this.Width;
        }

    }
}
