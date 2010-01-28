using System.Collections.Generic;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   /// <summary>
   /// Base for commands that receive files from CVS
   /// </summary>
   public abstract class ReceiveFileCommandBase : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ReceiveFileCommandBase"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      protected ReceiveFileCommandBase(IRoot root, IConnection connection)
         : base(root, connection)
      {
         CurrentFolder = root.RootFolder;
      }

      /// <summary>
      /// Gets a value indicating whether to save CVS folder information
      /// </summary>
      /// <value><c>true</c> if [save CVS folder]; otherwise, <c>false</c>.</value>
      public abstract bool SaveCVSFolder { get; }

      /// <summary>
      /// Gets or sets the current folder.
      /// </summary>
      /// <value>The current folder.</value>
      protected Folder CurrentFolder { get; set; }

      /// <summary>
      /// Processes the responses of each request. When all the requests 
      /// needed to save a file have been retrieved from the CVS server,
      /// the file is saved and the information contained in the command
      /// is deleted for performance.
      /// </summary>
      /// <param name="request">The request.</param>
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
                     Entry entry = processor.AddFile(CurrentFolder, file);
                     entry.Save(true);
                     Folder folder = entry.Parent;
                     if (SaveCVSFolder)
                     {
                        if (folder.Module != CurrentFolder.Module)
                           folder.SaveCVSFolder();
                     }

                     CurrentFolder = folder;
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
         }
         else
         {
            base.AfterRequest(request);
         }
      }
   }
}