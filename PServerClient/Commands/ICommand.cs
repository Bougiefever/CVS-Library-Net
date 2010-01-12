using System.Collections.Generic;
using System.Xml.Linq;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public interface ICommand
   {
      IList<IRequest> Requests { get; set; }

      IList<IResponse> Responses { get; set; }

      IList<ICommandItem> Items { get; set; }

      ExitCode ExitCode { get; set; }

      CommandType Type { get; }

      IList<IRequest> RequiredRequests { get; set; }

      IList<string> UserMessages { get; }

      void Execute();

      XDocument GetXDocument();
   }

   public interface ICommandItem
   {
      bool Processed { get; set; }

      IList<string> Lines { get; set; }

      XElement GetXElement();
   }
}