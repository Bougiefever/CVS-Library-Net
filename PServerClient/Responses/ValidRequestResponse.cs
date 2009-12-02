using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ValidRequestResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.ValidRequests; } }
      public IList<string> ValidRequests { get; private set; }
      public override void ProcessResponse(IList<string> lines)
      {
         string[] requests = lines[0].Split((char) 32);
         ValidRequests = requests.ToList();
      }
   }
}
