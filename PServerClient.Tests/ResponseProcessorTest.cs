using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseProcessorTest
   {
      [Test]
      public void CreateFileGroupsFromResponsesTest()
      {
         ResponseProcessor processor = new ResponseProcessor();
         DirectoryInfo di = Directory.GetParent(Environment.CurrentDirectory);
         FileInfo fi = new FileInfo(Path.Combine(di.FullName, "TestSetup\\ExportCommand.xml"));
         TextReader reader = fi.OpenText();
         XDocument xdoc = XDocument.Load(reader);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
         PServerFactory factory = new PServerFactory();
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         DateTime date = new DateTime();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] {root, date});
         IList<IResponse> responses = cmd.Responses;
         IList<IFileResponseGroup> files = processor.CreateFileGroupsFromResponses(responses);
         Assert.AreEqual(4, files.Count);
         IFileResponseGroup file = files[0];
         Assert.AreEqual(DateTime.Parse("12/8/2009 3:26:27 PM"), file.ModTime.ModTime);
         Assert.AreEqual("fname abougie/cvstest/AssemblyInfo.cs", file.MT.Lines[2]);
         Assert.AreEqual("AssemblyInfo.cs", file.FileResponse.Name);
      }

      [Test]
      public void CreateCVSFileStructureTest()
      {
         ResponseProcessor processor = new ResponseProcessor();
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password); 
         root.WorkingDirectory = TestConfig.WorkingDirectory;

         IFileResponse file1 = new UpdatedResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         string module = "mymod/";
         IList<string> lines = new List<string> {module, "/usr/local/cvsroot/sandbox/mymod/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         file1.Initialize(lines);
         file1.Contents = contents.Encode();

         IFileResponse file2 = new UpdatedResponse();
         lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/file2.cs", "/file2.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
         file2.Initialize(lines);
         file2.Contents = contents.Encode();

         IFileResponse file3 = new UpdatedResponse();
         module = "mymod/Properties/";
         lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/Properties/AssemblyInfo.cs", "/AssemblyInfo.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
         file3.Initialize(lines);
         file3.Contents = contents.Encode();

         IFileResponseGroup fg1 = new FileResponseGroup { FileResponse = file1 };
         IFileResponseGroup fg2 = new FileResponseGroup { FileResponse = file2 };
         IFileResponseGroup fg3 = new FileResponseGroup { FileResponse = file3 };

         IList<IFileResponseGroup> files = new List<IFileResponseGroup> { fg1, fg2, fg3 };

         Folder test = processor.CreateCVSFileStructure(root, files);

         Assert.AreEqual("mymod", test.Module);
         Assert.AreEqual(3, test.Count);
      }

      [Test]
      public void GetParentFolderTest()
      {
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\cvs\abougie");
         Folder rootFolder = new Folder(di, "connection string", "/usr/local/cvsroot/sandbox", "abougie");

         string module = "abougie";
         ResponseProcessor processor = new ResponseProcessor();
         Folder result = processor.GetModuleFolder(rootFolder, module);
         Assert.AreSame(rootFolder, result);

         module = "abougie/cvstest";
         result = processor.GetModuleFolder(rootFolder, module);
         Assert.IsNull(result);

         Folder sub1 = new Folder("sub1", rootFolder);
         Folder sub2 = new Folder("sub2", rootFolder);
         Folder sub3 = new Folder("sub3", rootFolder);
         Folder sub11 = new Folder("sub11", sub1);

         module = "abougie/sub1";
         result = processor.GetModuleFolder(rootFolder, module);
         Assert.AreSame(result, sub1);
         module = "abougie/sub2";
         result = processor.GetModuleFolder(rootFolder, module);
         Assert.AreSame(result, sub2);
         module = "abougie/sub3";
         result = processor.GetModuleFolder(rootFolder, module);
         Assert.AreSame(result, sub3);
         module = "abougie/sub1/sub11";
         result = processor.GetModuleFolder(rootFolder, module);
         Assert.AreSame(result, sub11);
      }

      [Test]
      public void AddFolderToStructureTest()
      {
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\cvs\abougie");
         Folder rootFolder = new Folder(di, "connection string", "/usr/local/cvsroot/sandbox", "abougie");
         string module = "abougie/sub1";
         ResponseProcessor processor = new ResponseProcessor();
         Folder result = processor.AddFolderToStructure(rootFolder, module);
         Assert.AreNotSame(rootFolder, result);
         Assert.AreEqual("sub1", result.Info.Name);
         Assert.AreEqual(1, rootFolder.Count);

      }
   }
}