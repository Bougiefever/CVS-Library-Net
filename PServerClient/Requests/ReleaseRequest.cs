using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ReleaseRequest : RequestBase 
   {
      public override bool ResponseExpected { get { return true; } }

      public override string GetRequestString()
      {
         return string.Format("release{0}", lineEnd);
      }
   }
}
