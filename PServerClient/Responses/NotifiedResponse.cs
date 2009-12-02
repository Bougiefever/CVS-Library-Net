﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class NotifiedResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Notified; } }
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }
   }
}