using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ArgumentRequest : OneArgRequestBase
   {
      public ArgumentRequest(string arg) : base(arg) {}
      public override string RequestName { get { return "Argument"; } }      
   }
}
