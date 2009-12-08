using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public class Entry : CvsItemBase
   {
      public Entry(FileSystemInfo info) : base(info)
      {
      }

      public override IEnumerator<ICvsItem> CreateIterator()
      {
         return null;
      }

      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo)Item);
      }

      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo)Item, FileContents);
      }
   }
}