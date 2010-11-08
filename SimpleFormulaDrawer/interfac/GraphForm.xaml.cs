using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ListBox = System.Windows.Controls.ListBox;

namespace SimpleFormulaDrawer.interfac
{
    /// <summary>
    /// Логика взаимодействия для GraphForm.xaml
    /// </summary>
    public partial class GraphForm : Window
    {
      
        public GraphForm(double MinX,double MaxX, double MinY,double MaxY, int Quality)
        {
            InitializeComponent();
            //вычисление позиции. Позиция ставится так, что ГрафФорм располагается справа от МайнФорм
            int ScreenActiveWidth = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width;
            int ScreenActiveHeight = System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Height;
            int MainWsize = ScreenActiveWidth / 5;//ширина 
            int MainHsize = ScreenActiveHeight;//высота. надо.
            this.Left = 5+5+MainWsize;
            this.Top = 5;
            this.Height = ScreenActiveHeight-5;
            this.Width = ScreenActiveWidth - MainWsize-5-5;
          // +\- 5 -для грамотного позиционирвоания с учетом бордюров
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
