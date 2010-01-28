using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   /// <summary>
   /// Tests for the ResponseProcessor class
   /// </summary>
   [TestFixture]
   public class ResponseProcessorTest
   {
      private ResponseProcessor _processor;
      private Folder _rootFolder;
      private Folder _sub1;
      private Folder _sub2;
      private Folder _sub3;
      private Folder _sub11;
      private Folder _sub21;
      private Folder _sub211;
      private Folder _sub12;

      /// <summary>
      /// Sets up for ResponseProcessor tests
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _processor = new ResponseProcessor();
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\cvs\abougie");
         _rootFolder = new Folder(di, "connection string", "/usr/local/cvsroot/sandbox", "abougie");
         _sub1 = new Folder("sub1", _rootFolder);
         _sub2 = new Folder("sub2", _sub1);
         _sub21 = new Folder("sub21", _sub2);
         _sub211 = new Folder("sub211", _sub21);
         _sub3 = new Folder("sub3", _sub2);
         _sub11 = new Folder("sub11", _sub1);
         _sub12 = new Folder("sub12", _sub11);
      }

      ////[Test][Ignore]
      ////public void CreateFileGroupsFromResponsesTest()
      ////{
      ////   ResponseProcessor processor = new ResponseProcessor();
      ////   DirectoryInfo di = Directory.GetParent(Environment.CurrentDirectory);
      ////   FileInfo fi = new FileInfo(Path.Combine(di.FullName, "TestSetup\\ExportCommand.xml"));
      ////   TextReader reader = fi.OpenText();
      ////   XDocument xdoc = XDocument.Load(reader);
      ////   bool result = TestHelper.ValidateCommandXML(xdoc);
      ////   Assert.IsTrue(result);
      ////   PServerFactory factory = new PServerFactory();
      ////   IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      ////   DateTime date = new DateTime();
      ////   IConnection connection = new PServerConnection();
      ////   ICommand cmd = factory.CreateCommand(xdoc, new object[] { root, connection, date });
      ////   IList<IResponse> responses = cmd.Responses;
      ////   IList<IFileResponseGroup> files = processor.CreateFileGroupsFromResponses(responses);
      ////   Assert.AreEqual(4, files.Count);
      ////   IFileResponseGroup file = files[0];
      ////   Assert.AreEqual(DateTime.Parse("12/8/2009 3:26:27 PM"), file.ModTime.ModTime);
      ////   Assert.AreEqual("fname abougie/cvstest/AssemblyInfo.cs", file.MT.Lines[2]);
      ////   Assert.AreEqual("AssemblyInfo.cs", file.FileResponse.Name);
      ////}

      ////[Test]
      ////public void CreateCVSFileStructureTest()
      ////{
      ////   ResponseProcessor processor = new ResponseProcessor();
      ////   IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password); 
      ////   root.WorkingDirectory = TestConfig.WorkingDirectory;

      ////   IFileResponse file1 = new UpdatedResponse();
      ////   string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
      ////   string module = "mymod/";
      ////   IList<string> lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
      ////   file1.Initialize(lines);
      ////   file1.Contents = contents.Encode();

      ////   IFileResponse file2 = new UpdatedResponse();
      ////   lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/file2.cs", "/file2.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
      ////   file2.Initialize(lines);
      ////   file2.Contents = contents.Encode();

      ////   IFileResponse file3 = new UpdatedResponse();
      ////   module = "mymod/Properties/";
      ////   lines = new List<string> { module, "/usr/local/cvsroot/sandbox/mymod/Properties/AssemblyInfo.cs", "/AssemblyInfo.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
      ////   file3.Initialize(lines);
      ////   file3.Contents = contents.Encode();

      ////   IFileResponseGroup fg1 = new FileResponseGroup { FileResponse = file1 };
      ////   IFileResponseGroup fg2 = new FileResponseGroup { FileResponse = file2 };
      ////   IFileResponseGroup fg3 = new FileResponseGroup { FileResponse = file3 };

      ////   IList<IFileResponseGroup> files = new List<IFileResponseGroup> { fg1, fg2, fg3 };

      ////   Folder test = processor.CreateCVSFileStructure(root, files);

      ////   Assert.AreEqual("mymod", test.Module);
      ////   Assert.AreEqual(3, test.Count);
      ////}

      /// <summary>
      /// Test for FindModuleFolder from root folder test.
      /// </summary>
      [Test]
      public void FindModuleFolderFromRootFolderTest()
      {
         string module = "abougie/sub1";
         Folder result = _processor.FindModuleFolder(_rootFolder, module);
         Assert.AreSame(_sub1, result);
         module = "abougie/sub1/sub2";
         result = _processor.FindModuleFolder(_rootFolder, module);
         Assert.AreSame(_sub2, result);
         module = "abougie/sub1/sub2/sub3";
         result = _processor.FindModuleFolder(_rootFolder, module);
         Assert.AreSame(_sub3, result);
         module = "abougie/sub1/sub11";
         result = _processor.FindModuleFolder(_rootFolder, module);
         Assert.AreSame(_sub11, result);
      }

      /// <summary>
      /// Test for FindModuleFolder from self folder test.
      /// </summary>
      [Test]
      public void FindModuleFolderFromSelfTest()
      {
         string module = "abougie/sub1/sub2/sub3";

         Folder result = _processor.FindModuleFolder(_sub3, module);
         Assert.AreSame(result, _sub3);
      }

      /// <summary>
      /// Test for FindModuleFolder from parent folder test.
      /// </summary>
      [Test]
      public void FindModuleFolderFromParentTest()
      {
         string module = "abougie/sub1/sub2/sub3";

         Folder result = _processor.FindModuleFolder(_sub2, module);
         Assert.AreSame(result, _sub3);

         result = _processor.FindModuleFolder(_sub1, module);
         Assert.AreSame(result, _sub3);

         result = _processor.FindModuleFolder(_rootFolder, module);
         Assert.AreSame(result, _sub3);
      }

      /// <summary>
      /// Test for FindModuleFolder from sibling folder test.
      /// </summary>
      [Test]
      public void FindModuleFolderFromSiblingTest()
      {
         string module = "abougie/sub1/sub2/sub3";

         Folder result = _processor.FindModuleFolder(_sub211, module);
         Assert.AreSame(_sub3, result);

         result = _processor.FindModuleFolder(_sub12, module);
         Assert.AreSame(_sub3, result);
      }

      /// <summary>
      /// Test for FindModuleFolder returns null when the folder does not exist.
      /// </summary>
      [Test]
      public void FindModuleFolderReturnsNullWhenNotExistsTest()
      {
         string module = "abougie/sub";

         Folder result = _processor.FindModuleFolder(_sub3, module);
         Assert.IsNull(result);
      }

      /// <summary>
      /// Test for AddFolderToStructure
      /// </summary>
      [Test]
      public void AddFolderToStructureTest()
      {
         string module = "abougie/newfolder";
         Folder result = _processor.AddFolderToStructure(_rootFolder, module);
         Assert.AreNotSame(_rootFolder, result);
         Assert.AreEqual("newfolder", result.Info.Name);
         Assert.AreEqual(module, result.Module);
         module = "abougie/sub1/newfolder";
         result = _processor.AddFolderToStructure(_rootFolder, module);
         Assert.AreEqual(module, result.Module);
         module = "abougie/sub11/sub12/newfolder";
         result = _processor.AddFolderToStructure(_rootFolder, module);
         Assert.AreEqual(module, result.Module);
      }

      /// <summary>
      /// Test for GetModuleFolder when the folder exists
      /// </summary>
      [Test]
      public void GetModuleFolderWhenFolderExistsTest()
      {
         string module = "abougie/sub1/sub2/";
         Folder result = _processor.GetModuleFolder(_rootFolder, module);
         Assert.AreSame(_sub2, result);
      }

      /// <summary>
      /// Test for GetModuleFolder when the folder does not exist
      /// </summary>
      [Test]
      public void GetModuleFolderAddFolderToStructureTest()
      {
         string module = "abougie/newfolder";
         Folder result = _processor.GetModuleFolder(_rootFolder, module);
         Assert.IsNotNull(result);
         Assert.AreEqual("abougie/newfolder", result.Module);
      }
   }
}