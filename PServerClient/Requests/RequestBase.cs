using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      public string CvsResponse { get; set; }
      public abstract bool ResponseExpected { get; }
      public abstract string GetRequestString();
      public virtual void SetCvsResponse(string response)
      {
         CvsResponse = response;
      }

      public void GetResponse()
      {
         ResponseFactory factory = new ResponseFactory();
         Response = factory.CreateResponse(CvsResponse);
         Response.ResponseString = CvsResponse;
      }

      public IResponse Response { get; set; }
   }
}
