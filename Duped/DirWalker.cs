using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Duped
{

    public class DirWalker
    {
        public class Result
        {
            public String Root { private set; get; }
            public List<Duped.FileInfoWrapper> Files { private set; get; }

            public long TimeTakenMs { set; get; }

            public Result(String root)
            {
                Root = root;
                Files = new List<Duped.FileInfoWrapper>();
            }
        }

        public class Progress
        {
            public String FileName { private set; get; }
            public int DiretoriesInQueue { private set; get; }

            public Progress(String fileName,int diretoriesInQueue)
            {
                FileName = fileName;
                DiretoriesInQueue = diretoriesInQueue;
            }
        }



        public static IEnumerable<Progress> Files(string root)
        {
            Stack<string> dirs = new Stack<string>(5000);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                foreach (string str in subDirs)
                {
                    dirs.Push(str);
                }

                foreach (string file in files)
                {
                    yield return new Progress(file,dirs.Count);
                }

            }

        }

        public static DirWalker.Result Run(string root)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var result = new DirWalker.Result(root);

            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            int count = 0;

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                foreach (string str in subDirs)
                {
                    dirs.Push(str);
                }

                foreach (string file in files)
                {
                    try
                    {
                        count++;
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        result.Files.Add(new Duped.FileInfoWrapper(new System.IO.FileInfo(root).FullName,fi));
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

            }


            watch.Stop();
            result.TimeTakenMs = watch.ElapsedMilliseconds;
            return result;

        }
    }

}
