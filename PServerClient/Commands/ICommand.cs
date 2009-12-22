using System.Collections.Generic;
using System.Xml.Linq;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public interface ICommand
   {
      IList<IRequest> Requests { get; set;}
      Root Root { get; }
      ExitCode ExitCode { get; }
      void Execute();
   }
}
