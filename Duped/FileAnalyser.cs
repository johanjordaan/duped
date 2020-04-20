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
            public int TotalFileCount { private set;  get; }
            public int UniqueFileCount { private set; get; }

            public Result(int totalFileCount,int uniqueFileCount)
            {
                TotalFileCount = totalFileCount;
                UniqueFileCount = uniqueFileCount;
            }
        }

        public static FileAnalyser.Result Run(List<Duped.FileInfoWrapper> files)
        {
            var uniqueByNameAndSize = files
                .GroupBy(x => $"{x.Source.Name.ToLower()}__{x.Source.Length}")
                .ToDictionary(x => x.Key, x => x.OrderBy(x=>x.Depth).ToList());


            var result = new Result(files.Count,uniqueByNameAndSize.Count);


            return result;
        }

    }
}
