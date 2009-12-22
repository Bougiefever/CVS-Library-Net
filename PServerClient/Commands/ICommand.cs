using System.Collections.Generic;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public interface ICommand
   {
      IList<IRequest> Requests { get; set; }
      Root Root { get; }
      ExitCode ExitCode { get; }
      CommandType CommandType { get; }
      IList<IRequest> RequiredRequests { get; }
      void Execute();
   }
}