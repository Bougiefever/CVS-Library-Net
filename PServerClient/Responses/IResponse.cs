using System.Collections.Generic;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      int LineCount { get; }
      ResponseType Type { get; }
      IList<string> Lines { get; set; }
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
      string ModuleName { get; set; }
      string RepositoryPath { get; set; }
      string EntryLine { get; set; }
      ReceiveFile File { get; set; }
   }

   public interface IMessageResponse : IResponse
   {
      string Message { get; }
   }
}