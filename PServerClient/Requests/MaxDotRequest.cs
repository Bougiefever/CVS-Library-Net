using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class MaxDotRequest : OneArgRequestBase
   {
      public MaxDotRequest(string arg) : base(arg) { }
      public override string RequestName { get { return "Max-dotdot"; } }      
   }
}
