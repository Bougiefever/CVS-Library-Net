using System.Collections.Generic;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      int LineCount { get; }
      ResponseType ResponseType { get; }
      void ProcessResponse(IList<string> lines);
      string ResponseText { get; set; }
   }

   public interface IAuthResponse : IResponse
   {
      AuthStatus Status { get; }
   }

   public interface IFileResponse : IResponse
   {
      long FileLength { get; set; }
      Entry CvsEntry { get; set; }
   }
}
