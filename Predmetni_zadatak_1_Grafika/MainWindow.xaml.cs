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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Predmetni_zadatak_1_Grafika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Action<Point> drawMethod;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Elipse_Checked(object sender, RoutedEventArgs e)
        {
            drawMethod = ElpiseSettings;
        }

        private void Rectangle_Checked(object sender, RoutedEventArgs e)
        {
            drawMethod = RectangleSettings;
        }

        private void Polygon_Checked(object sender, RoutedEventArgs e)
        {
            drawMethod = PolygonSettings;
        }

        private void Image_Checked(object sender, RoutedEventArgs e)
        {
            drawMethod = ImageSettings;
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Cnv.Children.Clear();
        }

        private void CanvasLeftMouse_Click(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void CanvasRightMouse_Click(object sender, MouseButtonEventArgs e)
        {
            drawMethod(e.GetPosition(Cnv));
        }

        private void ElpiseSettings(Point mousePosition)
        {
            var window = new ElipseWindow();
            window.ShowDialog();
            var ellipse = window.ResultedEllipse;
            if (ellipse != null)
            {
                ellipse.SetValue(Canvas.LeftProperty, mousePosition.X);
                ellipse.SetValue(Canvas.TopProperty, mousePosition.Y);
                ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                Cnv.Children.Add(ellipse); 
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ellipseClicked = sender as Ellipse;
            var canvasLeft = ellipseClicked.GetValue(Canvas.LeftProperty);
            var canvasTop = ellipseClicked.GetValue(Canvas.TopProperty);

            var window = new ElipseWindow(ellipseClicked);
            window.ShowDialog();

            var index = Cnv.Children.IndexOf(ellipseClicked);
            var ellipse = window.ResultedEllipse;
            ellipse.SetValue(Canvas.LeftProperty, canvasLeft);
            ellipse.SetValue(Canvas.TopProperty, canvasTop);
            ellipse.MouseLeftButtonUp -= Ellipse_MouseLeftButtonUp;
            ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;

            Cnv.Children.RemoveAt(index);
            Cnv.Children.Insert(index, ellipse);
        }

        private void RectangleSettings(Point mousePosition)
        {
            var window = new RectangleWindow();
            window.ShowDialog();
            var rectangle = window.ResultedRectangle;
            if (rectangle != null)
            {
                rectangle.SetValue(Canvas.LeftProperty, mousePosition.X);
                rectangle.SetValue(Canvas.TopProperty, mousePosition.Y);
                rectangle.MouseLeftButtonUp += Rectangle_MouseLeftButtonUp;
                Cnv.Children.Add(rectangle);
            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var rectangleClicked = sender as Rectangle;
            var canvasLeft = rectangleClicked.GetValue(Canvas.LeftProperty);
            var canvasTop = rectangleClicked.GetValue(Canvas.TopProperty);

            var window = new RectangleWindow(rectangleClicked);
            window.ShowDialog();

            var index = Cnv.Children.IndexOf(rectangleClicked);
            var rectangle = window.ResultedRectangle;
            rectangle.SetValue(Canvas.LeftProperty, canvasLeft);
            rectangle.SetValue(Canvas.TopProperty, canvasTop);
            rectangle.MouseLeftButtonUp -= Rectangle_MouseLeftButtonUp;
            rectangle.MouseLeftButtonUp += Rectangle_MouseLeftButtonUp;

            Cnv.Children.RemoveAt(index);
            Cnv.Children.Insert(index, rectangle);
        }

        private void PolygonSettings(Point mousePosition)
        {
            throw new NotImplementedException();
        }

        private void ImageSettings(Point mousePosition)
        {
            throw new NotImplementedException();
        }
    }
}
