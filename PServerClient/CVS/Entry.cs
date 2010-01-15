using System;
using System.IO;
using System.Text.RegularExpressions;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// Represents a cvs file in the repository
   /// </summary>
   public class Entry : CVSItemBase
   {
      private string _entryLineRegex = @"(?<code>D?)/(?<name>[^/]+)/(?<revision>[^/]*)/(?<date>[^/]*)/(?<keyword>[^/]*)/(?<sticky>[^/]*)";

      private string _revision;

      /// <summary>
      /// Initializes a new instance of the Entry class.
      /// </summary>
      /// <param name="name">File name for new entry</param>
      /// <param name="parent">Parent Folder instance</param>
      public Entry(string name, Folder parent)
         : base(parent)
      {
         FileInfo fi = new FileInfo(Path.Combine(parent.Info.FullName, name));
         Info = fi;
      }

      public Entry(string name, Folder parent, DateTime modTime, string revision, string properties, string stickyOption)
         : base(parent)
      {
         ModTime = modTime;
         Revision = revision;
         Properties = properties;
         StickyOption = stickyOption;
         FileInfo fi = new FileInfo(Path.Combine(parent.Info.FullName, name));
         Info = fi;
      }

      public override CVSFolder CVSFolder
      {
         get
         {
            return Parent.CVSFolder;
         }
      }

      public DateTime ModTime { get; set; }

      public string Revision
      {
         get
         {
            _revision = string.Empty;
            Match m = Regex.Match(EntryLine, _entryLineRegex);
            if (m.Success)
               _revision = m.Groups["revision"].Value;
            return _revision;
         }

         set
         {
            _revision = value;
         }
      }

      public string Properties { get; set; }

      public string StickyOption { get; set; }

      public long Length { get; set; }

      public byte[] FileContents { get; set; }
      
      public FileType Type { get; set; }

      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo) Info);
      }

      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo) Info, FileContents);
         FileContents = new byte[Length]; // clear the array to save memory
      }

      public override void Save(bool recursive)
      {
         if (recursive)
         {
            Folder parent = Parent;
            do
            {
               parent.Save(false);
               parent = parent.Parent;
            }
            while (parent != null);
         }

         Write();
      }

      public void WriteCVSEntryLine()
      {
         CVSFolder.WriteEntry(this);
      }
   }
}