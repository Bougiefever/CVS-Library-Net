using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class MessageResponseBase : ResponseBase, IMessageResponse
   {
      public string Message { get; internal set; }

      public override void Process(IList<string> lines)
      {
         Message = lines[0];
         base.Process(lines);
      }
   }
}