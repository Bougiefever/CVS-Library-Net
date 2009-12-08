using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.LocalFileSystem;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ReaderWriterTest
   {
      [Test]
      public void ReadWriteFileDirectoryTest()
      {
         DirectoryInfo dir = new DirectoryInfo(@"c:\_junk\rwtesting");
         if (dir.Exists)
            dir.Delete(true);
         dir.Refresh();
         ReaderWriter rw = new ReaderWriter();

         Assert.IsFalse(rw.Exists(dir));
         rw.CreateDirectory(dir);
         Assert.IsTrue(rw.Exists(dir));

         string blah = "blah";
         string fileName = "blah.txt";

         FileInfo file = new FileInfo(Path.Combine(dir.FullName, fileName));
         Assert.IsFalse(rw.Exists(file));
         rw.WriteFile(file, blah.Encode());
         Assert.IsTrue(rw.Exists(file));
         byte[] buffer = rw.ReadFile(file);
         string result = buffer.Decode();
         Assert.AreEqual(blah, result);
         rw.Delete(file);
         Assert.IsFalse(file.Exists);
         rw.Delete(dir);
         Assert.IsFalse(dir.Exists);
      }

      [Test]
      public void ReadLinesTest()
      {
         Directory.CreateDirectory(@"c:\_junk\rwtesting\CVS");
         FileInfo file = new FileInfo(@"c:\_junk\rwtesting\CVS\Entries");
         TextWriter writer = file.CreateText();
         writer.WriteLine("/New Text Document.txt/1.1/Mon Dec  7 23:00:01 2009//");
         writer.WriteLine("/myfile/1.2/Mon Dec  7 23:15:36 2009//");
         writer.Flush();
         writer.Close();

         ReaderWriter rw = new ReaderWriter();
         IList<string> lines = rw.ReadFileLines(file);
         Assert.AreEqual(2, lines.Count);
      }

      [Test]
      public void WriteLinesTest()
      {
         Directory.CreateDirectory(@"c:\_junk\rwtesting\CVS");
         FileInfo file = new FileInfo(@"c:\_junk\rwtesting\CVS\Entries");
         IList<string> text = new List<string>
                                 {
                                    "/New Text Document.txt/1.1/Mon Dec  7 23:00:01 2009//",
                                    "/myfile/1.2/Mon Dec  7 23:15:36 2009//"
                                 };
         ReaderWriter rw = new ReaderWriter();
         rw.WriteFileLines(file, text);

         TextReader tr = file.OpenText();
         string l1 = tr.ReadLine();
         string l2 = tr.ReadLine();
         tr.Close();

         Assert.AreEqual("/New Text Document.txt/1.1/Mon Dec  7 23:00:01 2009//", l1);
         Assert.AreEqual("/myfile/1.2/Mon Dec  7 23:15:36 2009//", l2);
      }
   }
}
