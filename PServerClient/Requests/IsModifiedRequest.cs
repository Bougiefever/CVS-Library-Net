using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class IsModifiedRequest : OneArgRequestBase
   {
      public IsModifiedRequest(string fileName) : base(fileName) { }
      public override string RequestName { get { return "Is-modified"; } }
   }
}
