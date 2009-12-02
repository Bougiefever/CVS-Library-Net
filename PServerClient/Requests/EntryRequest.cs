using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class EntryRequest : RequestBase
   {
      private string _entryLine;
      public EntryRequest(string name, string version, string conflict, string options, string tagOrDate)
      {
         _entryLine = string.Format("/{0}/{1}/{2}/{3}/{4}", name, version, conflict, options, tagOrDate);
      }
      public override bool ResponseExpected { get { return false; } }
      public override string GetRequestString()
      {
         return string.Format("Entry {0}{1}", _entryLine, lineEnd);
      }
   }
}
