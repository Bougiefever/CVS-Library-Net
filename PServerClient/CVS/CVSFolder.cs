using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace PServerClient.CVS
{
   /// <summary>
   /// The CVS folder that belongs to each repository folder
   /// </summary>
   public class CVSFolder
   {
      ////private string _entryRegex = @"(D?)/([^/]+)/([^/]*)/([^/]*)/([^/]*)/([^/]*)";
      private readonly Folder _parent;

      /// <summary>
      /// Initializes a new instance of the <see cref="CVSFolder"/> class.
      /// </summary>
      /// <param name="parentFolder">The parent folder.</param>
      public CVSFolder(Folder parentFolder)
      {
         _parent = parentFolder;
         CVSDirectory = new DirectoryInfo(Path.Combine(parentFolder.Info.FullName, "CVS"));
         RepositoryFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Repository"));
         EntriesFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Entries"));
         RootFile = new FileInfo(Path.Combine(CVSDirectory.FullName, "Root"));
      }

      /// <summary>
      /// Gets the CVS directory.
      /// </summary>
      /// <value>The CVS directory location.</value>
      public DirectoryInfo CVSDirectory { get; private set; }

      /// <summary>
      /// Gets the Repository file.
      /// </summary>
      /// <value>The Repository file location.</value>
      public FileInfo RepositoryFile { get; private set; }

      /// <summary>
      /// Gets the Entries file.
      /// </summary>
      /// <value>The Entries file.</value>
      public FileInfo EntriesFile { get; private set; }

      /// <summary>
      /// Gets the Root file.
      /// </summary>
      /// <value>The root file.</value>
      public FileInfo RootFile { get; private set; }

      /// <summary>
      /// Reads the Root file.
      /// </summary>
      /// <returns>string contents of the Root file</returns>
      public string ReadRootFile()
      {
         byte[] buffer = ReaderWriter.Current.ReadFile(RootFile);
         string root = buffer.Decode();
         return root;
      }

      /// <summary>
      /// Writes the Root file.
      /// </summary>
      public void WriteRootFile()
      {
         byte[] buffer = _parent.Connection.Encode();
         ReaderWriter.Current.WriteFile(RootFile, buffer);
      }

      /// <summary>
      /// Reads the Repository file.
      /// </summary>
      /// <returns>string contents of the repository file</returns>
      public string ReadRepositoryFile()
      {
         byte[] buffer = ReaderWriter.Current.ReadFile(RepositoryFile);
         string repository = buffer.Decode();
         return repository;
      }

      /// <summary>
      /// Writes the Repository file.
      /// </summary>
      public void WriteRepositoryFile()
      {
         byte[] buffer = _parent.Repository.Encode();
         ReaderWriter.Current.WriteFile(RepositoryFile, buffer);
      }

      /// <summary>
      /// Reads the Entries lines from the Entries file and creates an ICVSItem instance
      /// for each line in the file
      /// </summary>
      /// <returns>list of cvs items</returns>
      public IList<ICVSItem> ReadEntries()
      {
         ////IList<string> entryLines = ReaderWriter.Current.ReadFileLines(EntriesFile);
         IList<ICVSItem> items = new List<ICVSItem>();

         ////foreach (string s in entryLines)
         ////{
         ////   Match m = Regex.Match(s, EntryRegex);
         ////   if (m.Success)
         ////   {
         ////      string code = m.Groups[1].ToString();
         ////      string entryName = m.Groups[2].ToString();
         ////      string revision = m.Groups[3].ToString();
         ////      string date = m.Groups[4].ToString();
         ////      string keywordMode = m.Groups[5].ToString();
         ////      string stickyOption = m.Groups[6].ToString();

         ////      ICVSItem item;
         ////      string path = Path.Combine(_parent.Info.FullName, entryName);
         ////      if (code == "D")
         ////      {
         ////         DirectoryInfo di = new DirectoryInfo(path);
         ////         string repo = _parent.Repository + "/" + entryName;
         ////         item = new Folder(_parent);
         ////      }
         ////      else
         ////      {
         ////         FileInfo fi = new FileInfo(path);
         ////         item = new Entry(fi, _parent)
         ////                   {
         ////                      Revision = revision,
         ////                      ModTime = date.EntryToDateTime(),
         ////                      Properties = keywordMode,
         ////                      StickyOption = stickyOption
         ////                   };
         ////      }
         ////      items.Add(item);
         ////   }
         ////}
         return items;
      }

      /// <summary>
      /// Writes the entries in the ICVSItem collection of the parent Folder
      /// to the CVS Entries file
      /// </summary>
      public void WriteEntries()
      {
         IList<string> lines = new List<string>();
         foreach (ICVSItem item in _parent)
            lines.Add(item.EntryLine);

         ReaderWriter.Current.WriteFileLines(EntriesFile, lines);
      }

      /// <summary>
      /// Saves the CVS folder.
      /// </summary>
      public void SaveCVSFolder()
      {
         // create CVS folder if it doesn't exist
         ReaderWriter.Current.CreateDirectory(CVSDirectory);
         WriteRootFile();
         WriteRepositoryFile();
      }

      /// <summary>
      /// Writes one Entry to the CVS Entries file.
      /// If the entry is in the file, it updates the line.
      /// Otherwise, it adds it to the end of the file.
      /// </summary>
      /// <param name="entry">The entry.</param>
      public void WriteEntry(ICVSItem entry)
      {
         // Read file
         IList<string> readLines = ReaderWriter.Current.ReadFileLines((FileInfo) entry.Info);
         ////using (TextReader reader = new StreamReader(EntriesFile.Open(FileMode.Open, FileAccess.Read)))
         ////{
         ////   string line = string.Empty;
         ////   while (line != null)
         ////   {
         ////      line = reader.ReadLine();
         ////      readLines.Add(line);
         ////   }

         ////   reader.Close();
         ////}

         IList<string> writeLines = new List<string>();

         // Find entry line and update
         bool lineFound = false;
         foreach (var line in readLines)
         {
            string writeLine;
            string regex = PServerHelper.GetEntryFilenameRegex(entry.Info.Name);
            Match m = Regex.Match(line, regex);
            if (m.Success)
            {
               writeLine = entry.EntryLine;
               lineFound = true;
            }
            else
               writeLine = line;
            writeLines.Add(writeLine);
         }

         if (!lineFound)
            writeLines.Add(entry.EntryLine);

         // Save updated lines to file
         ReaderWriter.Current.WriteFileLines((FileInfo) entry.Info, writeLines);
         ////using (TextWriter writer = new StreamWriter(EntriesFile.Open(FileMode.Create, FileAccess.Write)))
         ////{
         ////   foreach (var line in writeLines)
         ////   {
         ////      writer.WriteLine(line);
         ////   }

         ////   writer.Flush();
         ////   writer.Close();
         ////}
      }
   }
}