using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class TagRequest : RequestBase
   {
      public override bool ResponseExpected { get { return true; } }

      public override string GetRequestString()
      {
         return "tag \n";
      }
   }
}
