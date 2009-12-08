using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace PServerClient.LocalFileSystem
{
   public class CvsFolder
   {
      private const string EntryRegex = @"(D?)/([^/]+)/([^/]*)/([^/]*)/([^/]*)/([^/]*)";
      private ICvsItem _parent;

      public CvsFolder(ICvsItem parent)
      {
         // create objects
         _parent = parent;
         Directory = new DirectoryInfo(Path.Combine(parent.Item.FullName, "CVS"));
         RepositoryFile = new FileInfo(Path.Combine(Directory.FullName, "Repository"));
         EntriesFile = new FileInfo(Path.Combine(Directory.FullName, "Entries"));
         RootFile = new FileInfo(Path.Combine(Directory.FullName, "Root"));

         // create file system folder for CVS folder if it doesn't exist
         ReaderWriter.Current.CreateDirectory(Directory);
      }

      //public ICvsItem ParentFolder { get; private set; }
      public DirectoryInfo Directory { get; private set; }
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

      public IList<ICvsItem> GetEntryItems()
      {
         IList<string> entryLines = ReaderWriter.Current.ReadFileLines(EntriesFile);
         IList<ICvsItem> items = new List<ICvsItem>();
         foreach (string s in entryLines)
         {
            Match m = Regex.Match(s, EntryRegex);
            if (m.Success)
            {
               string code = m.Groups[1].ToString();
               string fileName = m.Groups[1].ToString();
               string revision = m.Groups[2].ToString();
               string date = m.Groups[3].ToString();
               string keywordMode = m.Groups[4].ToString();
               string stickyOption = m.Groups[5].ToString();

               FileInfo file = new FileInfo(Path.Combine(_parent.Item.FullName, fileName));
               ICvsItem item;
               if (code == "D")
                  item = new Folder(file);
               else
                  item = new Entry(file)
                            {
                               Revision = revision,
                               ModTime = date.Rfc822ToDateTime(),
                               Properties = keywordMode
                            };
               items.Add(item);
            }

         }
         return items;
      }

      public void SaveEntriesFile(IList<ICvsItem> items)
      {
         IList<string> lines = new List<string>();
         foreach (ICvsItem item in items)
         {
            string entryLine;
            if (item.ItemType == CvsItemType.Entry)
            {
               entryLine = string.Format("/{0}/{1}/{2}/{3}/", item.Item.Name, item.Revision,
                                                item.ModTime.ToEntryFileDateTimeFormat(), item.Properties);
            }
            else
            {
               entryLine = string.Format("D/{0}////", item.Item.Name);
            }
            lines.Add(entryLine);
         }
         ReaderWriter.Current.WriteFileLines(EntriesFile, lines);
      }
   }
}
