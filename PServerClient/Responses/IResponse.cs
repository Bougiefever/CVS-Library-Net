using System.Collections.Generic;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      void ProcessResponse(IList<string> lines);
      int LineCount { get; }
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
