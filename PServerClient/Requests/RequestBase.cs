using System.Collections.Generic;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      internal string LineEnd = "\n";

      public abstract bool ResponseExpected { get; }
      public abstract string GetRequestString();
      public abstract RequestType RequestType { get; }
      public string RequestName
      {
         get
         {
            return RequestHelper.RequestNames[(int) RequestType];
         }
      }
      public IList<IResponse> Responses { get; set; }

   }
}