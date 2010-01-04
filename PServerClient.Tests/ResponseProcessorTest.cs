using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
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
         IRequest request = cmd.Requests.OfType<ExportRequest>().First();
         IList<IResponse> responses = request.Responses;
         IList<IFileResponseGroup> files = processor.CreateFileGroupsFromResponses(responses);
         Assert.AreEqual(4, files.Count);
         IFileResponseGroup file = files[0];
         Assert.AreEqual(DateTime.Parse("12/8/2009 3:26:27 PM"), file.ModTime.ModTime);
         Assert.AreEqual("fname abougie/cvstest/AssemblyInfo.cs", file.MT.Lines[2]);
         Assert.AreEqual("AssemblyInfo.cs", file.FileResponse.File.Name);
      }

      [Test]
      public void ModuleFolderTest()
      {
         ResponseProcessor processor = new ResponseProcessor();
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password); 
         root.WorkingDirectory = TestConfig.WorkingDirectory;

         IFileResponse file1 = new UpdatedResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         string module = "mymod/";
         IList<string> lines = new List<string> {module, "/usr/local/cvsroot/sandbox/mymod/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         file1.Process(lines);
         file1.File.Contents = contents.Encode();

         IFileResponse file2 = new UpdatedResponse();
         lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/file2.cs", "/file2.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
         file2.Process(lines);
         file2.File.Contents = contents.Encode();

         IFileResponse file3 = new UpdatedResponse();
         module = module + "Properties/";
         lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/Properties/AssemblyInfo.cs", "/AssemblyInfo.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
         file3.Process(lines);
         file3.File.Contents = contents.Encode();

         IFileResponseGroup fg1 = new FileResponseGroup() { FileResponse = file1 };
         IFileResponseGroup fg2 = new FileResponseGroup() { FileResponse = file2 };
         IFileResponseGroup fg3 = new FileResponseGroup() { FileResponse = file3 };

         IList<IFileResponseGroup> files = new List<IFileResponseGroup> { fg1, fg2, fg3 };

         Folder test = processor.ModuleFolder(root, files);

      }
   }
}