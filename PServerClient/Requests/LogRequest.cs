using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class LogRequest : RequestBase
   {
      public override bool ResponseExpected { get { return true; } }

      public override string GetRequestString()
      {
         string request = "log \n";
         return request;
      }
   }
}
