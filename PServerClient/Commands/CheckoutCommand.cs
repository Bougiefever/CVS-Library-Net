using System;
using System.Collections.Generic;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   /// <summary>
   /// CVS checkout command. This retrieves the CVS module specified in the root object
   /// and saves the results to the file system with the CVS connection information needed for
   /// ongoing source control maintenance.
   /// </summary>
   /// <requirements>The Root instance must have a WorkingDirectory specified.</requirements>
   /// <notes>
   /// The checkout command stores the CVS folder in each directory in the Folder
   /// tree.
   /// </notes>
   /// <remarks>
   /// An exception will be thrown if the WorkingDirectory is not set to a valid
   /// value.
   /// </remarks>
   public class CheckOutCommand : ReceiveFileCommandBase
   {

      /// <summary>
      /// Initializes a new instance of the <see cref="CheckOutCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The PServer connection.</param>
      public CheckOutCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets the Command type.
      /// </summary>
      /// <value>The CommandType type.</value>
      public override CommandType Type
      {
         get
         {
            return CommandType.CheckOut;
         }
      }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         Requests.Add(new RootRequest(Root.Repository));
         Requests.Add(new GlobalOptionRequest(GlobalOption.Quiet)); // somewhat quiet
         Requests.Add(new ArgumentRequest(Root.Module));
         Requests.Add(new DirectoryRequest(".", Root.Repository + "/" + Root.Module));
         Requests.Add(new CheckOutRequest());
      }

      /////// <summary>
      /////// After each request, process the responses.
      /////// Determine if a complete file has been sent in the responses that can be
      /////// saved to the local file system.
      /////// </summary>
      /////// <param name="request">The request that was just sent.</param>
      ////protected internal override void AfterRequest(IRequest request)
      ////{
      ////   if (request is ExportRequest)
      ////   {
      ////      IFileResponseGroup file = null;
      ////      IList<IResponse> messages = null;
      ////      bool gettingFile = false;
      ////      ResponseProcessor processor = new ResponseProcessor();

      ////      IResponse response = Connection.GetResponse();
      ////      ProcessResponse(response);
      ////      do
      ////      {
      ////         if (gettingFile)
      ////         {
      ////            if (response is MTMessageResponse)
      ////               messages.Add(response);
      ////            if (response is UpdatedResponse)
      ////            {
      ////               messages = ResponseHelper.CollapseMessagesInResponses(messages);
      ////               file.MT = (IMessageResponse)messages[0];
      ////               file.FileResponse = (IFileResponse)response;

      ////               // process each file
      ////               Entry entry = processor.AddFile(CurrentFolder, file);
      ////               entry.Save(true);
      ////               Folder folder = entry.Parent;
      ////               if (folder.Module != CurrentFolder.Module)
      ////                  folder.SaveCVSFolder();
      ////               CurrentFolder = folder;
      ////               file = null;
      ////               RemoveProcessedResponses();
      ////               gettingFile = false; // all done getting file
      ////            }
      ////         }
      ////         else
      ////         {
      ////            if (response is ModTimeResponse)
      ////            {
      ////               file = new FileResponseGroup();
      ////               messages = new List<IResponse>();
      ////               file.ModTime = (ModTimeResponse)response;
      ////               gettingFile = true;
      ////            }
      ////         }

      ////         response = Connection.GetResponse();
      ////         ProcessResponse(response);
      ////      }
      ////      while (response != null);
      ////      CurrentFolder.SaveCVSFolder();
      ////   }
      ////   else
      ////   {
      ////      base.AfterRequest(request);
      ////   }
      ////}

      /// <summary>
      /// Gets a value indicating whether to save CVS folder information
      /// </summary>
      /// <value><c>true</c> if [save CVS folder]; otherwise, <c>false</c>.</value>
      protected override bool SaveCVSFolder
      {
         get
         {
            return true;
         }
      }
   }
}