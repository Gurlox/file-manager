using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataModels
{
    public class MyDirectory : DiscElement
    {
        public MyDirectory(string dirPath) : base(dirPath)
        {

        }

        public override DateTime CreationTime
        {
            get
            {

                return Directory.GetCreationTime(Path);
            }
        }

        public override string Name
        {
            get
            {
                return System.IO.Path.GetFileName(Path);
            }
        }

        /// <summary>
        /// Metoda zwracajaca (posortowane) pliki i foldery
        /// </summary>
        /// <returns></returns>
        public List<DiscElement> ListDiscElements(string sortType = null)
        {
            List<DiscElement> result = new List<DiscElement>();
            result.AddRange(GetAllSubDirectoriesAndSort(sortType));
            result.AddRange(GetAllFilesAndSort(sortType));

            return result;
        }

        /// <summary>
        /// Metoda zwracajaca (posortowane) pliki
        /// </summary>
        /// <returns></returns>
        public List<MyFile> GetAllFilesAndSort(string sortType = null)
        {
            string[] files = Directory.GetFiles(Path);

            List<MyFile> filesList = new List<MyFile>();

            foreach (string filePath in files)
            {
                filesList.Add(new MyFile(filePath));
            }

            if (sortType == "name")
            {
                return filesList.OrderByDescending(o => o.Name).ToList();
            }
            else if (sortType == "creationTime")
            {
                return filesList.OrderBy(o => o.CreationTime).ToList();
            }
            else
            {
                return filesList;
            }
        }

        /// <summary>
        /// Metoda zwracajaca (posortowane) foldery
        /// </summary>
        /// <returns></returns>
        public List<MyDirectory> GetAllSubDirectoriesAndSort(string sortType = null)
        {
            string[] directories = Directory.GetDirectories(Path);

            List<MyDirectory> directoriesList = new List<MyDirectory>();

            foreach (string folderPath in directories)
            {
                directoriesList.Add(new MyDirectory(folderPath));
            }

            if (sortType == "name")
            {
                return directoriesList.OrderByDescending(o => o.Name).ToList();
            }
            else if (sortType == "creationTime")
            {
                return directoriesList.OrderBy(o => o.CreationTime).ToList();
            }
            else
            {
                return directoriesList;
            }
        }
    }
}