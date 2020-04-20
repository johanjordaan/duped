using NUnit.Framework;

namespace DupedTests
{
    public class FileInfoWrapperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Run()
        {
            var fi = new System.IO.FileInfo("../../../fixtures/src/a/file-one.txt");

            var wrapped = new Duped.FileInfoWrapper(new System.IO.FileInfo("../../..").FullName,fi);

            Assert.AreEqual(3, wrapped.Depth);

            Assert.AreEqual("a", wrapped.Parents[0]);
            Assert.AreEqual("src", wrapped.Parents[1]);
            Assert.AreEqual("fixtures", wrapped.Parents[2]);

        }
    }
}