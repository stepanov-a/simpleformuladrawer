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

namespace SimpleFormulaDrawer
{
    /// <summary>
    /// Логика взаимодействия для DebugForm.xaml
    /// </summary>
    public partial class DebugForm : Window
    {
        public DebugForm()
        {
         //   InitializeComponent();
        }
        public void addmessage(string str)
        {
            this.Title = str;
            this.textbox1.text = str;
        }
    }//end debugform class
}
