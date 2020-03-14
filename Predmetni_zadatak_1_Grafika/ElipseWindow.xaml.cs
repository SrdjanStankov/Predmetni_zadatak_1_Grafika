using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Interaction logic for ElipseWindow.xaml
    /// </summary>
    public partial class ElipseWindow : Window
    {
        public Ellipse ResultedEllipse { get; set; }

        private const string errorMessage = "{0} is missing!";

        public ElipseWindow()
        {
            InitializeComponent();
        }

        private void SetWindowProperties(double width, double height, Brush fill, Brush stroke, double strokeThickness)
        {
            EWidth.Value = width;
            EHeight.Value = height;
            EFill.SelectedColor = (Color)fill.GetValue(SolidColorBrush.ColorProperty);
            EStroke.SelectedColor = (Color)stroke.GetValue(SolidColorBrush.ColorProperty);
            EStrokeThickness.Value = strokeThickness;
        }

        public ElipseWindow(Ellipse ellipse) : this()
        {
            ResultedEllipse = ellipse;
            SetWindowProperties(ellipse.Width, ellipse.Height, ellipse.Fill, ellipse.Stroke, ellipse.StrokeThickness);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                ResultedEllipse = new Ellipse()
                {
                    Width = EWidth.Value.Value,
                    Height = EHeight.Value.Value,
                    Fill = new SolidColorBrush(EFill.SelectedColor.Value),
                    Stroke = new SolidColorBrush(EStroke.SelectedColor.Value),
                    StrokeThickness = EStrokeThickness.Value.Value
                };
                Close();
            }
        }

        private bool IsValid()
        {
            EErrorMsg.Content = string.Empty;
            if (!EWidth.Value.HasValue)
            {
                FormatErrorMessage("Width");
                return false;
            }

            if (!EHeight.Value.HasValue)
            {
                FormatErrorMessage("Height");
                return false;
            }

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
