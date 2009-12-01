using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ModuleExpansionResponse : ResponseBase
   {
      public string ModuleName { get; set; }
      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
      }
   }
}
