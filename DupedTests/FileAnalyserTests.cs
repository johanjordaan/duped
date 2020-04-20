using NUnit.Framework;

namespace DupedTests
{
    public class FileAnalayserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Run()
        {
            var files = Duped.DirWalker.Run("../../../fixtures/src").Files;
            Assert.AreEqual(2, files.Count);
            var result = Duped.FileAnalyser.Run(files);

        }
    }
}