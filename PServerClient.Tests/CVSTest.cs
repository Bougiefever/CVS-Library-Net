using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CVSTest
   {
      private readonly string _connection = ":pserver:username@host-name:/usr/local/cvsroot/sandbox";
      private readonly string _repository = "/usr/local/cvsroot/sandbox";
      private readonly string _module = "mymod";

      [Test]
      public void RootTest()
      {
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         root.WorkingDirectory = TestConfig.WorkingDirectory;
         root.Repository = _repository;
         Assert.AreEqual(_connection, root.CVSConnectionString);
      }

      [Test]
      public void FolderTest()
      {
         // starting module folder
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\working\team\mymod");
         Folder rootModuleFolder = new Folder(di, _connection, _repository, _module);
         Assert.IsNull(rootModuleFolder.Parent);
         Assert.AreEqual("mymod", rootModuleFolder.Module);
         Assert.AreEqual(_connection, rootModuleFolder.Connection);
         Assert.AreEqual(_repository, rootModuleFolder.Repository);
         CVSFolder cvsfolder = rootModuleFolder.CVSFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\CVS", cvsfolder.CVSDirectory.FullName);

         // add sub folder
         di = new DirectoryInfo(@"c:\_temp\working\team\mymod\project1");
         Folder project1 = new Folder("project1", rootModuleFolder);
         Assert.AreSame(rootModuleFolder, project1.Parent);
         Assert.AreEqual("mymod/project1", project1.Module);
         Assert.AreEqual(_connection, project1.Connection);
         Assert.AreEqual(_repository, project1.Repository);
         cvsfolder = project1.CVSFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\project1\CVS", cvsfolder.CVSDirectory.FullName);
         Assert.AreEqual(1, rootModuleFolder.Count);
         Assert.AreSame(project1, rootModuleFolder[0]);

         // add sub-sub folder
         di = new DirectoryInfo(@"c:\_temp\working\team\mymod\project1\Properties");
         Folder prop = new Folder("Properties", project1);
         Assert.AreEqual("mymod/project1/Properties", prop.Module);
         Assert.AreSame(project1, prop.Parent);
         cvsfolder = prop.CVSFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\project1\Properties\CVS", cvsfolder.CVSDirectory.FullName);
         Assert.AreEqual(1, project1.Count);
         Assert.AreSame(prop, project1[0]);
      }

      [Test]
      public void EntryTest()
      {
         // starting module folder
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\working\team\mymod");
         Folder rootModuleFolder = new Folder(di, _connection, _repository, _module);
         Entry file1 = new Entry("file1.cs", rootModuleFolder);
         Assert.AreSame(rootModuleFolder, file1.Parent);
         Assert.AreSame(file1.CVSFolder, rootModuleFolder.CVSFolder);
         Assert.AreEqual(@"c:\_temp\working\team\mymod\file1.cs", file1.Info.FullName);
      }
   }
}