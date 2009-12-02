using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class Repository : RequestBase
   {
      private CvsRoot _root;
      public Repository(CvsRoot root)
      {
         _root = root;
      }
      public override bool ResponseExpected { get { return false; } }
      public override string GetRequestString()
      {
         return string.Format("Repository {0}{1}", _root.CvsRootPath, lineEnd);
      }
   }
}
