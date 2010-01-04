using System;
using System.Collections;
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

      public abstract void Write();
      public abstract string Repository { get; }
      public abstract string Module { get; }
      public abstract CVSFolder CVSFolder { get; } 
      
      public virtual void Read() { throw new NotSupportedException(); }
      public virtual void AddItem(ICVSItem item) { throw new NotSupportedException(); }
      public virtual void RemoveItem(ICVSItem item) { throw new NotSupportedException(); }
      public virtual ICVSItem this[int idx] { get { throw new NotSupportedException(); } }
      public virtual IEnumerator GetEnumerator() { throw new NotSupportedException(); }
      public virtual int Count { get { throw new NotSupportedException(); } }

      public FileSystemInfo Info { get; protected set; }
      public Folder Parent { get; protected set; }
      public DateTime ModTime { get; set; }
      public string Revision { get; set; }
      public string Properties { get; set; }
      public string StickyOption { get; set; }
      public long Length { get; set; }
      public byte[] FileContents { get; set; }

   }
}