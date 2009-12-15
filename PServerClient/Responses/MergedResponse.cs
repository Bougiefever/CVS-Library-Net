using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Merged pathname \n
   //This is just like Updated and takes the same additional data, with the one
   //difference that after the new copy of the file is enclosed, it will still not be up
   //to date. Used for the results of a merge, with or without conflicts.
   //It is useful to preserve an copy of what the file looked like before the merge.
   //This is basically handled by the server; before sending Merged it will send a
   //Copy-file response. For example, if the file is ‘aa’ and it derives from revision
   //1.3, the Copy-file response will tell the client to copy ‘aa’ to ‘.#aa.1.3’. It is
   //up to the client to decide how long to keep this file around; traditionally clients
   //have left it around forever, thus letting the user clean it up as desired. But
   //another answer, such as until the next commit, might be preferable.
   /// </summary>
   public class MergedResponse : ResponseBase, IFileResponse
   {
      public long FileLength { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.Merged; } }
      public ReceiveFile File { get; set; }
      public override int LineCount { get { return 5; } }

   }
}