using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.LocalFileSystem;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;
using Is=Rhino.Mocks.Constraints.Is;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ServerFileReceiverTest
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         _working = new DirectoryInfo(@"c:\_temp");
         _root.WorkingDirectory = _working;
         _root.Module = _module;
         _fileReceiver = new ServerFileReceiver(_root);
      }

      #endregion

      private Root _root;
      private DirectoryInfo _working;
      private string _module = "mymod";
      private ServerFileReceiver _fileReceiver;

      private static void PrintWorkingDirStructure(ICVSItem working)
      {
         Console.Write("(d)");
         Console.WriteLine(working.Info.FullName);
         foreach (ICVSItem item in working)
         {
            if (item is Folder)
               PrintWorkingDirStructure(item);
            else
               Console.WriteLine("(f)" + item.Info.FullName);
         }
      }

      [Test]
      public void AddNewEntryTest()
      {
         ModTimeResponse mod = new ModTimeResponse();
         mod.Process(new List<string> {"27 Nov 2009 14:21:06 -0000"});
         MessageTagResponse mt = new MessageTagResponse();
         mt.Process(new List<string> { "fname mymod/file1.cs" });
         UpdatedResponse udr = TestHelper.GetMockUpdatedResponse("mymod/", "file1.cs");

         IList<IResponse> responses = new List<IResponse> {mod, mt, udr};

         _fileReceiver.AddNewEntry(responses);
         ICVSItem entry = _root.ModuleFolder[0];
         Assert.IsInstanceOf(typeof (Entry), entry);
         Assert.AreEqual("file1.cs", entry.Name);
      }

      [Test]
      public void CreateFolderStructureTest()
      {
         string[] folders = new[] {"mymod", "sub1", "sub2"};
         _fileReceiver.CreateFolderStructure(folders);
         Folder sub1 = (Folder) _root.ModuleFolder[0];
         Assert.AreEqual("sub1", sub1.Name);
         Folder sub2 = (Folder) sub1[0];
         Assert.AreEqual("sub2", sub2.Name);

         folders = new[] {"mymod", "sub1", "sub2", "sub3"};
         _fileReceiver.CreateFolderStructure(folders);

         folders = new[] {"module", "sub1", "sub22"};
         _fileReceiver.CreateFolderStructure(folders);
         Folder sub22 = (Folder) _root.ModuleFolder[0][1];
         Assert.AreEqual("sub22", sub22.Name);

         Folder sub3 = (Folder) _root.ModuleFolder[0][0][0];
         Assert.AreEqual("sub3", sub3.Name);
         PrintWorkingDirStructure(_root.ModuleFolder);
      }

      [Test]
      public void ReceiveMTResponsesTest()
      {
         IList<IResponse> coresponses = new List<IResponse> {new ClearStickyResponse(), new ClearStaticDirectoryResponse()};

         IList<IResponse> responses = TestHelper.GetMockCheckoutResponses("8 Dec 2009 15:26:27 -0000", "mymod/", "file1.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }

         responses = TestHelper.GetMockCheckoutResponses("27 Nov 2009 14:21:06 -0000", "mymod/", "file2.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }

         responses = TestHelper.GetMockCheckoutResponses("27 Nov 2009 14:21:06 -0000", "mymod/sub1/", "file3.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }
         coresponses.Add(new OkResponse());

         _fileReceiver.ReceiveMTUpdatedResponses(coresponses);

         Folder module = _root.ModuleFolder;
         Assert.AreEqual(3, module.Count);
         Assert.AreEqual("file1.cs", _root.ModuleFolder[0].Name);
         Folder sub1 = (Folder) _root.ModuleFolder[2];
         Assert.AreEqual(1, sub1.Count);
         Entry entry = (Entry) sub1[0];
         Assert.AreEqual("file3.cs", entry.Name);
         PrintWorkingDirStructure(_root.ModuleFolder);
      }

      [Test]
      public void SaveFolderTest()
      {
         DirectoryInfo dimodule = new DirectoryInfo(@"c:\_temp\mymod");
         Folder module = new Folder(dimodule, "connection string", "mymod");

         FileInfo fi1 = new FileInfo(@"c:\_temp\mymod\file1.cs");
         Entry file1 = new Entry(fi1) {Length = 1, FileContents = new byte[] {97}};
         module.AddItem(file1);

         DirectoryInfo disub1 = new DirectoryInfo(@"c:\_temp\mymod\sub1");
         Folder sub1 = new Folder(disub1, "connection string", "mymod/sub1");
         module.AddItem(sub1);

         FileInfo fi2 = new FileInfo(@"c:\_temp\mymod\sub1\file2.cs");
         Entry file2 = new Entry(fi2) {Length = 1, FileContents = new byte[] {97}};
         sub1.AddItem(file2);

         DirectoryInfo disub2 = new DirectoryInfo(@"c:\_temp\mymod\sub1\sub2");
         Folder sub2 = new Folder(disub2, "connection string", "mymod/sub1/sub2");
         sub1.AddItem(sub2);

         FileInfo fi3 = new FileInfo(@"c:\_temp\mymod\sub1\sub2\file3.cs");
         Entry file3 = new Entry(fi3) {Length = 1, FileContents = new byte[] {97}};
         sub2.AddItem(file3);

         MockRepository mocks = new MockRepository();
         IReaderWriter rw = mocks.DynamicMock<IReaderWriter>();
         ReaderWriter.Current = rw;

         using (mocks.Record())
         {
            Expect.Call(() => rw.CreateDirectory(dimodule));
            Expect.Call(() => rw.CreateDirectory(disub1));
            Expect.Call(() => rw.CreateDirectory(disub2));
            Expect.Call(() => rw.WriteFile(null, null))
               .IgnoreArguments()
               .Constraints(Is.Anything(), Is.Anything()).Repeat.Times(3);
         }
         using (mocks.Playback())
         {
            _fileReceiver.WriteToDisk(module);
         }
      }
   }
}