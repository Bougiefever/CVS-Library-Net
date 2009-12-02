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
         return string.Format("Directory .{0}{1}/{2}{3}", lineEnd, _root.CvsRootPath, _root.Module, lineEnd);
      }
   }
}
