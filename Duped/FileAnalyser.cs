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
            public List<Recommendation> Recommendations { private set; get; }
            public long TimeTakenMs { set; get; }

            public Result(int totalFileCount,int uniqueFileCount, List<Recommendation> recommendations)
            {
                TotalFileCount = totalFileCount;
                UniqueFileCount = uniqueFileCount;
                Recommendations = recommendations;
            }
        }

        public class Recommendation
        {
            public FileInfoWrapper Keep { private set;  get; }
            public List<FileInfoWrapper> Remove { private set; get; }

            public Recommendation(IEnumerable<FileInfoWrapper> matchedFiles)
            {
                Remove = matchedFiles.OrderByDescending(x => x.Depth).ToList();
                Keep = Remove[0];
                Remove.RemoveAt(0);
            }
        }

        public static FileAnalyser.Result Run(List<Duped.FileInfoWrapper> files)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var recommendations = files
                .GroupBy(x => $"{x.Source.Name.ToLower()}__{x.Source.Length}")
                .ToDictionary(x => x.Key, x => new Recommendation(x))
                .Values
                .ToList();


            var result = new Result(files.Count,recommendations.Count,recommendations);

            watch.Stop();
            result.TimeTakenMs = watch.ElapsedMilliseconds;
            return result;
        }

    }
}
