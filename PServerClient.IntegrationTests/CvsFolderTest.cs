using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.LocalFileSystem;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class CvsFolderTest
   {
      private DirectoryInfo _moduleDI;
      private CVSFolder _cvsFolder;
      private readonly string _connection = ":pserver:user@gb-aix-q:/usr/local/cvsroot/sandbox";
      private readonly string _module = "mymod";


      [SetUp]
      public void SetUp()
      {
         _moduleDI = new DirectoryInfo(@"c:\_temp\mymod");

         if (_moduleDI.Exists)
            _moduleDI.Delete(true);
         _moduleDI.Refresh();
         _moduleDI.Create();
         _moduleDI.Refresh();
         _cvsFolder = new CVSFolder(_moduleDI, _connection, _module);
         _cvsFolder.CVSDirectory.Create();
      }

      [TearDown]
      public void TearDown()
      {
         DirectoryInfo dir = new DirectoryInfo(@"c:\_temp");
         if (dir.Exists)
            dir.Delete(true);
      }

      [Test]
      public void ConstructorTest()
      {
         Assert.AreEqual(@"c:\_temp\mymod\CVS", _cvsFolder.CVSDirectory.FullName);
         Assert.AreEqual(@"c:\_temp\mymod\CVS\Root", _cvsFolder.RootFile.FullName);
         Assert.AreEqual(@"c:\_temp\mymod\CVS\Repository", _cvsFolder.RepositoryFile.FullName);
         Assert.AreEqual(@"c:\_temp\mymod\CVS\Entries", _cvsFolder.EntriesFile.FullName);
      }

      [Test]
      [ExpectedException(typeof (IOException))]
      public void GetRootStringWhenRootFileDoesNotExistTest()
      {
         CVSFolder cvsFolder = new CVSFolder(_moduleDI, ":pserver:abougie@gb-aix-q:/usr/local/cvsroot/sandbox", "rwtesting");
         string result = cvsFolder.ReadRootFile();
      }

      [Test]
      public void WriteRootFile()
      {
         _cvsFolder.WriteRootFile();

         FileInfo fi = new FileInfo(@"c:\_temp\mymod\CVS\Root");
         string result = ReaderWriter.Current.ReadFile(fi).Decode();
         Assert.AreEqual(_connection, result);
      }

      [Test]
      public void ReadRootfile()
      {
         Directory.CreateDirectory(@"c:\_temp\mymod\CVS");
         FileInfo file = new FileInfo(@"c:\_temp\mymod\CVS\Root");
         FileStream fs = file.Open(FileMode.CreateNew);
         string root = ":pserver:abougie@gb-aix-q:/usr/local/cvsroot/sandbox";
         fs.Write(root.Encode(), 0, root.Length);
         fs.Flush();
         fs.Close();

         string result = _cvsFolder.ReadRootFile();
         Assert.AreEqual(root, result);
      }

      [Test]
      public void WriteRepositoryFileTest()
      {
         _cvsFolder.WriteRepositoryFile();

         FileInfo fi = new FileInfo(@"c:\_temp\mymod\CVS\Repository");
         string result = ReaderWriter.Current.ReadFile(fi).Decode();
         Assert.AreEqual(_module, result);
      }

      [Test]
      public void ReadRepositoryFileTest()
      {
         Directory.CreateDirectory(@"c:\_temp\mymod\CVS");
         FileInfo file = new FileInfo(@"c:\_temp\mymod\CVS\Repository");
         FileStream fs = file.Open(FileMode.CreateNew);
         string rep = "rwtesting";
         fs.Write(rep.Encode(), 0, rep.Length);
         fs.Flush();
         fs.Close();

         string result = _cvsFolder.ReadRepositoryFile();
         Assert.AreEqual(rep, result);
      }

      

   }
}
