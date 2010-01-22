using System;
using System.IO;
using System.Text.RegularExpressions;

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

      /// <summary>
      /// Initializes a new instance of the Entry class.
      /// </summary>
      /// <param name="name">The file name.</param>
      /// <param name="parent">The parent folder.</param>
      /// <param name="modTime">The mod time.</param>
      /// <param name="revision">The current revision.</param>
      /// <param name="properties">The entry file properties, if any.</param>
      /// <param name="stickyOption">The sticky option, if any.</param>
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

      /// <summary>
      /// Gets the CVS folder.
      /// </summary>
      /// <value>The CVS folder.</value>
      public override CVSFolder CVSFolder
      {
         get
         {
            return Parent.CVSFolder;
         }
      }

      /// <summary>
      /// Gets or sets the mod time.
      /// </summary>
      /// <value>The mod time.</value>
      public DateTime ModTime { get; set; }

      /// <summary>
      /// Gets or sets the revision.
      /// </summary>
      /// <value>The revision.</value>
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

      /// <summary>
      /// Gets or sets the properties.
      /// </summary>
      /// <value>The properties.</value>
      public string Properties { get; set; }

      /// <summary>
      /// Gets or sets the sticky option.
      /// </summary>
      /// <value>The sticky option.</value>
      public string StickyOption { get; set; }

      /// <summary>
      /// Gets or sets the length.
      /// </summary>
      /// <value>The length.</value>
      public long Length { get; set; }

      /// <summary>
      /// Gets or sets the file contents.
      /// </summary>
      /// <value>The file contents.</value>
      public byte[] FileContents { get; set; }

      /// <summary>
      /// Gets or sets the type.
      /// </summary>
      /// <value>Binary or text. Text is default value</value>
      public FileType Type { get; set; }

      /// <summary>
      /// Reads contents from  the local file system
      /// </summary>
      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo) Info);
      }

      /// <summary>
      /// Writes the contents to the local file system
      /// </summary>
      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo) Info, FileContents);
         FileContents = new byte[Length]; // clear the array to save memory
      }

      /// <summary>
      /// Writes the contents, and if recursive is true, will also save all the parent folders up to the root.
      /// </summary>
      /// <param name="recursive">if set to <c>true</c> [recursive].</param>
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
   }
}