using System;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// Abstract base class for both repository folders and repository files
   /// </summary>
   public abstract class CVSItemBase : ICVSItem
   {
      protected CVSItemBase(Folder parent)
      {
         Parent = parent;
         Parent.AddItem(this);
      }

      protected CVSItemBase(FileSystemInfo info)
      {
         Info = info;
         Parent = null;
      }

      public abstract CVSFolder CVSFolder { get; }

      public FileSystemInfo Info { get; protected set; }

      public Folder Parent { get; protected set; }

      public string EntryLine { get; set; }

      public void Save()
      {
         Save(false);
      }

      public abstract void Save(bool recursive);

      public virtual void Read()
      {
         throw new NotSupportedException();
      }

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