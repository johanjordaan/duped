using System;
using System.Collections.Generic;
using System.Text;

namespace Duped
{
    public class FileInfoWrapper
    {
        public System.IO.FileInfo Source { private set; get; }
        public int Depth { private set; get; }
        public List<String> Parents { private set; get; }
        public String Root { private set; get; }

        private void buildParents(System.IO.DirectoryInfo curDir,int curDepth)
        {
            var parent = curDir.Parent;
            Parents.Add(curDir.Name);
            if ( parent == null || curDir.FullName == Root)
            {
                Depth = curDepth;
            } else
            {
                buildParents(parent, curDepth+1);
            }

        }

        public FileInfoWrapper(String root,System.IO.FileInfo source)
        {
            Source = source;
            Root = root;
            Parents = new List<string>();

            this.buildParents(Source.Directory, 0);
        }
    }
}
