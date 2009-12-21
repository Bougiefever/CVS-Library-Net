using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Responses;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ServerFileReceiverTest
   {
      private Root _root;
      private DirectoryInfo _working;
      private string _module = "mymod";
      private ServerFileReceiver _fileReceiver;

      [SetUp]
      public void SetUp()
      {
         string hostName = "host-name";
         int port = 1;
         string user = "username";
         string pwd = "password";
         string repoPath = "/f1/f2/f3";

         _root = new Root(hostName, port, user, pwd, repoPath);

         _working = new DirectoryInfo(@"c:\_temp");
         _root.WorkingDirectory = _working;
         _root.Module = _module;
         _fileReceiver = new ServerFileReceiver(_root);

         if (_working.Exists)
            _working.Delete(true);
         _working.Refresh();
      }

      [TearDown]
      public void TearDown()
      {
         if (_working.Exists)
            _working.Delete(true);
      }

      [Test]
      public void SaveFolderTest()
      {

         string[] folders = new[] { "mymod", "sub1", "sub2" };
         _fileReceiver.CreateFolderStructure(folders);

         folders = new[] { "mymod", "sub1", "sub2", "sub3" };
         _fileReceiver.CreateFolderStructure(folders);

         folders = new[] { "module", "sub1", "sub22" };
         _fileReceiver.CreateFolderStructure(folders);

         _fileReceiver.WriteToDisk(_root.ModuleFolder);

         Folder sub1 = (Folder)_root.ModuleFolder[0];
         Folder sub2 = (Folder)sub1[0];
         Folder sub22 = (Folder)sub1[1];
         Folder sub3 = (Folder)sub2[0];

         PrintWorkingDirStructure(_root.ModuleFolder);

         Assert.IsTrue(sub1.Info.Exists);
         Assert.IsTrue(sub2.Info.Exists);
         Assert.IsTrue(sub22.Info.Exists);
         Assert.IsTrue(sub3.Info.Exists);
      }

      [Test]
      public void SaveFoldersWithEntriesTest()
      {
         DirectoryInfo dimodule = new DirectoryInfo(@"c:\_temp\mymod");
         Folder module = new Folder(dimodule, "connection string", "mymod");

         FileInfo fi1 = new FileInfo(@"c:\_temp\mymod\file1.cs");
         Entry file1 = new Entry(fi1) { Length = 1, FileContents = new byte[] { 97 } };
         module.AddItem(file1);

         DirectoryInfo disub1 = new DirectoryInfo(@"c:\_temp\mymod\sub1");
         Folder sub1 = new Folder(disub1, "connection string", "mymod/sub1");
         module.AddItem(sub1);

         FileInfo fi2 = new FileInfo(@"c:\_temp\mymod\sub1\file2.cs");
         Entry file2 = new Entry(fi2) { Length = 1, FileContents = new byte[] { 97 } };
         sub1.AddItem(file2);

         DirectoryInfo disub2 = new DirectoryInfo(@"c:\_temp\mymod\sub1\sub2");
         Folder sub2 = new Folder(disub2, "connection string", "mymod/sub1/sub2");
         sub1.AddItem(sub2);

         FileInfo fi3 = new FileInfo(@"c:\_temp\mymod\sub1\sub2\file3.cs");
         Entry file3 = new Entry(fi3) { Length = 1, FileContents = new byte[] { 97 } };
         sub2.AddItem(file3);
         PrintWorkingDirStructure(module);

         _fileReceiver.WriteToDisk(module);

         Assert.IsTrue(module.Info.Exists);
         Assert.IsTrue(file1.Info.Exists);
         Assert.IsTrue(sub1.Info.Exists);
         Assert.IsTrue(file2.Info.Exists);
         Assert.IsTrue(sub2.Info.Exists);
         Assert.IsTrue(file3.Info.Exists);
      }

      [Test]
      public void ReceiveCheckoutResponsesTest()
      {
         IList<IResponse> coresponses = new List<IResponse> { new ClearStickyResponse(), new ClearStaticDirectoryResponse() };

         IList<IResponse> responses = GetMockCheckoutResponses("8 Dec 2009 15:26:27 -0000", "mymod/", "file1.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }

         responses = GetMockCheckoutResponses("27 Nov 2009 14:21:06 -0000", "mymod/", "file2.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }

         responses = GetMockCheckoutResponses("27 Nov 2009 14:21:06 -0000", "mymod/sub1/", "file3.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }
         coresponses.Add(new OkResponse());

         _fileReceiver.ProcessCheckoutResponses(coresponses);

         Assert.AreEqual(3, _root.ModuleFolder.Count);
         Assert.IsTrue(_root.ModuleFolder.Info.Exists);
         Entry file1 = (Entry) _root.ModuleFolder[0];
         Entry file2 = (Entry) _root.ModuleFolder[1];
         Folder sub1 = (Folder) _root.ModuleFolder[2];
         Entry file3 = (Entry) sub1[0];
         Assert.IsTrue(file1.Info.Exists);
         Assert.IsTrue(file2.Info.Exists);
         Assert.IsTrue(sub1.Info.Exists);
         Assert.IsTrue(file3.Info.Exists);

      }

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

      private static IList<IResponse> GetMockCheckoutResponses(string time, string path, string file)
      {
         IList<IResponse> responses = new List<IResponse>();
         IResponse r = new ModTimeResponse();
         r.ProcessResponse(new List<string> { time });
         responses.Add(r);
         var list = (GetMockMTResponseGroup(path + file));
         foreach (IResponse response in list)
         {
            responses.Add(response);
         }
         responses.Add(GetMockUpdatedResponse(path, file));

         return responses;
      }

      private static IList<IResponse> GetMockMTResponseGroup(string fname)
      {
         IList<IResponse> responses = new List<IResponse>();
         responses.Add(new MessageTagResponse { Message = "+updated" });
         responses.Add(new MessageTagResponse { Message = "text U" });
         responses.Add(new MessageTagResponse { Message = "fname " + fname });
         responses.Add(new MessageTagResponse { Message = "newline" });
         responses.Add(new MessageTagResponse { Message = "-updated" });
         return responses;
      }

      private static UpdatedResponse GetMockUpdatedResponse(string path, string name)
      {
         UpdatedResponse res = new UpdatedResponse();
         IList<string> lines = new List<string>
                                  {
                                     "Updated " + path,
                                     "/usr/local/cvsroot/sandbox/" + path + name,
                                     "/" + name + "/1.1.1.1///",
                                     "u=rw,g=rw,o=rw",
                                     "5"
                                  };
         res.ProcessResponse(lines);
         string text = "abcde";
         res.File.Contents = text.Encode();
         return res;
      }
   }
}