using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class UseUnchangedRequest : RequestBase
   {
      public string Filename { get; set; }
      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         string request = "UseUnchanged " + Filename + " \n";
         return request;
      }
   }
}
