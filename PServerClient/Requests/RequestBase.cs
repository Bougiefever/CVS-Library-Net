using System.Collections.Generic;
using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      internal string LineEnd = "\n";

      public abstract bool ResponseExpected { get; }
      public virtual string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < RequestLines.Length; i++)
         {
            sb.Append(RequestLines[i]).Append(LineEnd);
         }
         string request = sb.ToString();
         return request;
      }
      public abstract RequestType RequestType { get; }
      public string[] RequestLines { get; internal set; }
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