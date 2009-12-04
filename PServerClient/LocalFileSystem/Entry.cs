using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public class Entry : LocalCvsItem
   {

      public override IEnumerator<LocalCvsItem> CreateIterator()
      {
         return null;
      }

      public override void Read()
      {
         FileInfo file = (FileInfo) Item;
         using (FileStream fileStream = file.OpenRead())
         {
            fileStream.Read(FileContents, 0, (int)file.Length);
            fileStream.Close();
         }
      }

      public override void Write()
      {
         FileInfo file = (FileInfo)Item;
         using (FileStream fileStream = file.Open(FileMode.OpenOrCreate))
         {
            fileStream.Write(FileContents, 0, FileContents.Length);
            fileStream.Close();
         }
      }
   }
}