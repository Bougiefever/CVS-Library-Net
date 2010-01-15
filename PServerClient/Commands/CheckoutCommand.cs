using System.Collections.Generic;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public class CheckOutCommand : CommandBase
   {
      private Folder _currentFolder;

      public CheckOutCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         _currentFolder = root.RootFolder;
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

      protected internal override void AfterRequest(IRequest request)
      {
         if (request is ExportRequest)
         {
            IResponse response;
            IFileResponseGroup file = null;
            IList<IResponse> messages = null;
            bool gettingFile = false;
            ResponseProcessor processor = new ResponseProcessor();

            response = Connection.GetResponse();
            ProcessResponse(response);
            do
            {
               if (gettingFile)
               {
                  if (response is MTMessageResponse)
                     messages.Add(response);
                  if (response is UpdatedResponse)
                  {
                     messages = ResponseHelper.CollapseMessagesInResponses(messages);
                     file.MT = (IMessageResponse)messages[0];
                     file.FileResponse = (IFileResponse)response;

                     // process each file
                     Entry entry = processor.AddFile(_currentFolder, file);
                     entry.Save(true);
                     Folder folder = entry.Parent;
                     if (folder.Module != _currentFolder.Module)
                        folder.SaveCVSFolder();
                     _currentFolder = folder;
                     file = null;
                     RemoveProcessedResponses();
                     gettingFile = false; // all done getting file
                  }
               }
               else
               {
                  if (response is ModTimeResponse)
                  {
                     file = new FileResponseGroup();
                     messages = new List<IResponse>();
                     file.ModTime = (ModTimeResponse)response;
                     gettingFile = true;
                  }
               }

               response = Connection.GetResponse();
               ProcessResponse(response);
            }
            while (response != null);
            _currentFolder.SaveCVSFolder();
         }
         else
         {
            base.AfterRequest(request);
         }
      }
   }
}