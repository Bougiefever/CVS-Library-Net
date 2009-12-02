using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ArgumentxRequest : OneArgRequestBase
   {
      public ArgumentxRequest(string arg) : base(arg) {}
      public override string RequestName { get { return "Argumentx"; } }      
   }
}
