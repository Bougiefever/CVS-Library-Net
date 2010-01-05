using System;
using System.IO;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// Represents a cvs file in the repository
   /// </summary>
   public class Entry : CVSItemBase
   {
      /// <summary>
      /// Constructor for entry
      /// </summary>
      /// <param name="name">File name</param>
      /// <param name="parent">Parent Folder instance</param>
      public Entry(string name, Folder parent) : base(parent)
      {
         Revision = string.Empty;
         Properties = string.Empty;
         StickyOption = string.Empty;
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

      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo) Info);
      }

      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo) Info, FileContents);
         FileContents = new byte[Length]; // clear the array to save memory
      }

      public override CVSFolder CVSFolder
      {
         get { return Parent.CVSFolder; }
      }

      public override void Save(bool recursive)
      {
         Write();
      }

      public string EntryLine { get; set; }
      public DateTime ModTime { get; set; }
      public string Revision { get; set; }
      public string Properties { get; set; }
      public string StickyOption { get; set; }
      public long Length { get; set; }
      public byte[] FileContents { get; set; }
      public FileType Type { get; set; }
   }
}