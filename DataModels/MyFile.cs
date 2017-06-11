using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataModels
{
    public class MyFile : DiscElement
    {
        private string name;

        public MyFile(string path) : base(path)
        {

        }

        public override DateTime CreationTime
        {
            get
            {
                return File.GetCreationTime(Path);
            }
        }

        public override string Name
        {
            get
            {
                return name = System.IO.Path.GetFileName(Path);
            }
        }

        public string Extension
        {
            get
            {
                return extension = System.IO.Path.GetExtension(Path);
            }
        }

        private string extension;


    }
}
