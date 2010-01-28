using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   using Rhino.Mocks;
   using Rhino.Mocks.Constraints;

   /// <summary>
   /// Test of CVS
   /// </summary>
   [TestFixture]
   public class CVSTest
   {
      private readonly string _connection = ":pserver:username@host-name:2401/usr/local/cvsroot/sandbox";
      private readonly string _repository = "/usr/local/cvsroot/sandbox";
      private readonly string _module = "mymod";
      private MockRepository _mocks;
      private IReaderWriter _readerWriter;
      private Folder _rootModuleFolder;
      private Entry _file1;
      private Entry _proj;
      private Folder _props;
      private Entry _file;
      private Entry _binary;
      private IList<string> _entries;


      [SetUp]
      public void SetUp()
      {
         _entries = new List<string>
                                    {
                                       "/file1.cs/1.1/Tue Jan  5 15:47:45 2010//",
                                       "/file2.cs/1.1/Tue Jan  5 15:47:45 2010//",
                                       "/file3.cs/1.3/Thu Jan  7 15:49:20 2010//",
                                       "/TestApp.csproj/1.1/Tue Jan  5 15:47:45 2010//",
                                       "D/Properties////",
                                       "/.gitignore/1.1/Tue Jan  5 15:47:45 2010//",
                                       "/AssemblyVersionIncrementor.dll/1.1/Tue Jan  5 15:47:45 2010/-kb/"
                                    };
         _mocks = new MockRepository();
         _readerWriter = _mocks.DynamicMock<IReaderWriter>();
         ReaderWriter.Current = _readerWriter;
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\working\team\mymod");
         _rootModuleFolder = new Folder(di, _connection, _repository, _module);
         _file1 = new Entry("file1.cs", _rootModuleFolder) { EntryLine = _entries[0] };
         _proj = new Entry("TestApp.csproj", _rootModuleFolder) { EntryLine = _entries[3] };
         _props = new Folder("Properties", _rootModuleFolder) { EntryLine = _entries[4] };
         _file = new Entry(".gitignore", _rootModuleFolder) { EntryLine = _entries[5] };
         _binary = new Entry("AssemblyVersionIncrementor.dll", _rootModuleFolder) { EntryLine = _entries[6] };

      }

      [Test]
      public void RootTest()
      {
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         root.WorkingDirectory = TestConfig.WorkingDirectory;
         root.Repository = _repository;
         Assert.AreEqual(_connection, root.CVSConnectionString);
      }

      [Test]
      public void FolderTest()
      {
         // starting module folder
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\working\team\mymod");
         Folder rootModuleFolder = new Folder(di, _connection, _repository, _module);
         Assert.IsNull(rootModuleFolder.Parent);
         Assert.AreEqual("mymod", rootModuleFolder.Module);
         Assert.AreEqual(_connection, rootModuleFolder.Connection);
         Assert.AreEqual(_repository, rootModuleFolder.Repository);
         CVSFolder cvsfolder = rootModuleFolder.CVSFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\CVS", cvsfolder.CVSDirectory.FullName);

         // add sub folder
         di = new DirectoryInfo(@"c:\_temp\working\team\mymod\project1");
         Folder project1 = new Folder("project1", rootModuleFolder);
         Assert.AreSame(rootModuleFolder, project1.Parent);
         Assert.AreEqual("mymod/project1", project1.Module);
         Assert.AreEqual(_connection, project1.Connection);
         Assert.AreEqual(_repository, project1.Repository);
         cvsfolder = project1.CVSFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\project1\CVS", cvsfolder.CVSDirectory.FullName);
         Assert.AreEqual(1, rootModuleFolder.Count);
         Assert.AreSame(project1, rootModuleFolder[0]);

         // add sub-sub folder
         di = new DirectoryInfo(@"c:\_temp\working\team\mymod\project1\Properties");
         Folder prop = new Folder("Properties", project1);
         Assert.AreEqual("mymod/project1/Properties", prop.Module);
         Assert.AreSame(project1, prop.Parent);
         cvsfolder = prop.CVSFolder;
         Assert.AreEqual(@"c:\_temp\working\team\mymod\project1\Properties\CVS", cvsfolder.CVSDirectory.FullName);
         Assert.AreEqual(1, project1.Count);
         Assert.AreSame(prop, project1[0]);
      }

      [Test]
      public void EntryTest()
      {
         // starting module folder
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp\working\team\mymod");
         Folder rootModuleFolder = new Folder(di, _connection, _repository, _module);
         Entry file1 = new Entry("file1.cs", rootModuleFolder);
         Assert.AreSame(rootModuleFolder, file1.Parent);
         Assert.AreSame(file1.CVSFolder, rootModuleFolder.CVSFolder);
         Assert.AreEqual(@"c:\_temp\working\team\mymod\file1.cs", file1.Info.FullName);
      }

      [Test]
      public void WriteEntryWhenEntryLineIsInFileTest()
      {
         string newEntryLine = "/TestApp.csproj/1.2/Mon Jan  4 10:47:45 2010//";
         _proj.EntryLine = newEntryLine;

        using (_mocks.Record())
         {
            Expect.Call(_readerWriter.ReadFileLines(null))
               .IgnoreArguments()
               .Return(_entries);
            Expect.Call(() => _readerWriter.WriteFileLines(null, null))
               .IgnoreArguments()
               .Constraints(
               Rhino.Mocks.Constraints.Is.Anything(),
               Rhino.Mocks.Constraints.List.Element(3, Rhino.Mocks.Constraints.Text.Contains("1.2")) && Rhino.Mocks.Constraints.List.Count(Rhino.Mocks.Constraints.Is.Equal(7)));
         }

         using (_mocks.Playback())
         {
            CVSFolder cvsFolder = _rootModuleFolder.CVSFolder;
            cvsFolder.WriteEntry(_proj);
         }
      }

      [Test]
      public void WriteEntryLineWhenLineIsNotInFileTest()
      {
         _entries.RemoveAt(3);
         string newEntryLine = "/TestApp.csproj/1.2/Mon Jan  4 10:47:45 2010//";
         _proj.EntryLine = newEntryLine;

         using (_mocks.Record())
         {
            Expect.Call(_readerWriter.ReadFileLines(null))
               .IgnoreArguments()
               .Return(_entries);
            Expect.Call(() => _readerWriter.WriteFileLines(null, null))
               .IgnoreArguments()
               .Constraints(
               Is.Anything(),
               List.Element(6, Text.Contains("1.2")) && List.Count(Is.Equal(7)));
         }

         using (_mocks.Playback())
         {
            CVSFolder cvsFolder = _rootModuleFolder.CVSFolder;
            cvsFolder.WriteEntry(_proj);
         }
      }

      [Test]
      public void SaveEntriesFileSavesAllEntryLinesTest()
      {
         using (_mocks.Record())
         {
            Expect.Call(() => _readerWriter.WriteFileLines(null, null))
               .IgnoreArguments()
               .Constraints(Is.Anything(), List.Count(Is.Equal(5)) && List.Element(1, Text.StartsWith("/TestApp.csproj")));
         }

         using (_mocks.Playback())
         {
            CVSFolder folder = _rootModuleFolder.CVSFolder;
            folder.WriteEntries();
         }
      }
   }
}