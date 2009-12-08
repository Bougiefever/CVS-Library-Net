using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   /// <summary>
   /// This class is to interact with the local file system. It represents one
   /// local windows folder in the project. It contains project files and other system folders.
   /// </summary>
   public class Folder : CvsItemBase
   {
      public Folder(FileSystemInfo info) : base(info)
      {
      }

      public override IEnumerator<ICvsItem> CreateIterator()
      {
         return ChildItems.GetEnumerator();
      }
      public override void AddItem(ICvsItem item)
      {
         ChildItems.Add(item);
      }
      public override void RemoveItem(ICvsItem item)
      {
         ChildItems.Remove(item);
      }
   }
}