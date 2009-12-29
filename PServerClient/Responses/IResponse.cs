using System.Collections.Generic;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      int LineCount { get; }
      ResponseType Type { get; }
      string[] Lines { get; }
      void Process(IList<string> lines);
      string Display();
      XElement GetXElement();
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