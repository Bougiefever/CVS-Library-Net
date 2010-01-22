using System;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// Abstract base class for both repository folders and repository files
   /// </summary>
   public abstract class CVSItemBase : ICVSItem
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="CVSItemBase"/> class.
      /// </summary>
      /// <param name="parent">The parent.</param>
      protected CVSItemBase(Folder parent)
      {
         Parent = parent;
         Parent.AddItem(this);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CVSItemBase"/> class.
      /// </summary>
      /// <param name="info">The FileInfo or DirectoryInfo.</param>
      protected CVSItemBase(FileSystemInfo info)
      {
         Info = info;
         Parent = null;
      }

      /// <summary>
      /// Gets the CVS hidden folder for repository information
      /// </summary>
      /// <value></value>
      public abstract CVSFolder CVSFolder { get; }

      /// <summary>
      /// Gets or sets either FileInfo or DirectoryInfo object depending on
      /// whether instance is an Entry or Folder object
      /// </summary>
      /// <value></value>
      public FileSystemInfo Info { get; protected set; }

      /// <summary>
      /// Gets or sets the parent folder item
      /// </summary>
      /// <value></value>
      public Folder Parent { get; protected set; }

      /// <summary>
      /// Gets or sets the line for the Entries CVS folder file
      /// </summary>
      /// <value></value>
      public string EntryLine { get; set; }

      /// <summary>
      /// Save to disk
      /// </summary>
      public void Save()
      {
         Save(false);
      }

      /// <summary>
      /// Save to disk
      /// </summary>
      /// <param name="recursive">Determines whether or not to save all children</param>
      public abstract void Save(bool recursive);

      /// <summary>
      /// read contents of the item
      /// </summary>
      public virtual void Read()
      {
         throw new NotSupportedException();
      }

      /// <summary>
      /// Write the FileContents to the Entry file if item is Entry type
      /// Create a directory if item is Folder type
      /// </summary>
      public abstract void Write();

      /// <summary>
      /// Writes or updates the CVS entry line to the CVS Entries file.
      /// </summary>
      public void WriteCVSEntryLine()
      {
         CVSFolder.WriteEntry(this);
      }
   }
}