using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Predmetni_zadatak_1_Grafika
{
    /// <summary>
    /// Interaction logic for PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        public Polygon ResultPolygon { get; private set; }

        private PointCollection points;

        private const string errorMessage = "{0} is missing!";
        
        public PolygonWindow()
        {
            InitializeComponent();
        }

        public PolygonWindow(Polygon polygon) : this(polygon.Points)
        {
            ResultPolygon = polygon;
            SetWindowProperties(polygon.Fill, polygon.Stroke, polygon.StrokeThickness);
        }

        public PolygonWindow(PointCollection points) : this()
        {
            this.points = points;
        }

        private void SetWindowProperties(Brush fill, Brush stroke, double strokeThickness)
        {
            EFill.SelectedColor = (Color)fill.GetValue(SolidColorBrush.ColorProperty);
            EStroke.SelectedColor = (Color)stroke.GetValue(SolidColorBrush.ColorProperty);
            EStrokeThickness.Value = strokeThickness;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                ResultPolygon = new Polygon()
                {
                    Fill = new SolidColorBrush(EFill.SelectedColor.Value),
                    Stroke = new SolidColorBrush(EStroke.SelectedColor.Value),
                    StrokeThickness = EStrokeThickness.Value.Value,
                    Points = points
                };
                Close();
            }
        }

        private bool IsValid()
        {
            EErrorMsg.Content = string.Empty;

            if (!EStrokeThickness.Value.HasValue)
            {
                FormatErrorMessage("Stroke thickness");
                return false;
            }

            return true;
        }

        private void FormatErrorMessage(string message)
        {
            EErrorMsg.Content = string.Format(errorMessage, message);
        }
    }
}
