using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class NotifyRequest : RequestBase
   {
      private string _fileName;
      public NotifyRequest(string fileName)
      {
         _fileName = fileName;
      }
      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         return string.Format("Notify {0}{1}", _fileName, lineEnd);
      }   
   }
}
