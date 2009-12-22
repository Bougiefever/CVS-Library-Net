using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class MessageResponseBase : ResponseBase
   {
      public string Message { get; internal set; }

      public override void ProcessResponse(IList<string> lines)
      {
         Message = lines[0];
         base.ProcessResponse(lines);
      }

      public override string DisplayResponse()
      {
         return Message;
      }
   }
}