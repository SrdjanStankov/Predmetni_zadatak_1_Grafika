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
        private Action drawMethod;

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
            MessageBox.Show($"{e.GetPosition(Cnv)}");
        }

        private void CanvasRightMouse_Click(object sender, MouseButtonEventArgs e)
        {
            drawMethod();
            MessageBox.Show($"{e.GetPosition(Cnv)}");
        }

        private void ElpiseSettings()
        {
            MessageBox.Show("Ellipse settings");
        }
        
        private void RectangleSettings()
        {
            MessageBox.Show("Rectangle settings");
        }
        
        private void PolygonSettings()
        {
            throw new NotImplementedException();
        }

        private void ImageSettings()
        {
            throw new NotImplementedException();
        }
    }
}
