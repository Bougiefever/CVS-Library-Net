using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class InitRequest : OneArgRequestBase
   {
      public InitRequest(string rootName) : base(rootName) { }
      public override bool ResponseExpected { get { return true; } }
      public override string RequestName { get { return "init"; } }
   }
}
