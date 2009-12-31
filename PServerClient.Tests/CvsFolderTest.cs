using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.LocalFileSystem;
using Rhino.Mocks;
using Is=Rhino.Mocks.Constraints.Is;
using List=Rhino.Mocks.Constraints.List;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CVSFolderTest
   {
      [SetUp]
      public void Setup()
      {
         _rw = _mocks.DynamicMock<IReaderWriter>();
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\mymod"); // containing folder
         _cvsFolder = new CVSFolder(di, _connection, _module);
         ReaderWriter.Current = _rw;
      }

      private readonly MockRepository _mocks = new MockRepository();
      private IReaderWriter _rw;
      private CVSFolder _cvsFolder;
      private readonly string _connection = ":pserver:user@gb-aix-q:/usr/local/cvsroot/sandbox";
      private readonly string _module = "mymod";

      [Test]
      public void ConstructorTest()
      {
         Assert.AreEqual(_cvsFolder.CVSDirectory.FullName, @"c:\_temp\mymod\CVS");
         Assert.AreEqual(_cvsFolder.RootFile.FullName, @"c:\_temp\mymod\CVS\Root");
         Assert.AreEqual(_cvsFolder.EntriesFile.FullName, @"c:\_temp\mymod\CVS\Entries");
         Assert.AreEqual(_cvsFolder.RepositoryFile.FullName, @"c:\_temp\mymod\CVS\Repository");
      }

      [Test]
      public void ReadEntriesTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "/myfile/1.2/Mon Dec  7 23:15:36 2009//",
                                     "/New Text Document.txt/1.1/Mon Dec  7 23:00:01 2009//",
                                     "D/Properties////"
                                  };

         Expect.Call(_rw.ReadFileLines(null))
            .IgnoreArguments()
            .Constraints(Is.Same(_cvsFolder.EntriesFile))
            .Return(lines);

         _mocks.ReplayAll();
         IList<ICVSItem> items = _cvsFolder.ReadEntries();
         _mocks.VerifyAll();

         Assert.IsInstanceOf(typeof (Entry), items[0]);
         Assert.IsInstanceOf(typeof (Entry), items[1]);
         Assert.IsInstanceOf(typeof (Folder), items[2]);
         ICVSItem entry = items[0];
         Assert.AreEqual("myfile", entry.Info.Name);
         Assert.AreEqual("1.2", entry.Revision);
         Assert.AreEqual(DateTime.Parse("12/7/2009 11:15:36 PM"), entry.ModTime);
         ICVSItem testfolder = items[2];
         Assert.AreEqual("Properties", testfolder.Info.Name);
      }

      [Test]
      public void ReadRepositoryFile()
      {
         byte[] buffer = _module.Encode();
         string result;
         using (_mocks.Record())
         {
            Expect.Call(_rw.ReadFile(_cvsFolder.RepositoryFile)).Return(buffer);
         }
         using (_mocks.Playback())
         {
            result = _cvsFolder.ReadRepositoryFile();
         }
         Assert.AreEqual(_module, result);
      }

      [Test]
      public void ReadRootFileTest()
      {
         byte[] buffer = _connection.Encode();
         string result;
         using (_mocks.Record())
         {
            Expect.Call(_rw.ReadFile(_cvsFolder.RootFile)).Return(buffer);
         }
         using (_mocks.Playback())
         {
            result = _cvsFolder.ReadRootFile();
         }
         Assert.AreEqual(_connection, result);
      }

      [Test]
      public void WriteEntriesTest()
      {
         
         ICVSItem parentFolder = new Folder(new DirectoryInfo(@"c:\_temp|mymod"), null, "mymod", null);
         FileInfo fi1 = new FileInfo(@"c:\_temp\mymod\myfile");
         ICVSItem e1 = new Entry(fi1, parentFolder) {Revision = "1.1", ModTime = DateTime.Parse("1/1/2010 1:15:30")};
         FileInfo fi2 = new FileInfo(@"c:\_temp\mymod\New Text Document.txt");
         ICVSItem e2 = new Entry(fi2, parentFolder) {Revision = "1.2", ModTime = DateTime.Parse("1/2/2010 2:30:45 PM")};
         DirectoryInfo d1 = new DirectoryInfo(@"c:\_temp\mymod\Properties");
         ICVSItem f1 = new Folder(d1, _connection, _module, parentFolder);
         IList<ICVSItem> items = new List<ICVSItem> {e1, e2, f1};

         DirectoryInfo dir = new DirectoryInfo(@"c:\_temp\mymod");
         string t0 = "/myfile/1.1/Fri Jan  1 01:15:30 2010//";
         string t1 = "/New Text Document.txt/1.2/Sat Jan  2 14:30:45 2010//";
         string t2 = "D/Properties////";
         IList<string> test = new List<string> {t0, t1, t2};

         Expect.Call(() => _rw.WriteFileLines(null, null))
            .IgnoreArguments()
            .Constraints(Is.Same(_cvsFolder.EntriesFile), List.ContainsAll(test));

         _mocks.ReplayAll();

         _cvsFolder.WriteEntries(items);

         _mocks.VerifyAll();
      }

      [Test]
      public void WriteRepositoryFile()
      {
         using (_mocks.Record())
         {
            Expect.Call(() => _rw.WriteFile(_cvsFolder.RepositoryFile, null))
               .IgnoreArguments()
               .Constraints(Is.Same(_cvsFolder.RepositoryFile), Is.TypeOf(typeof (byte[])));
         }
         using (_mocks.Playback())
         {
            _cvsFolder.WriteRepositoryFile();
         }
      }

      [Test]
      public void WriteRootFileTest()
      {
         using (_mocks.Record())
         {
            Expect.Call(() => _rw.WriteFile(_cvsFolder.RootFile, null))
               .IgnoreArguments()
               .Constraints(Is.Same(_cvsFolder.RootFile), Is.TypeOf(typeof (byte[])));
         }
         using (_mocks.Playback())
         {
            _cvsFolder.WriteRootFile();
         }
      }
   }
}