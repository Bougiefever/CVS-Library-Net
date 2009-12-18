using System.IO;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.LocalFileSystem;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class CvsServerFileReceiverTest
   {
      private Root _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root("host-name", 1, "username", "password", "/f1/f2/f3");
         Folder f = new Folder(new DirectoryInfo(@"c:\mydir"));
         _root.WorkingDirectory = f;
      }

      [Test]
      public void SaveFolderTest()
      {
         ServerFileReceiver receiver = new ServerFileReceiver(_root);
         if (Directory.Exists(@"c:\_temp"))
            Directory.Delete(@"c:\_temp", true);
         ICVSItem working = CreateTestFolderStructure();
         receiver.SaveFolder(working);
         Assert.IsTrue(Directory.Exists(@"c:\_temp"));
         Assert.IsTrue(Directory.Exists(@"c:\_temp\module"));
         Assert.IsTrue(File.Exists(@"c:\_temp\module\file1.cs"));
         Assert.IsTrue(File.Exists(@"c:\_temp\module\file2.cs"));
         Assert.IsTrue(Directory.Exists(@"c:\_temp\module\sub1"));
         Assert.IsTrue(File.Exists(@"c:\_temp\module\sub1\myfile.txt"));

         if (Directory.Exists(@"c:\_temp"))
            Directory.Delete(@"c:\_temp", true);
      }

      private static ICVSItem CreateTestFolderStructure()
      {
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
         ICVSItem root = new Folder(di);
         ICVSItem module = new Folder(new DirectoryInfo(@"c:\_temp\module"));
         root.AddItem(module);
         ICVSItem e1 = new Entry(new FileInfo(@"c:\_temp\module\file1.cs"));
         e1.Length = 5;
         string s = "abcde";
         e1.FileContents = s.Encode();
         module.AddItem(e1);
         ICVSItem e2 = new Entry(new FileInfo(@"c:\_temp\module\file2.cs"));
         e2.Length = 4;
         s = "blah";
         e2.FileContents = s.Encode();
         module.AddItem(e2);
         ICVSItem sub1 = new Folder(new DirectoryInfo(@"c:\_temp\module\sub1"));
         module.AddItem(sub1);
         ICVSItem e3 = new Entry(new FileInfo(@"c:\_temp\module\sub1\myfile.txt"));
         s = "ABCDE";
         e3.FileContents = s.Encode();
         sub1.AddItem(e3);
         return root;
      }
   }
}