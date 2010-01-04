using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// The CVS folder that belongs to each repository folder
   /// </summary>
   public class CVSFolder
   {
      private const string EntryRegex = @"(D?)/([^/]+)/([^/]*)/([^/]*)/([^/]*)/([^/]*)";
      private readonly Folder _parent;

      public CVSFolder(Folder parentFolder)
      {
         _parent = parentFolder;
         CVSDirectory = new DirectoryInfo(Path.Combine(parentFolder.Info.FullName, "CVS"));
         RepositoryFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Repository"));
         EntriesFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Entries"));
         RootFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Root"));
      }

      public DirectoryInfo CVSDirectory { get; private set; }
      public FileInfo RepositoryFile { get; private set; }
      public FileInfo EntriesFile { get; private set; }
      public FileInfo RootFile { get; private set; }

      public string ReadRootFile()
      {
         byte[] buffer = ReaderWriter.Current.ReadFile(RootFile);
         string root = buffer.Decode();
         return root;
      }

      public void WriteRootFile()
      {
         byte[] buffer = _parent.Connection.Encode();
         ReaderWriter.Current.WriteFile(RootFile, buffer);
      }

      public string ReadRepositoryFile()
      {
         byte[] buffer = ReaderWriter.Current.ReadFile(RepositoryFile);
         string repository = buffer.Decode();
         return repository;
      }

      public void WriteRepositoryFile()
      {
         byte[] buffer = _parent.Repository.Encode();
         ReaderWriter.Current.WriteFile(RepositoryFile, buffer);
      }

      public IList<ICVSItem> ReadEntries()
      {
         IList<string> entryLines = ReaderWriter.Current.ReadFileLines(EntriesFile);
         IList<ICVSItem> items = new List<ICVSItem>();
         //foreach (string s in entryLines)
         //{
         //   Match m = Regex.Match(s, EntryRegex);
         //   if (m.Success)
         //   {
         //      string code = m.Groups[1].ToString();
         //      string entryName = m.Groups[2].ToString();
         //      string revision = m.Groups[3].ToString();
         //      string date = m.Groups[4].ToString();
         //      string keywordMode = m.Groups[5].ToString();
         //      string stickyOption = m.Groups[6].ToString();

         //      ICVSItem item;
         //      string path = Path.Combine(_parent.Info.FullName, entryName);
         //      if (code == "D")
         //      {
         //         DirectoryInfo di = new DirectoryInfo(path);
         //         string repo = _parent.Repository + "/" + entryName;
         //         item = new Folder(_parent);
         //      }
         //      else
         //      {
         //         FileInfo fi = new FileInfo(path);
         //         item = new Entry(fi, _parent)
         //                   {
         //                      Revision = revision,
         //                      ModTime = date.EntryToDateTime(),
         //                      Properties = keywordMode,
         //                      StickyOption = stickyOption
         //                   };
         //      }
         //      items.Add(item);
         //   }
         //}
         return items;
      }

      public void WriteEntries(IList<ICVSItem> items)
      {
         //IList<string> lines = new List<string>();
         //foreach (ICVSItem item in items)
         //{
         //   string code = item is Folder ? "D" : string.Empty;
         //   string entryLine = string.Format("{4}/{0}/{1}/{2}/{3}/{5}", item.Info.Name, item.Revision,
         //                                    item.ModTime.ToEntryString(), item.Properties, code, item.StickyOption);
         //   Console.WriteLine(entryLine);
         //   lines.Add(entryLine);
         //}
         //ReaderWriter.Current.WriteFileLines(EntriesFile, lines);
      }

      public void SaveCVSFolder(IList<ICVSItem> items)
      {
         //// create CVS folder if it doesn't exist
         //ReaderWriter.Current.CreateDirectory(CVSDirectory);
         //WriteRootFile();
         //WriteRepositoryFile();
         //WriteEntries(items);
      }
   }
}