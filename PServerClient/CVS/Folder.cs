using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PServerClient.CVS
{
   /// <summary>
   /// Represents a folder in the cvs repository
   /// </summary>
   public class Folder : CVSItemBase, IEnumerable
   {
      private readonly IList<ICVSItem> _childItems;
      private readonly CVSFolder _cvsFolder;
      private string _module;
      private string _repository;
      private string _connection;

      /// <summary>
      /// Initializes a new instance of the Folder class
      /// The sub folders in the tree use this constructor, which contains the parent parameter.
      /// The Parent Info property is used with the module path name to name the new folder
      /// </summary>
      /// <param name="name">Name of the folder. This also is added to the module name of the parent</param>
      /// <param name="parent">Parent folder</param>
      public Folder(string name, Folder parent)
         : base(parent)
      {
         DirectoryInfo di = new DirectoryInfo(Path.Combine(parent.Info.FullName, name));
         Info = di;
         _childItems = new List<ICVSItem>();
         _cvsFolder = new CVSFolder(this);
      }

      /// <summary>
      /// Initializes a new instance of the Folder class
      /// The root folder in the tree uses this constructor
      /// The Parent property of the root folder is null. All other objects in the tree have a Parent folder
      /// </summary>
      /// <param name="info">DirectoryInfo of local folder that is the root folder for the CVS module</param>
      /// <param name="connection">CVS connection string - used to write the CVS Root file</param>
      /// <param name="repository">CVS repository</param>
      /// <param name="module">CVS module for current folder</param>
      public Folder(FileSystemInfo info, string connection, string repository, string module)
         : base(info)
      {
         _cvsFolder = new CVSFolder(this);
         _childItems = new List<ICVSItem>();
         _module = module;
         _repository = repository;
         _connection = connection;
      }

      /// <summary>
      /// Reference to the CVS folder implementation
      /// </summary>
      public override CVSFolder CVSFolder
      {
         get
         {
            return _cvsFolder;
         }
      }

      /// <summary>
      /// Gets the count of entries and folders contained in this folder
      /// </summary>
      public int Count
      {
         get
         {
            return _childItems.Count();
         }
      }

      /// <summary>
      /// Gets the repository string that gets written in the CVS folder Repository file
      /// </summary>
      public string Repository
      {
         get
         {
            if (Parent == null)
               return _repository;
            return Parent.Repository;
         }
      }

      /// <summary>
      /// Gets the connection string that gets written in the CVS folder Root file
      /// </summary>
      public string Connection
      {
         get
         {
            if (Parent == null)
               return _connection;
            return Parent.Connection;
         }
      }

      /// <summary>
      /// Gets the current CVS module for the location in the file structure
      /// </summary>
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

      /// <summary>
      /// Indexer for the child entry and folder items
      /// </summary>
      /// <param name="idx">index for item</param>
      /// <returns>Entry or Folder item</returns>
      public ICVSItem this[int idx]
      {
         get
         {
            return _childItems.ElementAt(idx);
         }
      }

      /// <summary>
      /// Add a child item to this folder
      /// </summary>
      /// <param name="item">Entry or Folder item</param>
      public void AddItem(ICVSItem item)
      {
         _childItems.Add(item);
      }

      /// <summary>
      /// Remove a child item from this folder
      /// </summary>
      /// <param name="item">Entry or Folder item</param>
      public void RemoveItem(ICVSItem item)
      {
         _childItems.Remove(item);
      }

      /// <summary>
      /// Implementation of IEnumerator for indexing
      /// </summary>
      /// <returns>Returns an enumerator for the child items</returns>
      public IEnumerator GetEnumerator()
      {
         return _childItems.GetEnumerator();
      }

      /// <summary>
      /// Create the folder on the local file system
      /// </summary>
      public override void Write()
      {
         ReaderWriter.Current.CreateDirectory((DirectoryInfo) Info);
      }

      /// <summary>
      /// Save the folder and optionally its child items
      /// </summary>
      /// <param name="recursive">Determines whether or not to </param>
      public override void Save(bool recursive)
      {
         Write();
         if (recursive)
         {
            foreach (ICVSItem item in this)
            {
               if (item is Entry)
                  item.Save();
               else
                  item.Save(true);
            }
         }
      }

      /// <summary>
      /// Gets the list of the Folder-type items contained in this folder
      /// </summary>
      /// <returns>List of folder items</returns>
      public IList<Folder> GetSubFolders()
      {
         return _childItems.OfType<Folder>().ToList();
      }

      /// <summary>
      /// Gets the list of Entry-type items contained in this folder
      /// </summary>
      /// <returns>List of Entry items</returns>
      public IList<Entry> GetEntries()
      {
         return _childItems.OfType<Entry>().ToList();
      }

      /// <summary>
      /// Gets the root folder for the structure
      /// </summary>
      /// <returns>Folder that is the root folder</returns>
      public Folder GetRootFolder()
      {
         if (Parent == null)
            return this;
         return Parent.GetRootFolder();
      }

      /// <summary>
      /// Saves the CVS information to the CVS folder
      /// </summary>
      public void SaveCVSFolder()
      {
         // save the cvs folder information
         ReaderWriter.Current.CreateDirectory(CVSFolder.CVSDirectory);
         CVSFolder.WriteRootFile();
         CVSFolder.WriteRepositoryFile();
         CVSFolder.WriteEntries();
      }
   }
}