using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// Represents a folder in the cvs repository
   /// </summary>
   public class Folder : CVSItemBase
   {
      private readonly IList<ICVSItem> _childItems;
      private readonly CVSFolder _cvsFolder;
      private string _cvsModule;
      private string _cvsConnectionString;

      public Folder(FileSystemInfo info, Folder parent) : base(info, parent)
      {
         _childItems = new List<ICVSItem>();
         _cvsFolder = new CVSFolder(this);
         parent.AddItem(this);
      }

      public Folder(FileSystemInfo info, string cvsConnectionString, string cvsModule) : base(info)
      {
         _cvsFolder = new CVSFolder(this);
         _childItems = new List<ICVSItem>();
         CVSModule = cvsModule;
         CVSConnectionString = cvsConnectionString;
      }

      public override CVSFolder CvsFolder { get { return _cvsFolder; } }
      public override int Count { get { return _childItems.Count; } }
      public override ICVSItem this[int idx] { get { return _childItems[idx]; } }
      public override string Repository
      {
         get
         {
            if (Parent == null)
               return CVSModule + "/";
            return Parent.Repository + Info.Name + "/";
         }
      }

      public string CVSModule
      {
         get
         {
            if (Parent == null)
               return _cvsModule;
            return Parent.CVSModule;
         } 
         private set
         {
            _cvsModule = value;
         }
      }

      public string CVSConnectionString
      {
         get
         {
            if (Parent == null)
               return _cvsConnectionString;
            return Parent.CVSConnectionString;
         } 
         private set
         {
            _cvsConnectionString = value;
         }
      }

      public void AddItem(ICVSItem item)
      {
         _childItems.Add(item);
      }

      public override void RemoveItem(ICVSItem item)
      {
         _childItems.Remove(item);
      }

      public override IEnumerator GetEnumerator()
      {
         return _childItems.GetEnumerator();
      }

      public override void Write()
      {
         ReaderWriter.Current.CreateDirectory((DirectoryInfo) Info);
      }
   }
}