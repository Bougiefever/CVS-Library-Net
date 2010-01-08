using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class CheckOutCommand : CommandBase
   {
      public CheckOutCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         Requests.Add(new RootRequest(root.Repository));
         Requests.Add(new GlobalOptionRequest("-q")); // somewhat quiet
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(".", root.Repository + "/" + root.Module));
         Requests.Add(new CheckOutRequest());
      }

      public override CommandType Type
      {
         get
         {
            return CommandType.CheckOut;
         }
      }

      protected internal override void AfterExecute()
      {
         ////IList<IResponse> checkOutResponses = Requests.Where(r => r.Type == RequestType.CheckOut)
         ////   .First()
         ////   .Responses;
         ////foreach (IResponse response in checkOutResponses)
         ////{
         ////   Console.WriteLine(response.Type + ": ");
         ////   Console.WriteLine(response.Display());
         ////}
         ////CVSFileReceiver fileReceiver = new CVSFileReceiver(Root);
         ////fileReceiver.ProcessCheckoutResponses(checkOutResponses);
      }
   }
}