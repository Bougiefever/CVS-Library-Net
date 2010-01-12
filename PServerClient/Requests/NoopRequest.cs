using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// noop \n
   /// Response expected: yes. This request is a null command in the sense that
   /// it doesn’t do anything, but merely (as with any other requests expecting a
   /// response) sends back any responses pertaining to pending errors, pending
   /// Notified responses, etc. The Root request need not have been previously
   /// sent.
   /// </summary>
   public class NoopRequest : NoArgRequestBase
   {
      public NoopRequest()
      {
      }

      public NoopRequest(IList<string> lines)
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
            return RequestType.Noop;
         }
      }
   }
}