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
using DataModels;
using System.Diagnostics;

namespace FileManager.Views
{
    /// <summary>
    /// Interaction logic for DiscElementView.xaml
    /// </summary>
    public partial class DiscElementView : UserControl
    {
        DiscElement discElement;

        public DiscElement DiscElement
        {
            get
            {
                return discElement;
            }
        }

        public DiscElementView(DiscElement discElement)
        {
            InitializeComponent();
            name.Text = discElement.Name;
            creationTime.Text = "" + discElement.CreationTime;
            this.discElement = discElement;
        }

        public delegate void OpenDirectoryEvent(DiscElement discElement);
        public event OpenDirectoryEvent openDirectory;

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.DiscElement is MyDirectory)
            {
                if (openDirectory != null)
                {
                    openDirectory.Invoke(this.DiscElement);
                }
            }
            else if (this.DiscElement is MyFile)
            {
                MyFile myFile = (MyFile)this.DiscElement;
                if (myFile.Extension == ".txt")
                {
                    TxtReaderView txtReaderView = new TxtReaderView(myFile);
                    txtReaderView.Show();
                }
                else if (myFile.Extension == ".png" || myFile.Extension == ".jpg" || myFile.Extension == ".jpeg" || myFile.Extension == ".gif")
                {
                    ImageViewerView imageViewerView = new ImageViewerView(myFile);
                    imageViewerView.Show();
                }
                else
                {
                    Process.Start(this.DiscElement.Path);
                }
            }
        }

        public delegate void DeleteDiscElementEvent(DiscElement discElement);
        public event DeleteDiscElementEvent deleteDiscElement;

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (deleteDiscElement != null)
            {
                deleteDiscElement.Invoke(this.DiscElement);
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            grid.Background = Brushes.Aqua;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Background = Brushes.AliceBlue;
        }
    }
}
