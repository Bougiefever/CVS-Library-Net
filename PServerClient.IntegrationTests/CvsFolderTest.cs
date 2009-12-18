using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.LocalFileSystem;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class CvsFolderTest
   {
      private ICVSItem _parent;

      [SetUp]
      public void SetUp()
      {
         DirectoryInfo dir = new DirectoryInfo(@"c:\_junk\rwtesting");
         if (dir.Exists)
            dir.Delete(true);
         dir.Refresh();
         dir.Create();
         dir.Refresh();
         _parent = new Folder(dir);
      }

      [TearDown]
      public void TearDown()
      {
         DirectoryInfo dir = new DirectoryInfo(@"c:\_junk\rwtesting");
         if (dir.Exists)
            dir.Delete(true);
      }

      [Test]
      public void ConstructorTest()
      {
         CVSFolder cvsFolder = new CVSFolder(_parent);
         Assert.AreEqual(@"c:\_junk\rwtesting\CVS", cvsFolder.CVSDirectory.FullName);
         Assert.AreEqual(@"c:\_junk\rwtesting\CVS\Root", cvsFolder.RootFile.FullName);
         Assert.AreEqual(@"c:\_junk\rwtesting\CVS\Repository", cvsFolder.RepositoryFile.FullName);
         Assert.AreEqual(@"c:\_junk\rwtesting\CVS\Entries", cvsFolder.EntriesFile.FullName);
      }

      [Test]
      [ExpectedException(typeof (IOException))]
      public void GetRootStringWhenRootFileDoesNotExistTest()
      {
         CVSFolder cvsFolder = new CVSFolder(_parent);
         string result = cvsFolder.GetRootString();
      }

      [Test]
      public void WriteRootFile()
      {
         CVSFolder cvsFolder = new CVSFolder(_parent);
         string root = ":pserver:abougie@gb-aix-q:/usr/local/cvsroot/sandbox";
         cvsFolder.WriteRootFile(root);
         
         FileInfo fi = new FileInfo(@"c:\_junk\rwtesting\CVS\Root");
         string result = ReaderWriter.Current.ReadFile(fi).Decode();
         Assert.AreEqual(root, result);
      }

      [Test]
      public void ReadRootfile()
      {
         Directory.CreateDirectory(@"c:\_junk\rwtesting\CVS");
         FileInfo file = new FileInfo(@"c:\_junk\rwtesting\CVS\Root");
         FileStream fs = file.Open(FileMode.CreateNew);
         string root = ":pserver:abougie@gb-aix-q:/usr/local/cvsroot/sandbox";
         fs.Write(root.Encode(), 0, root.Length);
         fs.Flush();
         fs.Close();

         CVSFolder folder = new CVSFolder(_parent);
         string result = folder.GetRootString();
         Assert.AreEqual(root, result);
      }

      [Test]
      public void WriteRepositoryFileTest()
      {
         CVSFolder folder = new CVSFolder(_parent);
         string rep = "abougie/cvstest";
         folder.WriteRepositoryFile(rep);

         FileInfo fi = new FileInfo(@"c:\_junk\rwtesting\CVS\Repository");
         string result = ReaderWriter.Current.ReadFile(fi).Decode();
         Assert.AreEqual(rep, result);
      }

      [Test]
      public void ReadRepositoryFileTest()
      {
         Directory.CreateDirectory(@"c:\_junk\rwtesting\CVS");
         FileInfo file = new FileInfo(@"c:\_junk\rwtesting\CVS\Repository");
         FileStream fs = file.Open(FileMode.CreateNew);
         string rep = "abougie/cvstest";
         fs.Write(rep.Encode(), 0, rep.Length);
         fs.Flush();
         fs.Close();

         CVSFolder folder = new CVSFolder(_parent);
         string result = folder.GetRepositoryString();
         Assert.AreEqual(rep, result);
      }

      

   }
}
