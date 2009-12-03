using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class CaseRequest : NoArgRequestBase
   {
      public override string RequestName { get { return "Case"; } }
   }
}
