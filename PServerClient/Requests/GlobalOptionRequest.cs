using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class GlobalOptionRequest : OneArgRequestBase
   {
      public GlobalOptionRequest(string arg) : base(arg) { }
      public override string RequestName { get { return "Global_option"; } }
   }
}
