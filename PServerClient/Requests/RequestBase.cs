using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      protected string CvsResponse;
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
      }

      public IResponse Response { get; set; }
   }
}
