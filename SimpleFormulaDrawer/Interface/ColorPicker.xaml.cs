using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleFormulaDrawer.Core
{
    /// <summary>
    /// Логика взаимодействия для ColorPicker.xaml
    /// </summary>
    
    public partial class ColorPicker : UserControl
    {
        private double OffsetX, OffsetY;


        public ColorPicker() //Constructor
        {
            InitializeComponent();
            DrawCrest();
        }
        
        public Color SelectedColor=Colors.Black; //DefaultSelectedColor is Black

        public Color InvertedSelectedColor=Colors.White; //Inversion of SelectedColor

        private Line[] TCrest;

        private static Color InvertColor(Color What) //Inversion function
        {
            var R = (byte) (255-What.R);
            var G = (byte) (255-What.G);
            var B = (byte) (255-What.B);
            return Color.FromRgb(R,G,B);
        }

        string ToString(Color What) //For debug function
        {
            return What.R.ToString() + '|' + What.G + '|' + What.B;
        }

        private void DrawCrest()
        {
            if (this.SpectroCanvas.Children.Count == 4)
            {
                var TRSF = new TranslateTransform(this.ActualWidth * OffsetX, this.ActualHeight * OffsetY);
                this.SpectroCanvas.Children[0].RenderTransform = TRSF;
                this.SpectroCanvas.Children[1].RenderTransform = TRSF;
                this.SpectroCanvas.Children[2].RenderTransform = TRSF;
                this.SpectroCanvas.Children[3].RenderTransform = TRSF;
            }
            else
            {
                TCrest = Crest.CrestToPoint(0,0);
                this.SpectroCanvas.Children.Clear();
                this.SpectroCanvas.Children.Add(TCrest[0]);
                this.SpectroCanvas.Children.Add(TCrest[1]);
                this.SpectroCanvas.Children.Add(TCrest[2]);
                this.SpectroCanvas.Children.Add(TCrest[3]);
            }
        }

        private void SetColor() //We can't get color under mouse. so we'll calcuate it!
        {
            byte R=0, G=0, B=0; //RGB

            if (OffsetX>=0 && OffsetX<=0.333333333333) //First triade (R to G)
            {
                R = (byte)(255 * (1 / 3 - OffsetX) * 3); //Simple math
                G = (byte) (255*(OffsetX)*3);
            }
            if (OffsetX > 0.333333333333 && OffsetX <= 0.66666666666) //Second triade (G to B)
            {
                G = (byte)(255 * (2 / 3 - OffsetX) * 3);//Simple math
                B = (byte)(255 * (OffsetX - 1 / 3) * 3);
            }
            if (OffsetX > 0.66666666666 && OffsetX <= 1) //Third triade (B to R)
            {
                R = (byte)(255 * (OffsetX - 2 / 3) * 3);//Simple math
                B = (byte) (255*(1-OffsetX)*3);
            }

            var CurColor = Color.FromRgb(R, G, B);
            CurColor=Color.Multiply(CurColor, (float) OffsetY); //UpDown gradient
            SelectedColor = CurColor;
            InvertedSelectedColor = InvertColor(CurColor);
#if DEBUG //Default VS Constant. Can be Debug or none.
            Forms.DF.AddMessage(ColorToString(CurColor),CurColor); //Debug
#endif
        }

        private void SpectroCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        { //OnMouseDownEvent. We need to set colors on clicked.
            var P = e.GetPosition(this);
            OffsetX = P.X/ActualWidth;
            OffsetY = P.Y/ActualHeight;
            SetColor();
            DrawCrest();
        }

        private void SpectroCanvas_MouseMove(object sender, MouseEventArgs e)
        { //OnMouseMoveEvent. We need to set colors on undermouse color.
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var P = e.GetPosition(this);
            OffsetX = P.X / ActualWidth;
            OffsetY = P.Y / ActualHeight;
            SetColor();
            DrawCrest();
        }

        private void SpectroCanvas_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            DrawCrest();
        }
    }
}