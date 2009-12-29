﻿using System.Collections.Generic;
using System.Xml.Linq;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   /// <summary>
   /// Request interface to process cvs requests
   /// </summary>
   public interface IRequest
   {
      bool ResponseExpected { get; }
      IList<IResponse> Responses { get; set; }
      string[] Lines { get; }
      RequestType Type { get; }
      string GetRequestString();
      XElement GetXElement();
   }

   /// <summary>
   /// used to differentiate the auth request from all the other requests on a command
   /// </summary>
   public interface IAuthRequest : IRequest
   {
      AuthStatus Status { get; }
   }

   public interface IFileRequest : IRequest
   {
      long FileLength { get; }
      byte[] FileContents { get; set; }
   }
}