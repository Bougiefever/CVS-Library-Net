using System.Collections.Generic;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public interface ICommand
   {
      IList<IRequest> Requests { get; set;}
      CvsRoot CvsRoot { get; }
      void Execute();
   }
}
