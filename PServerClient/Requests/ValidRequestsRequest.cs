using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public class ValidRequestsRequest : RequestBase
   {
      public ValidRequestsRequest()
      {
         Response = new ValidRequestResponse();
      }

      public override bool ResponseExpected
      {
         get { return true; }
      }

      public override string GetRequestString()
      {
         return "valid-requests \n";
      }

   }
}
