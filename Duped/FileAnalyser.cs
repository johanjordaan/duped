using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duped
{
    public class FileAnalyser
    {
        public class FileActions
        {
            System.IO.FileInfo Keep { get; set; }
            List<System.IO.FileInfo> Delete { get; set; }
        }

        public class Result
        {
        }

        public static FileAnalyser.Result Run(List<Duped.FileInfoWrapper> files)
        {
            var result = new Result();

            var uniqueByNameAndSize = files.GroupBy(x => $"{x.Source.Name.ToLower()}__{x.Source.Length}").ToDictionary(x => x.Key, x => x.ToList());

            return result;
        }

    }
}
