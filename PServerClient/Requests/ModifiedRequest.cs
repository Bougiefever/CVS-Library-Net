using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ModifiedRequest : RequestBase
   {
      public string Filename { get; set; }
      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         string request = "Modified " + Filename + " \n";
         return request;
      }
   }
}
