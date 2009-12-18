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

      public Folder(FileSystemInfo info, string cvsRoot, string cvsModule) : base(info)
      {
         _cvsFolder = new CVSFolder((DirectoryInfo) Info, cvsRoot, cvsModule);
         _childItems = new List<ICVSItem>();
      }

      public override CVSFolder CvsFolder {  get { return _cvsFolder; } }
      public override int Count { get { return _childItems.Count; } }
      public override ICVSItem this[int idx] { get { return _childItems[idx]; } }

      public override void AddItem(ICVSItem item)
      {
         _childItems.Add(item);
      }
      public override void RemoveItem(ICVSItem item)
      {
         _childItems.Remove(item);
      }

      public override IEnumerator GetEnumerator() { return _childItems.GetEnumerator(); }

      public override void Write()
      {
         ReaderWriter.Current.CreateDirectory((DirectoryInfo)Info);
      }
   }
}