using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ArgumentRequest : RequestBase
   {
      public override bool ResponseExpected { get { return false; } }
      public string ArgumentText { get; set; }
      public override string GetRequestString()
      {
         string request = "Argument " + ArgumentText + " \n";
         return request;
      }
   }
}
