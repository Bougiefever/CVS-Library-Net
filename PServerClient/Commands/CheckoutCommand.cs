using System;
using System.Collections.Generic;
using System.Linq;
using PServerClient.LocalFileSystem;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Responses.Messages;

namespace PServerClient.Commands
{
   public class CheckoutCommand : CommandBase
   {
      internal IList<IMessage> _messages;

      public CheckoutCommand(CvsRoot root) : base(root)
      {
         Requests.Add(new RootRequest(root));
         Requests.Add(new GlobalOptionRequest("-q"));
         Requests.Add(new ArgumentRequest("-N"));
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(root));
         Requests.Add(new CheckOutRequest());
      }

      public override void PostExecute()
      {
         IList<IResponse> checkOutResponses = Requests.Where(r => r.RequestType == RequestType.CheckOut)
            .First()
            .Responses;
         foreach (IResponse response in checkOutResponses)
         {
            Console.WriteLine(response.ResponseType + ": ");
            Console.WriteLine(response.DisplayResponse());
         }
         if (checkOutResponses.Where(r => r.ResponseType == ResponseType.MessageTag).Count() > 1)
         {
            ReadMTStyleResponses(checkOutResponses);
         }
         else
         {
            ReadMUStyleResponses(checkOutResponses);
         }
      }

      internal void ReadMTStyleResponses(IList<IResponse> responses)
      {
         CvsItemFactory factory = new CvsItemFactory();
         int i = 0;
         IResponse response = responses[i++];
         while (response.ResponseType != ResponseType.Ok)
         {
            if (response.ResponseType == ResponseType.ModTime)
            {
               IList<IResponse> entryResponses = new List<IResponse> {response};
               response = responses[i++];
               while (response.ResponseType == ResponseType.MessageTag)
               {
                  entryResponses.Add(response);
                  response = responses[i++];
               }
               entryResponses.Add(response);
            }
            response = responses[i++];
         }
      }

      internal void ReadMUStyleResponses(IList<IResponse> responses)
      {
         
      }
   }
}
