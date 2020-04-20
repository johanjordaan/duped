using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Duped
{
    class Program
    {
        public class StackBasedIteration
        {
            static void Main(string[] args)
            {
                var result = DirWalker.Run(args[0]);

                //var uniqueByExtension = result.Files.GroupBy(x => x.Extension.ToLower()).ToDictionary(x => x.Key, x => x.ToList());
                //var uniqueByName = result.Files.GroupBy(x=>x.Name).ToDictionary(x=>x.Key,x=>x.ToList());
                //var uniqueByNameAndSize = result.Files.GroupBy(x => $"{x.Name.ToLower()}__{x.Length}").ToDictionary(x => x.Key, x => x.ToList());


                Console.WriteLine($"[{result.Files.Count}] files in [{result.TimeTakenMs/1000}] s");
                //Console.WriteLine($"[{uniqueByExtension.Count}] unique extensions");
                //Console.WriteLine($"[{uniqueByName.Count}] uniquely named");
                //Console.WriteLine($"[{uniqueByNameAndSize.Count}] uniquely named and sized");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }

            
        }
    }
}
