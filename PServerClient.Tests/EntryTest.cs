using System.IO;
using NUnit.Framework;
using PServerClient.CVS;

namespace PServerClient.Tests
{
   [TestFixture]
   public class EntryTest
   {
      private Folder _parent;

      [SetUp]
      public void SetUp()
      {
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
         _parent = new Folder(di, "my connection", "/f1/f2/f3", "mymod");
      }

      [Test]
      public void RevisionPropertyTest()
      {
         Entry entry = new Entry("File1.cs", _parent);
         entry.EntryLine = "/AssemblyInfo.cs/1.1///D2010.01.15.09.27.11";
         string revision = entry.Revision;
         Assert.AreEqual("1.1", revision);
      }
   }
}