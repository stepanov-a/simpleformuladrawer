using System.Windows;
using Forms = SimpleFormulaDrawer.Core.Forms;
namespace SimpleFormulaDrawer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.MainWindow = Forms.MF;
            Forms.MF.Show();
        }
    }
}
