using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public abstract class NoArgRequestBase : RequestBase
   {
      public override bool ResponseExpected { get { return false; } }
      public abstract string RequestName { get; }
      public override string GetRequestString()
      {
         return string.Format("{0}{1}", RequestName, lineEnd);
      }
   }
}
