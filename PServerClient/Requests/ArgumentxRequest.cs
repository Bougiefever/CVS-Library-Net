using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ArgumentxRequest : RequestBase
   {
      public override bool ResponseExpected { get { return false; } }
      public string ArgumentText { get; set; }

      public override string GetRequestString()
      {
         string request = "Argumentx " + ArgumentText + lineEnd;
         return request;
      }
   }
}
