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
            Assert.AreEqual(2, result.TotalFileCount);
            Assert.AreEqual(1, result.UniqueFileCount);
            Assert.AreEqual(1, result.Recommendations.Count);
            Assert.AreEqual("file-one.txt", result.Recommendations[0].Keep.Source.Name);
            Assert.AreEqual(1, result.Recommendations[0].Remove.Count);
            Assert.AreEqual("file-one.txt", result.Recommendations[0].Remove[0].Source.Name);


        }
    }
}