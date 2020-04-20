using NUnit.Framework;

namespace DupedTests
{
    public class DirWalkerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Run()
        {
            var results = Duped.DirWalker.Run("../../../fixtures/src");
            Assert.AreEqual(2, results.Files.Count);
        }
    }
}