using System;
using System.Collections.Generic;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public class ExportCommand : CommandBase
   {
      private Folder _currentFolder;

      public ExportCommand(IRoot root, IConnection connection, DateTime exportDate)
         : base(root, connection)
      {
         string dateArg = GetExportDate(exportDate);
         IRequest exportTypeRequest = new ArgumentRequest(dateArg);
         StartUp(root, exportTypeRequest);
      }

      public ExportCommand(IRoot root, IConnection connection, string tag)
         : base(root, connection)
      {
         string tagArg = "-r " + tag;
         IRequest exportTypeRequest = new ArgumentRequest(tagArg);
         StartUp(root, exportTypeRequest);
      }

      private void StartUp(IRoot root, IRequest exportTypeRequest)
      {
         _currentFolder = root.RootFolder;

         Requests.Add(new RootRequest(root.Repository));
         Requests.Add(new GlobalOptionRequest("-q")); // somewhat quiet
         Requests.Add(exportTypeRequest);
         Requests.Add(new ArgumentRequest("-R"));
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(".", root.Repository + "/" + root.Module));
         Requests.Add(new ExportRequest());
      }

      public override CommandType Type
      {
         get
         {
            return CommandType.Export;
         }
      }

      public IList<IFileResponseGroup> FileGroups { get; set; }

      internal string GetExportDate(DateTime exportDate)
      {
         string mydate = exportDate.ToRfc822();
         return string.Format("-D {0}", mydate);
      }

      protected internal override void AfterRequest(IRequest request)
      {
         if (request is ExportRequest)
         {
            //FileGroups = new List<IFileResponseGroup>();
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
                     //FileGroups.Add(file);
                     _currentFolder = processor.AddFile(_currentFolder, file);
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
                  //else
                  //   base.AfterRequest(request);
               }
               response = Connection.GetResponse();
               ProcessResponse(response);
            }
            while (response != null);
         }
         else
         {
            base.AfterRequest(request);
         }
      }

      protected internal override void AfterExecute()
      {
         var processor = new ResponseProcessor();
         //Root.RootFolder = processor.CreateCVSFileStructure(Root, FileGroups);
         //Root.RootFolder.Save(true);
         base.AfterExecute();
      }
   }
}