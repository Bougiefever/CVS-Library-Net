using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.LocalFileSystem;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Is=NUnit.Framework.Is;
using Text=Rhino.Mocks.Constraints.Text;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CvsFolderTest
   {


      [Test]
      public void GetEntryItemsTest()
      {
         IList<string> lines = new List<string>()
                                  {
                                     "/myfile/1.2/Mon Dec  7 23:15:36 2009//",
                                     "/New Text Document.txt/1.1/Mon Dec  7 23:00:01 2009//",
                                     "D/Properties////"
                                  };
         MockRepository mocks = new MockRepository();
         IReaderWriter rw = mocks.DynamicMock<IReaderWriter>();
         ReaderWriter.Current = rw;

         Expect.Call(rw.ReadFileLines(null)).IgnoreArguments().Return(lines);

         DirectoryInfo dir = new DirectoryInfo(@"c:\_junk\rwtesting");
         
         mocks.ReplayAll();
         ICvsItem folder = new Folder(dir);
         CvsFolder cvsFolder = new CvsFolder(folder);
         IList<ICvsItem> items = cvsFolder.GetEntryItems();
         mocks.VerifyAll();
         Assert.IsInstanceOf(typeof(Entry), items[0]);
         Assert.IsInstanceOf(typeof(Entry), items[1]);
         Assert.IsInstanceOf(typeof(Folder), items[2]);
         ICvsItem entry = items[0];
         Assert.AreEqual("myfile", entry.Item.Name);
         Assert.AreEqual("1.2", entry.Revision);
         Assert.AreEqual(DateTime.Parse("12/7/2009 11:15:36 PM"), entry.ModTime);
         ICvsItem testfolder = items[2];
         Assert.AreEqual("Properties", testfolder.Item.Name);
      }

      [Test]
      public void WriteEntriesTest()
      {
         FileInfo fi1 = new FileInfo(@"c:\_junk\rwtesting\CVS\myfile");
         ICvsItem e1 = new Entry(fi1) {Revision = "1.1", ModTime = DateTime.Parse("1/1/2010 1:15:30")};
         FileInfo fi2 = new FileInfo(@"c:\_junk\rwtesting\CVS\New Text Document.txt");
         ICvsItem e2 = new Entry(fi2) {Revision = "1.2", ModTime = DateTime.Parse("1/2/2010 2:30:45 PM")};
         DirectoryInfo d1 = new DirectoryInfo(@"c:\_junk\rwtesting\Properties");
         ICvsItem f1 = new Folder(d1);
         IList<ICvsItem> items = new List<ICvsItem> {e1, e2, f1};

         MockRepository mocks = new MockRepository();
         IReaderWriter rw = mocks.DynamicMock<IReaderWriter>();
         ReaderWriter.Current = rw;

         DirectoryInfo dir = new DirectoryInfo(@"c:\_junk\rwtesting");
         ICvsItem folder = new Folder(dir);
         string t1 = "/New Text Document.txt/1.2/Sat Jan  2 14:30:45 2010//";
         string t2 = "D/Properties////";
         Expect.Call(() => rw.WriteFileLines(null, null))
            .IgnoreArguments()
            .Constraints(Rhino.Mocks.Constraints.Is.Anything(),
                         Rhino.Mocks.Constraints.List.Element(1, Rhino.Mocks.Constraints.Is.Equal(t1))
                         && Rhino.Mocks.Constraints.List.Element(2, Rhino.Mocks.Constraints.Is.Equal(t2)));
         
         mocks.ReplayAll();
        
         CvsFolder cvsFolder = new CvsFolder(folder);
         cvsFolder.SaveEntriesFile(items);
         
         mocks.VerifyAll();
      }
   }
}
