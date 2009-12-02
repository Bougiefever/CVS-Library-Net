using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ExpandModulesRequest : RequestBase
   {
      public override bool ResponseExpected
      {
         get { return true; }
      }

      public override string GetRequestString()
      {
         return string.Format("expand-modules{0}", lineEnd);
      }
   }
}
