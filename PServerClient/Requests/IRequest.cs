﻿using PServerClient.Responses;
using System.Collections.Generic;

namespace PServerClient.Requests
{

   /// <summary>
   /// Request interface to process cvs requests
   /// </summary>
   public interface IRequest 
   {
      bool ResponseExpected { get; }
      string RawCvsResponse { get; set; }
      string GetRequestString();
      //void SetCvsResponse(string response);
      void ProcessResponses(IList<string> cvsResponseLines);
      IList<IResponse> Responses { get; set; }
   }

   /// <summary>
   /// used to differentiate the auth request from all the other requests on a command
   /// </summary>
   public interface IAuthRequest : IRequest
   {
      
   }
}
