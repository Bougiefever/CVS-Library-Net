using System;

namespace PServerClient.Responses
{
   public class EMessageResponse : MessageResponseBase
   {
      public override ResponseType Type { get { return PServerClient.ResponseType.EMessage;}}
   }
}