using System.Collections.Generic;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      int LineCount { get; }
      ResponseType Type { get; }
      bool Processed { get; set; }
      IList<string> Lines { get; set; }
      void Initialize(IList<string> lines);
      void Process();
      string Display();
      XElement GetXElement();
   }

   public interface IAuthResponse : IResponse
   {
      AuthStatus Status { get; }
   }

   public interface IFileResponse : IResponse
   {
      string Module { get; set; }
      string RepositoryPath { get; set; }
      string EntryLine { get; set; }
      string Name { get; }
      string Revision { get; }
      string Properties { get; }
      long Length { get; set; }
      byte[] Contents { get; set; }
      FileType FileType { get; set; }
   }

   public interface IMessageResponse : IResponse
   {
      string Message { get; }
   }
}