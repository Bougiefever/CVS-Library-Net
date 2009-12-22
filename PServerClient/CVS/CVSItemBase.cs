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
      protected CVSItemBase(FileSystemInfo info)
      {
         Info = info;
         Revision = string.Empty;
         Properties = string.Empty;
         StickyOption = string.Empty;
      }

      #region ICVSItem Members

      public abstract void Write();
      public virtual CVSFolder CvsFolder { get { throw new NotSupportedException(); } }

      public virtual void Read()
      {
         throw new NotSupportedException();
      }

      public virtual void AddItem(ICVSItem item)
      {
         throw new NotSupportedException();
      }

      public virtual void RemoveItem(ICVSItem item)
      {
         throw new NotSupportedException();
      }

      public virtual ICVSItem this[int idx] { get { throw new NotSupportedException(); } }

      public virtual IEnumerator GetEnumerator()
      {
         throw new NotSupportedException();
      }

      public virtual int Count { get { throw new NotSupportedException(); } }

      /// <summary>
      /// This is the file or folder
      /// </summary>
      public FileSystemInfo Info { get; private set; }

      public string Name { get { return Info.Name; } }
      public DateTime ModTime { get; set; }
      public string Revision { get; set; }
      public string Properties { get; set; }
      public string StickyOption { get; set; }
      public long Length { get; set; }
      public byte[] FileContents { get; set; }

      #endregion
   }
}