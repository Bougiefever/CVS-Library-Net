using System;
using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   /// <summary>
   /// Abstract base class to interact with both Cvs folders and Cvs entry files on the Windows file system
   /// </summary>
   public abstract class CvsItemBase : ICvsItem
   {
      /// <summary>
      /// This is the file or folder
      /// </summary>
      public FileSystemInfo Item { get; set; }
      public IList<ICvsItem> ChildItems { get; set; }
      public DateTime ModTime { get; set; }
      public string Version { get; set; }
      public string Properties { get; set; }
      public long Length { get; set; }
      public byte[] FileContents { get; set; }

      public CvsItemType ItemType 
      {
         get
         {
            if (Item is FileInfo)
               return CvsItemType.Entry;
            return CvsItemType.Folder;
         }
      }

      public abstract IEnumerator<ICvsItem> CreateIterator();
      
      public virtual void Write()
      {
         throw new NotSupportedException();
      }
      public virtual void Read()
      {
         throw new NotSupportedException();
      }

      public virtual void AddItem(ICvsItem item)
      {
         throw new NotSupportedException();
      }

      public virtual void RemoveItem(ICvsItem item)
      {
         throw new NotSupportedException();
      }
   }
}
