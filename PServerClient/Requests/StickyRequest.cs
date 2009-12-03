using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class StickyRequest : OneArgRequestBase
   {
      public StickyRequest(string tagspec) : base(tagspec) { }
      public override string RequestName { get { return "Sticky"; } }      
   }
}
