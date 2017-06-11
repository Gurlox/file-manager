using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public abstract class DiscElement
    {
        string path;

        public DiscElement(string path)
        {
            this.path = path;
        }

        public string Path
        {
            get
            {
                return path;
            }
        }

        public abstract DateTime CreationTime
        {
            get;
        }

        public abstract string Name
        {
            get;
        }

        public string ParentDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(Path);
            }
        }

        string parentDirectory;
    }
}
