using System;
using System.Collections.Generic;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// Abstract base class to interact with both Cvs folders and Cvs entry files on the Windows file system
   /// </summary>
   public abstract class CVSItemBase : ICVSItem
   {
      protected CVSItemBase(FileSystemInfo info)
      {
         Item = info;
         Revision = string.Empty;
         Properties = string.Empty;
         StickyOption = string.Empty;
      }


      public abstract IList<ICVSItem> ChildItems { get; }
      public abstract CVSFolder CvsFolder { get; }
      /// <summary>
      /// This is the file or folder
      /// </summary>
      public FileSystemInfo Item { get; private set; }      
      public string Name { get { return Item.Name; } }
      public DateTime ModTime { get; set; }
      public string Revision { get; set; }
      public string Properties { get; set; }
      public string StickyOption { get; set; }
      public long Length { get; set; }
      public byte[] FileContents { get; set; }

      public ItemType ItemType 
      {
         get
         {
            if (Item is FileInfo)
               return ItemType.Entry;
            return ItemType.Folder;
         }
      }

      
      public virtual void Write()
      {
         throw new NotSupportedException();
      }
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

   }
}
