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
using DataModels;
using System.IO;

namespace FileManager.Views
{
    /// <summary>
    /// Interaction logic for ImageViewerView.xaml
    /// </summary>
    public partial class ImageViewerView : Window
    {
        public ImageViewerView(MyFile myFile)
        {
            InitializeComponent();

            this.Title = myFile.Name;
            image.Source = new BitmapImage(new Uri(myFile.Path));
        }
    }
}
