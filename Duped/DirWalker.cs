using System;
using System.Collections.Generic;
using System.Text;

namespace Duped
{

    public class DirWalker
    {
        public class Result
        {
            public String Root { private set; get; }
            public List<System.IO.FileInfo> Files { private set; get; }

            public long TimeTakenMs { set; get; }

            public Result(String root)
            {
                Root = root;
                Files = new List<System.IO.FileInfo>();
            }
        }

        public static DirWalker.Result Run(string root)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var result = new DirWalker.Result(root);

            // Data structure to hold names of subfolders to be
            // examined for files.
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
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The 
                // choice of which exceptions to catch depends entirely on the specific task 
                // you are intending to perform and also on how much you know with certainty 
                // about the systems on which this code will run.
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
                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files)
                {
                    try
                    {
                        count++;
                        // Perform whatever action is required in your scenario.
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        result.Files.Add(fi);
                        //Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                        try
                        {
                            //IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fi.FullName);
                            //foreach (var directory in directories)
                            //    foreach (var tag in directory.Tags)
                            //        Console.WriteLine($"META - {directory.Name} - {tag.Name} = {tag.Description}");
                        }
                        catch
                        {
                            //Console.WriteLine($"META - NONE");
                        }

                        if (count % 1000 == 0)
                        {
                            Console.WriteLine($"Count - -------------------------------------------------------------------------->>>  {count}");
                        }


                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }


            watch.Stop();
            result.TimeTakenMs = watch.ElapsedMilliseconds;
            return result;

        }
    }

}
