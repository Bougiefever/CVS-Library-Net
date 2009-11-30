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

      public override bool ResponseExpected { get { return false; } }
      public override string GetRequestString()
      {
         //string checkoutDir = _root.CvsRootPath + "/" + _root.Module;
         string request = "Directory ." + lineEnd + _root.CvsRootPath + lineEnd;
         return request;
      }
   }
}
