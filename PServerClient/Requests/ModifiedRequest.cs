using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ModifiedRequest : OneArgRequestBase
   {
      public ModifiedRequest(string fileName) : base(fileName) { }
      public override string RequestName { get { return "Modified"; } }
   }
}
