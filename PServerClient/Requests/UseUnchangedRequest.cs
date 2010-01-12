using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// UseUnchanged \n
   /// Response expected: no. To specify the version of the protocol described in this
   /// document, servers must support this request (although it need not do anything)
   /// and clients must issue it. The Root request need not have been previously sent.
   /// </summary>
   public class UseUnchangedRequest : NoArgRequestBase
   {
      public UseUnchangedRequest()
      {
      }

      public UseUnchangedRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.UseUnchanged;
         }
      }
   }
}