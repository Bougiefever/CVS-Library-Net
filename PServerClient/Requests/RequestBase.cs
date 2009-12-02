using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      internal string lineEnd = "\n";

      public RequestBase()
      {
         Responses = new List<IResponse>();
      }

      public abstract bool ResponseExpected { get; }
      public abstract string GetRequestString();
      public IList<IResponse> Responses { get; set; }

   }
}
