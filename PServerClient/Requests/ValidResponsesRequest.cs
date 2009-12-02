using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ValidResponsesRequest : RequestBase
   {
      public ValidResponsesRequest(string responses)
      {
         ValidResponses = responses.Split((char)32).ToList<string>();
      }
      public ValidResponsesRequest(IList<string> responses)
      {
         ValidResponses = responses;
      }

      public IList<string> ValidResponses { get; private set; }
      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         string responses = String.Join(" ", ValidResponses.ToArray());
         return string.Format("Valid-responses {0}{1}", responses, lineEnd);
      }
   }
}
