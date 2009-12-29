using System;
using System.Collections.Generic;
using System.Linq;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public class CheckOutCommand : CommandBase
   {
      public CheckOutCommand(Root root)
         : base(root)
      {
         Requests.Add(new RootRequest(root));
         Requests.Add(new GlobalOptionRequest("-q")); // somewhat quiet
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(root));
         Requests.Add(new CheckOutRequest());
      }

      public override CommandType Type { get { return CommandType.CheckOut; } }

      public override void PostExecute()
      {
         IList<IResponse> checkOutResponses = Requests.Where(r => r.Type == RequestType.CheckOut)
            .First()
            .Responses;
         foreach (IResponse response in checkOutResponses)
         {
            Console.WriteLine(response.Type + ": ");
            Console.WriteLine(response.Display());
         }
         ServerFileReceiver fileReceiver = new ServerFileReceiver(Root);
         fileReceiver.ProcessCheckoutResponses(checkOutResponses);
      }
   }
}