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
    /// Interaction logic for TxtReaderView.xaml
    /// </summary>
    public partial class TxtReaderView : Window
    {
        MyFile myFile;

        public TxtReaderView(MyFile myFile)
        {
            InitializeComponent();
            this.Title = myFile.Name;
            this.myFile = myFile;

            FileReader();
        }

        /// <summary>
        /// Metoda, która czyta całą zawartość pliku tekstowego, którą następnie wypisuje na ekranie
        /// </summary>
        /// <returns></returns>
        private void FileReader()
        {
            using (StreamReader sr = new StreamReader(this.myFile.Path))
            {
                string line = sr.ReadToEnd();
                content.Text = line;
            }
        }
    }
}
