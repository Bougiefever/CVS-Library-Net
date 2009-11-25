using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ValidRequestResponse : IResponse
   {
      public string ResponseString { get; set; }
      public bool Success { get; set; }

      public string ErrorMessage { get; set; }
      public IList<string> ValidRequests { get; private set; }
      public void ProcessResponse()
      {
         string[] requests = ResponseString.Split((char) 32);
         ValidRequests = requests.ToList();
         Success = true;
      }
   }
}
