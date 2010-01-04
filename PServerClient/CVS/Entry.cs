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
      }

      public override CVSFolder CVSFolder
      {
         get { return Parent.CVSFolder; }
      }

      public override string Module
      {
         get { return Parent.Module; }
      }

      public override string Repository
      {
         get { return Parent.Repository; }
      }

      public string EntryLine { get; set; }
   }
}