using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Remove-entry pathname \n
   //The file needs its entry removed from CVS/Entries, but the file itself is already
   //gone (this happens in response to a ci request which involves committing the
   //removal of a file).
   /// </summary>
   public class RemoveEntryResponse : ResponseBase
   {
      public string RepositoryPath { get; private set; }
      public override ResponseType Type { get { return ResponseType.RemoveEntry; } }

      public override void Process(IList<string> lines)
      {
         RepositoryPath = lines[0];
         base.Process(lines);
      }

      public override string Display()
      {
         return RepositoryPath;
      }
   }
}