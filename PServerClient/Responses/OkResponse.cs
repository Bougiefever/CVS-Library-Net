using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class OkResponse : ResponseBase
   {
      public override void ProcessResponse()
      {
         Success = true;
         ResponseString = "";
      }
   }
}
