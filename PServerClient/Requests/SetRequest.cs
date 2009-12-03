using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class SetRequest : RequestBase
   {
      private string _variable;
      private string _value;

      public SetRequest(string variableName, string value)
      {
         _variable = variableName;
         _value = value;
      }

      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         return string.Format("Set {0}={1}{2}", _variable, _value, lineEnd);
      }
   }
}
