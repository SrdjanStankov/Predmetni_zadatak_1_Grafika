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
    /// Interaction logic for ElipseWindow.xaml
    /// </summary>
    public partial class ElipseWindow : Window
    {
        private Point startingPoint;

        public ElipseWindow()
        {
            InitializeComponent();
        }

        public ElipseWindow(Point startingPoint) : this()
        {
            this.startingPoint = startingPoint;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
