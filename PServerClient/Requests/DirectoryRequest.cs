using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class DirectoryRequest : RequestBase
   {
      private CvsRoot _root;

      public DirectoryRequest(CvsRoot root)
      {
         _root = root;
      }
      public override bool ResponseExpected { get { return true; } }
      public override string GetRequestString()
      {
         string request = "Directory " + _root.LocalPath + " \n";
         return request;
      }
   }
}
