using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   public class CVSFolder
   {
      private const string EntryRegex = @"(D?)/([^/]+)/([^/]*)/([^/]*)/([^/]*)/([^/]*)";
      private readonly ICVSItem _parent;
      private readonly string _cvsRoot;
      private readonly string _cvsModule;

      public CVSFolder(ICVSItem parent, string cvsRoot, string cvsModule)
      {
         // create objects
         _parent = parent;
         CVSDirectory = new DirectoryInfo(Path.Combine(parent.Item.FullName, "CVS"));
         RepositoryFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Repository"));
         EntriesFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Entries"));
         RootFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Root"));
         _cvsRoot = cvsRoot;
         _cvsModule = cvsModule;
      }

      public DirectoryInfo CVSDirectory { get; private set; }
      public FileInfo RepositoryFile { get; private set; }
      public FileInfo EntriesFile { get; private set; }
      public FileInfo RootFile { get; private set; }

      public string GetRootString()
      {
         byte[] buffer = ReaderWriter.Current.ReadFile(RootFile);
         string root = buffer.Decode();
         return root;
      }

      public void WriteRootFile(string root)
      {
         byte[] buffer = root.Encode();
         ReaderWriter.Current.WriteFile(RootFile, buffer);
      }

      public string GetRepositoryString()
      {
         byte[] buffer = ReaderWriter.Current.ReadFile(RepositoryFile);
         string repository = buffer.Decode();
         return repository;
      }

      public void WriteRepositoryFile(string repository)
      {
         byte[] buffer = repository.Encode();
         ReaderWriter.Current.WriteFile(RepositoryFile, buffer);
      }

      public IList<ICVSItem> GetEntryItems()
      {
         IList<string> entryLines = ReaderWriter.Current.ReadFileLines(EntriesFile);
         IList<ICVSItem> items = new List<ICVSItem>();
         foreach (string s in entryLines)
         {
            Match m = Regex.Match(s, EntryRegex);
            if (m.Success)
            {
               string code = m.Groups[1].ToString();
               string fileName = m.Groups[2].ToString();
               string revision = m.Groups[3].ToString();
               string date = m.Groups[4].ToString();
               string keywordMode = m.Groups[5].ToString();
               string stickyOption = m.Groups[6].ToString();

               FileInfo file = new FileInfo(Path.Combine(_parent.Item.FullName, fileName));
               ICVSItem item;
               if (code == "D")
                  item = new Folder(file, _cvsRoot, _cvsModule);
               else
                  item = new Entry(file)
                            {
                               Revision = revision,
                               ModTime = date.EntryToDateTime(),
                               Properties = keywordMode,
                               StickyOption = stickyOption
                            };
               items.Add(item);
            }
         }
         return items;
      }

      public void SaveEntriesFile(IList<ICVSItem> items)
      {
         IList<string> lines = new List<string>();
         foreach (ICVSItem item in items)
         {
            string code = item.ItemType == ItemType.Folder ? "D" : string.Empty;
            string entryLine = string.Format("{4}/{0}/{1}/{2}/{3}/{5}", item.Item.Name, item.Revision,
                                         item.ModTime.ToEntryString(), item.Properties, code, item.StickyOption);
            Console.WriteLine(entryLine);
            lines.Add(entryLine);
         }
         ReaderWriter.Current.WriteFileLines(EntriesFile, lines);
      }

      public void SaveCVSFolder(IList<ICVSItem> items)
      {
         // create CVS folder if it doesn't exist
         ReaderWriter.Current.CreateDirectory(CVSDirectory);
         WriteRootFile(_cvsRoot);
         WriteRepositoryFile(_cvsModule);
         SaveEntriesFile(items);
      }
   }
}
