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

namespace SimpleFormulaDrawer.Core
{
    /// <summary>
    /// Логика взаимодействия для ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        public Color SelectedColor;

        private void SetColor(double OffsetX,double OffsetY)
        {
            var CurColor=new Color();
            if (OffsetX < 0.428571428571429)
            {
                CurColor.R = (byte) (255*(0.428571428571429 - OffsetX)/3*7);
            }
            if (OffsetX > 0.714285714285714)
            {
                CurColor.R = (byte)(255 * (OffsetX - 0.714285714285714) / 2 * 7);
            }

            if (OffsetX <= 0.428571428571429)
            {
                CurColor.G = (byte)(255 * (OffsetX) / 3 * 7);
            }
            if (OffsetX > 0.428571428571429 && OffsetX < 0.714285714285714)
            {
                CurColor.G = (byte)(255 * (0.714285714285714-OffsetX) / 2 * 7);
            }

            if (OffsetX <= 0.714285714285714 && OffsetX > 0.428571428571429)
            {
                CurColor.B = (byte)(255 * (OffsetX - 0.428571428571429) / 2 * 7);
            }
            if (OffsetX > 0.714285714285714)
            {
                CurColor.B = (byte)(255 * (1-OffsetX) / 2 * 7);
            }

            CurColor=Color.Multiply(CurColor, (float) OffsetY);
            SelectedColor = CurColor;
            this.label1.Background = new SolidColorBrush(CurColor);
        }

        private void SpectroCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var P = e.GetPosition(SpectroCanvas);
            SetColor(SpectroCanvas.Width/P.X, SpectroCanvas.Height/P.Y);
        }
    }
}
