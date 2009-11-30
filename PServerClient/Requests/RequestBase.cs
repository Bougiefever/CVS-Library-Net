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

      public string RawCvsResponse { get; set; }
      public abstract bool ResponseExpected { get; }
      public abstract string GetRequestString();
      public virtual void SetCvsResponse(string response)
      {
         RawCvsResponse = response;
      }

      //public void GetResponses()
      //{
      //   ResponseFactory factory = new ResponseFactory();
      //   Responses = factory.CreateResponses
      //   //IResponse response = factory.CreateResponse(RawCvsResponse);
      //   //Responses.Add(response);
      //   //response.ResponseString = RawCvsResponse;
      //}

      public IList<IResponse> Responses { get; set; }

      #region IRequest Members


      public void ProcessResponses(IList<string> cvsResponseLines)
      {
         ResponseFactory factory = new ResponseFactory();
         Responses = factory.CreateResponses(cvsResponseLines);
      }

      #endregion
   }
}
