using System;

namespace PServerClient.Responses
{
   public class EMessageResponse : MessageResponseBase
   {
      public override ResponseType ResponseType { get { return PServerClient.ResponseType.EMessage;}}
   }
}