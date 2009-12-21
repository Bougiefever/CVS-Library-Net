using System.Collections.Generic;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      int LineCount { get; }
      ResponseType ResponseType { get; }
      void ProcessResponse(IList<string> lines);
      string DisplayResponse();
      XDocument ToXML();
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