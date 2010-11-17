using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SimpleFormulaDrawer.interfac;
using SimpleFormulaDrawer.Core;

namespace SimpleFormulaDrawer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Forms.MF = new MainForm();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = Forms.MF;
            Forms.MF.Show();
        }
    }
}
