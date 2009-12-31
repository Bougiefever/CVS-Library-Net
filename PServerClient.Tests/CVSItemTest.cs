using System.IO;
using NUnit.Framework;
using PServerClient.CVS;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CVSItemTest
   {
      private readonly string _connection = ":pserver:user@gb-aix-q:/usr/local/cvsroot/sandbox";
      private readonly string _module = "mymod";

      [Test]
      public void FolderStructorTest()
      {
         // starting module folder
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\working\team\mymod");
         Folder rootModuleFolder = new Folder(di, _connection, _module);
         Assert.AreEqual("mymod/", rootModuleFolder.Repository);
         Assert.IsNull(rootModuleFolder.Parent);
         Assert.AreEqual(_connection, rootModuleFolder.CVSConnectionString);
         CVSFolder cvsfolder = rootModuleFolder.CvsFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\CVS", cvsfolder.CVSDirectory.FullName);

         // add sub folder
         di = new DirectoryInfo(@"c:\_temp\working\team\mymod\project1");
         Folder project1 = new Folder(di, rootModuleFolder);
         Assert.AreEqual("mymod/project1/", project1.Repository);
         Assert.AreSame(rootModuleFolder, project1.Parent);
         Assert.AreEqual(_connection, project1.CVSConnectionString);
         cvsfolder = project1.CvsFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\project1\CVS", cvsfolder.CVSDirectory.FullName);
      }
   }
}