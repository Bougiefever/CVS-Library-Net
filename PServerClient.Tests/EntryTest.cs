using System.IO;
using NUnit.Framework;
using PServerClient.CVS;

namespace PServerClient.Tests
{
   /// <summary>
   /// Tests for the Entry class
   /// </summary>
   [TestFixture]
   public class EntryTest
   {
      private Folder _parent;

      /// <summary>
      /// Sets up for testing
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
         _parent = new Folder(di, "my connection", "/f1/f2/f3", "mymod");
      }

      /// <summary>
      /// Tests the revision property.
      /// </summary>
      [Test]
      public void TestRevisionProperty()
      {
         Entry entry = new Entry("File1.cs", _parent);
         entry.EntryLine = "/AssemblyInfo.cs/1.1///D2010.01.15.09.27.11";
         string revision = entry.Revision;
         Assert.AreEqual("1.1", revision);
      }
   }
}