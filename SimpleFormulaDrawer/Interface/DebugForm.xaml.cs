using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public void AddMessage(string What, Color CLR)
        {
            var NewItem=new ListBoxItem
                            {
                                Content = What, Background = new SolidColorBrush(CLR)
                            };
            listBox1.Items.Add(NewItem);
        }

        public void AddMessage(string What)
        {
            listBox1.Items.Add(What);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
