using System.Windows;
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
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Current.MainWindow = Forms.MF;
            Forms.MF.Show();
        }
    }
}
