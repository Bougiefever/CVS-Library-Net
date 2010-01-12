using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// watchers \n
   /// Response expected: yes. Actually do a cvs command. This uses any previous
   /// Argument, Directory, Entry, or Modified requests, if they have been sent.
   /// The last Directory sent specifies the working directory at the time of the
   /// operation. No provision is made for any input from the user. This means that
   /// ci must use a -m argument if it wants to specify a log message.
   /// </summary>
   public class WatchersRequest : NoArgRequestBase
   {
      public WatchersRequest()
      {
      }

      public WatchersRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.Watchers;
         }
      }
   }
}