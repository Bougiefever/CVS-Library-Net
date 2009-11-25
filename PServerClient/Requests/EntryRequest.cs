using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class EntryRequest : RequestBase
   {
      public override bool ResponseExpected { get { return false; } }
      public string EntryLine { get; set; }

      public override string GetRequestString()
      {
         string request = "Entry " + EntryLine + " \n";
         return request;
      }
   }
}
