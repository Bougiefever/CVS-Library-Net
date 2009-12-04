using System.Collections.Generic;

namespace PServerClient.LocalFileSystem
{
   public class WorkingDirectory : LocalCvsItem
   {
      public override IEnumerator<LocalCvsItem> CreateIterator()
      {
         return ChildItems.GetEnumerator();
      }
   }
}