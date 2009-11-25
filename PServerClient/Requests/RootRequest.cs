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
         string request = "Root " + _root.RepositoryPath + " \n";
         return request;
      }
   }
}
