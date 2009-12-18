using System;
using System.Collections.Generic;
using System.IO;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// This class is to interact with the local file system. It represents one
   /// local windows folder in the project. It contains project files and other system folders.
   /// </summary>
   public class Folder : CVSItemBase
   {
      private IList<ICVSItem> _childItems;
      private CVSFolder _cvsFolder;

      public Folder(FileSystemInfo info, string cvsRoot, string cvsModule) : base(info)
      {
         _cvsFolder = new CVSFolder(this, cvsRoot, cvsModule);
      }

      public override void AddItem(ICVSItem item)
      {
         ChildItems.Add(item);
      }
      public override void RemoveItem(ICVSItem item)
      {
         ChildItems.Remove(item);
      }

      public override CVSFolder CvsFolder 
      { 
         get { return _cvsFolder; }
      }

      public override IList<ICVSItem> ChildItems 
      { 
         get
         {
            if (_childItems == null)
               _childItems = new List<ICVSItem>();
            return _childItems;
         } 
      }

      public override void Write()
      {
         ReaderWriter.Current.CreateDirectory((DirectoryInfo)Item);
      }
   }
}