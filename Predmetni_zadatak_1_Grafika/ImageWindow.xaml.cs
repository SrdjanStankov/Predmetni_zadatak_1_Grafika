using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using Microsoft.Win32;

namespace Predmetni_zadatak_1_Grafika
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        public Image ResultedImage { get; private set; }
        
        private ImageSource imageSource;
        private const string errorMessage = "{0} is missing!";
        
        public ImageWindow()
        {
            InitializeComponent();
        }

        public ImageWindow(Image imageClicked) : this()
        {
            ResultedImage = imageClicked;

            EWidth.Value = ResultedImage.Width;
            EHeight.Value = ResultedImage.Height;
            imageSource = ResultedImage.Source;
            ImgUri.Text = ResultedImage.Source.ToString().Split('\\').LastOrDefault();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                ResultedImage = new Image()
                {
                    Source = imageSource,
                    Width = EWidth.Value.Value,
                    Height = EHeight.Value.Value
                };
                Close();
            }
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Images |*.png;*.jpg;*.bmp;*.gif;*.tif;*.wmp;*.ico",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Title = "Odaberi sliku",
            };

            if (fileDialog.ShowDialog(this) == true)
            {
                string safeFileName = fileDialog.FileName;
                var uriSource = new Uri(safeFileName);
                imageSource = new BitmapImage(uriSource);
                ImgUri.Text = fileDialog.SafeFileName;
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

            if (imageSource == null)
            {
                FormatErrorMessage("Image");
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
