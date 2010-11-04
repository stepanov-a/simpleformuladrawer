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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleFormulaDrawer.Core;
using SimpleFormulaDrawer.interfac;

namespace SimpleFormulaDrawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var Main = new MainForm();
            LogManager.Init("debug.log", "error.log");
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = Main;
            Main.Show();
            this.Hide();
            Forms.StartupWindow = this;
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var LMGR=new LibraryManager();
            LMGR.AddFunction("x^x+5SIN(x)-5^x");
            LMGR.AddFunction("x*y");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteDebug("GAY");
            Forms.DF.Show();
        }

   

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ConfigurationSystem.ReadConfig("CONFIG_FORM_THEME", "Default"));
        }
    }
}
