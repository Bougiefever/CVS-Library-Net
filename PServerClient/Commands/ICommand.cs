using System.Collections.Generic;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public interface ICommand
   {
      IList<IRequest> Requests { get; set;}
      CvsRoot Root { get; }
      ExitCode ExitCode { get; }
      void Execute();
   }
}
