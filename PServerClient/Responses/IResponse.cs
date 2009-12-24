﻿using System.Collections.Generic;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      int LineCount { get; }
      ResponseType Type { get; }
      string[] ResponseLines { get; }
      void ProcessResponse(IList<string> lines);
      string DisplayResponse();
   }

   public interface IAuthResponse : IResponse
   {
      AuthStatus Status { get; }
   }

   public interface IFileResponse : IResponse
   {
      ReceiveFile File { get; set; }
   }
}