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
      private string _module;
      private string _repository;
      private string _connection;

      /// <summary>
      /// Constructor for folders under the root. The directory info uses the parent folder as its base
      /// </summary>
      /// <param name="name">Name of the folder. This also is added to the module name of the parent</param>
      /// <param name="parent">Parent folder</param>
      public Folder(string name, Folder parent) : base(parent)
      {
         DirectoryInfo di = new DirectoryInfo(Path.Combine(parent.Info.FullName, name));
         Info = di;
         _childItems = new List<ICVSItem>();
         _cvsFolder = new CVSFolder(this);
      }

      /// <summary>
      /// Constructor for root folder
      /// </summary>
      /// <param name="info">DirectoryInfo of local folder that is the root folder for the CVS module</param>
      /// <param name="connection">CVS connection string - used to write the CVS Root file</param>
      /// <param name="repository">CVS repository</param>
      /// <param name="module">CVS module for current folder</param>
      public Folder(FileSystemInfo info, string connection, string repository, string module) : base(info)
      {
         _cvsFolder = new CVSFolder(this);
         _childItems = new List<ICVSItem>();
         _module = module;
         _repository = repository;
         _connection = connection;
      }

      public override CVSFolder CVSFolder { get { return _cvsFolder; } }
      public override int Count { get { return _childItems.Count; } }
      public override ICVSItem this[int idx] { get { return _childItems[idx]; } }
      public override string Repository
      {
         get
         {
            if (Parent == null)
               return _repository;
            return Parent.Repository;
         }
      }

      public string Connection
      {
         get
         {
            if (Parent == null)
               return _connection;
            return Parent.Connection;
         } 
      }

      public override string Module
      {
         get 
         {
            string module;
            if (Parent == null)
               module = _module;
            else
               module = Parent.Module + "/" + Info.Name;
            return module;
         }
      }

      public override void AddItem(ICVSItem item)
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