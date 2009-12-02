using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class StickyRequest : OneArgRequestBase
   {
      public StickyRequest(string arg) : base(arg) { }
      public override string RequestName { get { return "Sticky"; } }      
   }
}
