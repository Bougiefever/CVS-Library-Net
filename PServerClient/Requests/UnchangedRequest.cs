using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class UnchangedRequest : OneArgRequestBase
   {
      public UnchangedRequest(string fileName) : base(fileName) { }
      public override string RequestName { get { return "Unchanged"; } }
   }
}
