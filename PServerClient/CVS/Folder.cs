using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// Represents a folder in the cvs repository
   /// </summary>
   public class Folder : CVSItemBase, IEnumerable
   {
      private readonly IEnumerable<ICVSItem> _childItems;
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
      public int Count { get { return _childItems.Count(); } }
      public ICVSItem this[int idx] { get { return _childItems.ElementAt(idx); } }
      public string Repository
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

      public string Module
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

      public void AddItem(ICVSItem item)
      {
         _childItems.ToList().Add(item);
      }

      public void RemoveItem(ICVSItem item)
      {
         _childItems.ToList().Remove(item);
      }

      public IEnumerator GetEnumerator()
      {
         return _childItems.GetEnumerator();
      }

      public override void Write()
      {
         ReaderWriter.Current.CreateDirectory((DirectoryInfo) Info);
      }

      public override void Save(bool recursive)
      {
         Write();
         if (recursive)
         {
            foreach (ICVSItem item in this)
            {
               item.Save(true);
            }
         }
      }

      public IList<Folder> GetSubFolders()
      {
         return _childItems.OfType<Folder>().ToList();
      }

      public IList<Entry> GetEntries()
      {
         return _childItems.OfType<Entry>().ToList();
      }
   }
}