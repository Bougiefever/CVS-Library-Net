using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public class Entry : CvsItemBase
   {

      public override IEnumerator<ICvsItem> CreateIterator()
      {
         return null;
      }

      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo)Item);
         //FileInfo file = (FileInfo) Item;
         //using (FileStream fileStream = file.OpenRead())
         //{
         //   fileStream.Read(FileContents, 0, (int)file.Length);
         //   fileStream.Close();
         //}
      }

      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo)Item, FileContents);
         //FileInfo file = (FileInfo)Item;
         //using (FileStream fileStream = file.Open(FileMode.OpenOrCreate))
         //{
         //   fileStream.Write(FileContents, 0, FileContents.Length);
         //   fileStream.Close();
         //}
      }
   }
}