using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Sticky tagspec \n
   /// Response expected: no. Tell the server that the directory most recently specified
   /// with Directory has a sticky tag or date tagspec. The first character of tagspec
   /// is ‘T’ for a tag, ‘D’ for a date, or some other character supplied by a Set-
   /// sticky response from a previous request to the server. The remainder of tagspec
   /// contains the actual tag or date, again as supplied by Set-sticky.
   /// The server should remember Static-directory and Sticky requests for a par-
   /// ticular directory; the client need not resend them each time it sends a Directory
   /// request for a given directory. However, the server is not obliged to remember
   /// them beyond the context of a single command.
   /// </summary>
   public class StickyRequest : OneArgRequestBase
   {
      public StickyRequest(string tagspec)
         : base(tagspec)
      {
      }

      public StickyRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.Sticky;
         }
      }
   }
}