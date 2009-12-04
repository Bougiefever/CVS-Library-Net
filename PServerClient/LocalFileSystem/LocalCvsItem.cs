using System;
using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public abstract class LocalCvsItem
   {
      /// <summary>
      /// This is the file or folder
      /// </summary>
      public FileSystemInfo Item { get; set; }
      public IList<LocalCvsItem> ChildItems { get; set; }
      public DateTime ModTime { get; set; }
      public string Version { get; set; }
      public string Properties { get; set; }
      public long Length { get; set; }
      public byte[] FileContents { get; set; }

      public LocalItemType ItemType 
      {
         get
         {
            if (Item is FileInfo)
               return LocalItemType.Entry;
            return LocalItemType.Folder;
         }
      }

      public abstract IEnumerator<LocalCvsItem> CreateIterator();
      
      public virtual void Write()
      {
         throw new NotSupportedException();
      }
      public virtual void Read()
      {
         throw new NotSupportedException();
      }

      public virtual void AddItem(LocalCvsItem item)
      {
         throw new NotSupportedException();
      }
   }
}
