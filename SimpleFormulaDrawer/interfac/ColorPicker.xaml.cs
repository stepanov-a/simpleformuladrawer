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

        public Color SelectedColor=Colors.Black;

        public Color InvertedSelectedColor=Colors.White;

        private static Color InvertColor(Color What)
        {
            var R = (byte) (255-What.R);
            var G = (byte) (255-What.G);
            var B = (byte) (255-What.B);
            return Color.FromRgb(R,G,B);
        }

        private void SetColor(double OffsetX,double OffsetY)
        {
            byte R=0, G=0, B=0;

            if (OffsetX>=0 && OffsetX<=0.333333333333)
            {
                R = (byte) (255*OffsetX*3);
                G = (byte) (255*(1/3 - OffsetX)*3);
            }
            if (OffsetX > 0.333333333333 && OffsetX <= 0.66666666666)
            {
                G = (byte)(255 * (2 / 3 - OffsetX) * 3);
                B = (byte)(255 * (OffsetX - 1 / 3) * 3);
            }
            if (OffsetX > 0.66666666666 && OffsetX <= 1)
            {
                R = (byte) (255*(1 - OffsetX)*3);
                B = (byte) (255*(OffsetX - 2/3)*3);
            }
            var CurColor = Color.FromRgb(R, G, B);
            CurColor=Color.Multiply(CurColor, (float) OffsetY);
            SelectedColor = CurColor;
            InvertedSelectedColor = InvertColor(CurColor);
        }

        private void SpectroCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var P = e.GetPosition(this);
            SetColor(P.X/ActualWidth, P.Y/ActualHeight);
        }

        private void SpectroCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var P = e.GetPosition(this);
            SetColor(P.X/ActualWidth, P.Y/ActualHeight);
        }
    }
}