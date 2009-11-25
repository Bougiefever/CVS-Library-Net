using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class NullResponse : IResponse
   {
      private string _responseString = string.Empty;
      public string ResponseString
      {
         get { return _responseString; }
         set { _responseString = value; }
      }

      public bool Success { get; set; }

      public string ErrorMessage { get; set; }

      public void ProcessResponse()
      {
         Success = true;
         // do nothing
      }
   }
}
