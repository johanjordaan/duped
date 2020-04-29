using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DupedTests
{
    public class DirWalkerTests
    {
        [SetUp]
        public void Setup()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }


        [Test]
        public void Files()
        {
            var files = new List<string>();
            foreach (var progress in Duped.DirWalker.Files("../../../fixtures/src"))
            {
                files.Add(progress.FileName);
            }

            
            Assert.AreEqual(2, files.Count);
        }

        [Test]
        public void Run()
        {
            var results = Duped.DirWalker.Run("../../../fixtures/src");
            Assert.AreEqual(2, results.Files.Count);
        }
    }
}