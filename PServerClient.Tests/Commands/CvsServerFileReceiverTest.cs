using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.LocalFileSystem;
using PServerClient.Responses;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class CvsServerFileReceiverTest
   {
      private Root _root;
      private Folder _working;
      private ServerFileReceiver _cfr;

      [SetUp]
      public void SetUp()
      {
          _root = new Root("host", 1, "user", "pwd", "\f1\f2\f3");
         _working = new Folder(new DirectoryInfo(@"c:\projects"));
         _root.WorkingDirectory = _working;
         _cfr = new ServerFileReceiver(_root);
      }

      [Test]
      public void SaveFolderTest()
      {
         MockRepository mocks = new MockRepository();
         ICVSItem work = mocks.DynamicMock<ICVSItem>(); // working folder
         ICVSItem module = mocks.DynamicMock<ICVSItem>(); // folder, child of working
         ICVSItem entry = mocks.DynamicMock<ICVSItem>(); // entry, child of module
         ICVSItem subFolder = mocks.DynamicMock<ICVSItem>(); // folder, child of module
         ICVSItem subEntry = mocks.DynamicMock<ICVSItem>(); // entry, child of subFolder
         _root.WorkingDirectory = work;

         IList<ICVSItem> child1 = new List<ICVSItem>{module};
         IList<ICVSItem> child2 = new List<ICVSItem>{entry, subFolder};
         IList<ICVSItem> child3 = new List<ICVSItem>{subEntry};
         using (mocks.Record())
         {
            //Expect.Call(work.ItemType).Return(ItemType.Folder);
            Expect.Call(module.ItemType).Return(ItemType.Folder);
            Expect.Call(entry.ItemType).Return(ItemType.Entry);
            Expect.Call(subFolder.ItemType).Return(ItemType.Folder);
            Expect.Call(subEntry.ItemType).Return(ItemType.Entry);

            Expect.Call(work.ChildItems).Return(child1).Repeat.Any();
            Expect.Call(module.ChildItems).Return(child2).Repeat.Any();
            Expect.Call(subFolder.ChildItems).Return(child3).Repeat.Any();

            Expect.Call(work.Write).Repeat.Once();
            Expect.Call(module.Write).Repeat.Once();
            Expect.Call(entry.Write).Repeat.Once();
            Expect.Call(subFolder.Write).Repeat.Once();
            Expect.Call(subEntry.Write).Repeat.Once();
         }
         using (mocks.Playback())
         {
            ServerFileReceiver receiver = new ServerFileReceiver(_root);
            receiver.SaveFolder(work);
         }
      }

      [Test]
      public void CreateFolderStructureTest()
      {
         string[] folders = new[] { "module", "sub1", "sub2" };
         _cfr.CreateFolderStructure(_working, folders);
         Folder f = (Folder)_working.ChildItems[0].ChildItems[0];
         Assert.AreEqual("sub1", f.Name);
         f = (Folder)f.ChildItems[0];
         Assert.AreEqual("sub2", f.Name);

         folders = new[] { "module", "sub1", "sub2", "sub3" };
         _cfr.CreateFolderStructure(_working, folders);

         folders = new[] { "module", "sub1", "sub22" };
         _cfr.CreateFolderStructure(_working, folders);
         f = (Folder)_working.ChildItems[0].ChildItems[0].ChildItems[1];
         Assert.AreEqual("sub22", f.Name);

         f = (Folder)_working.ChildItems[0].ChildItems[0].ChildItems[0].ChildItems[0];
         Assert.AreEqual("sub3", f.Name);
         PrintWorkingDirStructure(_working);
      }

      [Test]
      public void AddNewEntryTest()
      {
         ModTimeResponse mod = new ModTimeResponse();
         mod.ProcessResponse(new List<string> {"27 Nov 2009 14:21:06 -0000"});
         MessageTagResponse mt = new MessageTagResponse {Message = "fname module/file1.cs"};
         UpdatedResponse udr = GetMockUpdatedResponse("module/", "file1.cs");

         IList<IResponse> responses = new List<IResponse> {mod, mt, udr};

         _cfr.AddNewEntry(responses);
         ICVSItem module = _working.ChildItems[0];
         Assert.IsInstanceOf(typeof(Folder), module);
         Assert.AreEqual("module", module.Name);
         ICVSItem entry = module.ChildItems[0];
         Assert.IsInstanceOf(typeof(Entry), entry);
         Assert.AreEqual("file1.cs", entry.Name);
      }

      [Test]
      public void ReceiveMTResponsesTest()
      {
         IList<IResponse> coresponses = new List<IResponse> { new ClearStickyResponse(), new ClearStaticDirectoryResponse() };

         // file1.cs
         IList<IResponse> responses = GetMockCheckoutResponses("8 Dec 2009 15:26:27 -0000", "abougie/", "file1.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }

         responses = GetMockCheckoutResponses("27 Nov 2009 14:21:06 -0000", "abougie/", "file1.cs");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }

         responses = GetMockCheckoutResponses("27 Nov 2009 14:21:06 -0000", "abougie/cvstest/", "NewTestApp.csproj");
         foreach (IResponse response in responses)
         {
            coresponses.Add(response);
         }
         coresponses.Add(new OkResponse());

         _cfr.ReceiveMTUpdatedResponses(coresponses);
         PrintWorkingDirStructure(_root.WorkingDirectory);
      }

      private static void PrintWorkingDirStructure(ICVSItem working)
      {
         Console.Write("(d)");
         Console.WriteLine(working.Item.FullName);
         foreach (ICVSItem item in working.ChildItems)
         {
            if (item.ItemType == ItemType.Folder)
               PrintWorkingDirStructure(item);
            else
               Console.WriteLine("(f)" + item.Item.FullName);
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
         res.File.FileContents = text.Encode();
         return res;
      }

      private static ICVSItem CreateMockFolderStructure()
      {
         DirectoryInfo di = new DirectoryInfo(@"c:\projects");
         ICVSItem root = new Folder(di);
         ICVSItem module = new Folder(new DirectoryInfo(@"c:\projects\module"));
         root.AddItem(module);
         module.AddItem(new Entry(new FileInfo(@"c:\projects\module\file1.cs")));
         module.AddItem(new Entry(new FileInfo(@"c:\projects\module\file1.cs")));
         ICVSItem sub1 = new Folder(new DirectoryInfo(@"c:\projects\module\sub1"));
         module.AddItem(sub1);
         sub1.AddItem(new Entry(new FileInfo(@"c:\projects\module\sub1\myfile.txt")));
         return root;
      }
   }
}