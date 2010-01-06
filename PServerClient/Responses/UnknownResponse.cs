using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class UnknownResponse : ResponseBase
   {
      public override int LineCount { get { return 1; } }
      public override ResponseType Type { get { return ResponseType.Unknown; } }
   }
}