using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class RootRequest : RequestBase
   {
      private CvsRoot _root;

      public RootRequest(CvsRoot root)
      {
         _root = root;
      }
      public override bool ResponseExpected { get { return false; } }
      public override string GetRequestString()
      {
         return string.Format("Root {0}{1}", _root.Root, lineEnd);
      }
   }
}
