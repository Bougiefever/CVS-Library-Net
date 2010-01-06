using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class MessageResponseBase : ResponseBase, IMessageResponse
   {
      public string Message { get; internal set; }

      public override void Process()
      {
         Message = Lines[0];
         base.Process();
      }
   }
}