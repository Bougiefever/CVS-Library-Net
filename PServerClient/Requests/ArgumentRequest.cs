using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ArgumentRequest : RequestBase
   {
      private string _arg;
      public ArgumentRequest(string arg)
      {
         _arg = arg;
      }
      public override bool ResponseExpected { get { return false; } }
      
      public override string GetRequestString()
      {
         string request = "Argument " + _arg + " \n";
         return request;
      }
   }
}
