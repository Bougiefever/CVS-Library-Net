using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      public abstract void ProcessResponse(IList<string> lines);
      public virtual int LineCount { get { return 1; } }
   }
}
