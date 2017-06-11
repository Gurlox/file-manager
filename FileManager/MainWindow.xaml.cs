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
using System.Windows.Threading;
using System.IO;
using DataModels;
using FileManager.Views;

namespace FileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDirectory currentDirectoryLeft;
        MyDirectory currentDirectoryRight;

        Stack<String> backStackLeft = new Stack<String>();
        Stack<String> backStackRight = new Stack<String>();

        public MainWindow()
        {
            InitializeComponent();
            GetAllDrives();
        }

        /// <summary>
        /// Metoda pobierająca wszystkie dyski znajdujące się na komputerze lokalnym
        /// </summary>
        /// <returns></returns>
        private void GetAllDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                discLeft.Items.Add(drive.Name);
                discRight.Items.Add(drive.Name);
            }

            InitializeStartDrive(drives[0].Name);
        }

        /// <summary>
        /// Metoda wyświetlająca listę dysków
        /// </summary>
        /// <returns></returns>
        private void InitializeStartDrive(string driveName)
        {
            RefreshLeftList(driveName);
            discLeft.Text = driveName;
            RefreshRightList(driveName);
            discRight.Text = driveName;
        }

        private void RefreshLeftList(string path, string sortType = null)
        {
            try
            {
                if (this.backStackLeft.Count == 0)
                {
                    backLeft.IsEnabled = false;
                }
                else
                {
                    backLeft.IsEnabled = true;
                }
                MyDirectory myDirectory = new MyDirectory(path);
                this.currentDirectoryLeft = myDirectory;
                leftList.Children.Clear();

                List<DiscElement> discElementsList = myDirectory.ListDiscElements(sortType);

                foreach (DiscElement discElement in discElementsList)
                {
                    DiscElementView discElementView = new DiscElementView(discElement);

                    leftList.Children.Add(discElementView);

                    discElementView.openDirectory += OpenLeftDirectory;
                    discElementView.deleteDiscElement += DeleteDiscElementLeft;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void RefreshRightList(string path, string sortType = null)
        {
            try
            {
                if (this.backStackRight.Count == 0)
                {
                    backRight.IsEnabled = false;
                }
                else
                {
                    backRight.IsEnabled = true;
                }
                MyDirectory myDirectory = new MyDirectory(path);
                this.currentDirectoryRight = myDirectory;
                rightList.Children.Clear();

                List<DiscElement> discElementsList = myDirectory.ListDiscElements(sortType);

                foreach (DiscElement discElement in discElementsList)
                {
                    DiscElementView discElementView = new DiscElementView(discElement);

                    rightList.Children.Add(discElementView);

                    discElementView.openDirectory += OpenRightDirectory;
                    discElementView.deleteDiscElement += DeleteDiscElementRight;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteDiscElementLeft(DiscElement discElement)
        {
            try
            {
                if (discElement is MyFile)
                {
                    File.Delete(discElement.Path);
                    RefreshLeftList(currentDirectoryLeft.Path);
                }
                else if (discElement is MyDirectory)
                {
                    Directory.Delete(discElement.Path, true);
                    RefreshLeftList(currentDirectoryLeft.Path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteDiscElementRight(DiscElement discElement)
        {
            try
            {
                if (discElement is MyFile)
                {
                    File.Delete(discElement.Path);
                    RefreshRightList(currentDirectoryRight.Path);
                }
                else if (discElement is MyDirectory)
                {
                    Directory.Delete(discElement.Path, true);
                    RefreshRightList(currentDirectoryRight.Path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OpenLeftDirectory(DiscElement discElement)
        {
            this.backStackLeft.Push(currentDirectoryLeft.Path);
            RefreshLeftList(discElement.Path);
        }

        private void OpenRightDirectory(DiscElement discElement)
        {
            this.backStackRight.Push(currentDirectoryRight.Path);
            RefreshRightList(discElement.Path);
        }

        private void discLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.backStackLeft.Clear();
            RefreshLeftList(discLeft.SelectedItem.ToString());
        }

        private void discRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.backStackRight.Clear();
            RefreshRightList(discRight.SelectedItem.ToString());
        }

        private void sortByNameLeft_Click(object sender, RoutedEventArgs e)
        {
            RefreshLeftList(this.currentDirectoryLeft.Path, "name");
        }

        private void sortByCreationTimeLeft_Click(object sender, RoutedEventArgs e)
        {
            RefreshLeftList(this.currentDirectoryLeft.Path, "creationTime");
        }

        private void sortByNameRight_Click(object sender, RoutedEventArgs e)
        {
            RefreshRightList(this.currentDirectoryRight.Path, "name");
        }

        private void sortByCreationTimeRight_Click(object sender, RoutedEventArgs e)
        {
            RefreshRightList(this.currentDirectoryRight.Path, "creationTime");
        }

        private void backRight_Click(object sender, RoutedEventArgs e)
        {
            RefreshRightList(this.backStackRight.Pop());
        }

        private void backLeft_Click(object sender, RoutedEventArgs e)
        {
            RefreshLeftList(this.backStackLeft.Pop());
        }

        private void createDirectoryLeft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(currentDirectoryLeft.Path + "/" + newLeftDirectoryName.Text);
                RefreshLeftList(this.currentDirectoryLeft.Path);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void createDirectoryRight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(currentDirectoryLeft.Path + "/" + newRightDirectoryName.Text);
                RefreshRightList(this.currentDirectoryRight.Path);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Metoda kopiująca wszystkie zaznaczone elementy z lewej listy na prawą
        /// </summary>
        /// <returns></returns>
        private void copyLeft_Click(object sender, RoutedEventArgs e)
        {
            foreach (DiscElementView discElementView in leftList.Children)
            {
                DiscElement discElement = discElementView.DiscElement;

                if (discElementView.checkBox.IsChecked == true)
                {
                    try
                    {
                        File.Copy(discElement.Path, this.currentDirectoryRight.Path + "/" + discElement.Name);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
            RefreshRightList(this.currentDirectoryRight.Path);
        }

        /// <summary>
        /// Metoda kopiująca wszystkie zaznaczone elementy z prawej listy na lewą
        /// </summary>
        /// <returns></returns>
        private void copyRight_Click(object sender, RoutedEventArgs e)
        {
            foreach (DiscElementView discElementView in rightList.Children)
            {
                DiscElement discElement = discElementView.DiscElement;

                if (discElementView.checkBox.IsChecked == true)
                {
                    try
                    {
                        File.Copy(discElement.Path, this.currentDirectoryLeft.Path + "/" + discElement.Name);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
            RefreshLeftList(this.currentDirectoryLeft.Path);
        }

        /// <summary>
        /// Metoda wyszukująca pliki w lewej liście
        /// </summary>
        /// <returns></returns>
        private void searchLeft_Click(object sender, RoutedEventArgs e)
        {
            List<MyFile> filesList = this.currentDirectoryLeft.GetAllFilesAndSort();

            leftList.Children.Clear();

            foreach (MyFile file in filesList)
            {
                if (file.Name.StartsWith(searchValueLeft.Text))
                {
                    DiscElementView discElementView = new DiscElementView(file);

                    leftList.Children.Add(discElementView);

                    discElementView.deleteDiscElement += DeleteDiscElementRight;
                }
            }
        }

        /// <summary>
        /// Metoda wyszukująca pliki w prawej liście
        /// </summary>
        /// <returns></returns>
        private void searchRight_Click(object sender, RoutedEventArgs e)
        {
            List<MyFile> filesList = this.currentDirectoryRight.GetAllFilesAndSort();

            rightList.Children.Clear();

            foreach (MyFile file in filesList)
            {
                if (file.Name.StartsWith(searchValueRight.Text))
                {
                    DiscElementView discElementView = new DiscElementView(file);

                    rightList.Children.Add(discElementView);

                    discElementView.deleteDiscElement += DeleteDiscElementRight;
                }
            }
        }
    }
}
