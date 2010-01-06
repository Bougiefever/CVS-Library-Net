using System.Collections.Generic;
using System.Xml.Linq;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public interface ICommand
   {
      IList<IRequest> Requests { get; set; }
      IList<IResponse> Responses { get; set; }
      //IRoot Root { get; }
      ExitCode ExitCode { get; set; }
      CommandType Type { get; }
      IList<IRequest> RequiredRequests { get; set; }
      IList<string> UserMessages { get; }
      void Execute();
      XDocument GetXDocument();
   }
}