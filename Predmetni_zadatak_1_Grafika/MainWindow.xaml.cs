using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
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

        private PointCollection points = new PointCollection();

        // name of action that caused it, index of object on canvas, object itself
        private Stack<Tuple<string, int, object>> undoStack = new Stack<Tuple<string, int, object>>();
        private Stack<Tuple<string, int, object>> redoStack = new Stack<Tuple<string, int, object>>();

        public MainWindow()
        {
            InitializeComponent();
            CreateScortcut(Key.Z, ModifierKeys.Control, Undo_Click);
            CreateScortcut(Key.Y, ModifierKeys.Control, Redo_Click);
            CreateScortcut(Key.C, ModifierKeys.Control, Clear_Click);
        }

        void CreateScortcut(Key key, ModifierKeys modifier, ExecutedRoutedEventHandler handler)
        {
            var cmd = new RoutedCommand();
            cmd.InputGestures.Add(new KeyGesture(key, modifier));
            CommandBindings.Add(new CommandBinding(cmd, handler));
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
            points.Clear();
            drawMethod = PolygonSettings;
        }

        private void Image_Checked(object sender, RoutedEventArgs e)
        {
            drawMethod = ImageSettings;
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (undoStack.Count <= 0)
                {
                    return;
                }

                var undoObj = undoStack.Pop();
                switch (undoObj.Item1)
                {
                    case "add":
                        Cnv.Children.RemoveAt(undoObj.Item2);
                        redoStack.Push(new Tuple<string, int, object>("del", undoObj.Item2, undoObj.Item3));
                        break;
                    case "clr":
                        foreach (var item in undoObj.Item3 as List<UIElement>)
                        {
                            Cnv.Children.Add(item);
                        }
                        redoStack.Push(new Tuple<string, int, object>("clr", -1, null));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception) { }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (redoStack.Count <= 0)
                {
                    return;
                }

                var redoObj = redoStack.Pop();
                switch (redoObj.Item1)
                {
                    case "del":
                        Cnv.Children.Insert(redoObj.Item2, redoObj.Item3 as UIElement);
                        undoStack.Push(new Tuple<string, int, object>("add", redoObj.Item2, redoObj.Item3));
                        break;
                    case "clr":
                        undoStack.Push(new Tuple<string, int, object>("clr", -1, (from UIElement item in Cnv.Children select item).ToList()));
                        Cnv.Children.Clear();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception) { }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            undoStack.Push(new Tuple<string, int, object>("clr", -1, (from UIElement item in Cnv.Children select item).ToList()));
            Cnv.Children.Clear();
        }

        private void CanvasLeftMouse_Click(object sender, MouseButtonEventArgs e)
        {
            if (PolyBtn.IsChecked.Value)
            {
                drawMethod(e.GetPosition(Cnv));
                points.Clear();
            }
        }

        private void CanvasRightMouse_Click(object sender, MouseButtonEventArgs e)
        {
            if (PolyBtn.IsChecked.Value)
            {
                points.Add(e.GetPosition(Cnv));
            }
            else
            {
                drawMethod(e.GetPosition(Cnv));
                points.Clear();
            }
        }

        private void ElpiseSettings(Point mousePosition)
        {
            var window = new ElipseWindow()
            {
                Owner = this
            };
            window.ShowDialog();
            var ellipse = window.ResultedEllipse;
            if (ellipse != null)
            {
                ellipse.SetValue(Canvas.LeftProperty, mousePosition.X);
                ellipse.SetValue(Canvas.TopProperty, mousePosition.Y);
                ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                Cnv.Children.Add(ellipse);
                undoStack.Push(new Tuple<string, int, object>("add", Cnv.Children.IndexOf(ellipse), ellipse));
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ellipseClicked = sender as Ellipse;

            var window = new ElipseWindow(ellipseClicked)
            {
                Owner = this
            };
            window.ShowDialog();

            UpdateObjectValues(Cnv.Children.IndexOf(ellipseClicked), window.ResultedEllipse);
            e.Handled = true;
        }

        private void RectangleSettings(Point mousePosition)
        {
            var window = new RectangleWindow()
            {
                Owner = this
            };
            window.ShowDialog();
            var rectangle = window.ResultedRectangle;
            if (rectangle != null)
            {
                rectangle.SetValue(Canvas.LeftProperty, mousePosition.X);
                rectangle.SetValue(Canvas.TopProperty, mousePosition.Y);
                rectangle.MouseLeftButtonUp += Rectangle_MouseLeftButtonUp;
                Cnv.Children.Add(rectangle);
                undoStack.Push(new Tuple<string, int, object>("add", Cnv.Children.IndexOf(rectangle), rectangle));
            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var rectangleClicked = sender as Rectangle;

            var window = new RectangleWindow(rectangleClicked)
            {
                Owner = this
            };
            window.ShowDialog();

            UpdateObjectValues(Cnv.Children.IndexOf(rectangleClicked), window.ResultedRectangle);
            e.Handled = true;
        }

        private void PolygonSettings(Point mousePosition)
        {
            var window = new PolygonWindow(new PointCollection(points))
            {
                Owner = this
            };
            window.ShowDialog();

            var polygon = window.ResultPolygon;
            if (polygon != null)
            {
                polygon.MouseLeftButtonUp += Polygon_MouseLeftButtonUp;
                Cnv.Children.Add(polygon);
                undoStack.Push(new Tuple<string, int, object>("add", Cnv.Children.IndexOf(polygon), polygon));
            }
        }

        private void Polygon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var polygonClicked = sender as Polygon;

            var window = new PolygonWindow(polygonClicked)
            {
                Owner = this
            };
            window.ShowDialog();

            UpdateObjectValues(Cnv.Children.IndexOf(polygonClicked), window.ResultPolygon);
            e.Handled = true;
        }

        private void ImageSettings(Point mousePosition)
        {
            var window = new ImageWindow()
            {
                Owner = this
            };
            window.ShowDialog();

            var image = window.ResultedImage;
            if (image != null)
            {
                image.SetValue(Canvas.LeftProperty, mousePosition.X);
                image.SetValue(Canvas.TopProperty, mousePosition.Y);
                image.MouseLeftButtonUp += Image_MouseLeftButtonUp;
                Cnv.Children.Add(image);
                undoStack.Push(new Tuple<string, int, object>("add", Cnv.Children.IndexOf(image), image));
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var imageClicked = sender as Image;

            var window = new ImageWindow(imageClicked) { Owner = this };
            window.ShowDialog();

            UpdateObjectValues(Cnv.Children.IndexOf(imageClicked), window.ResultedImage);
            e.Handled = true;
        }

        private void UpdateObjectValues(int index, object objectToUpdate)
        {
            var fe = objectToUpdate as FrameworkElement;

            Cnv.Children[index].SetValue(WidthProperty, fe.Width);
            Cnv.Children[index].SetValue(HeightProperty, fe.Height);

            if (objectToUpdate is Shape shape)
            {
                Cnv.Children[index].SetValue(Shape.FillProperty, shape.Fill);
                Cnv.Children[index].SetValue(Shape.StrokeProperty, shape.Stroke);
                Cnv.Children[index].SetValue(Shape.StrokeThicknessProperty, shape.StrokeThickness);
            }
            else if (objectToUpdate is Image img)
            {
                Cnv.Children[index].SetValue(Image.SourceProperty, img.Source);
            }
        }
    }
}
