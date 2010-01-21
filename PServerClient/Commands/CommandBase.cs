using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using log4net;
using log4net.Config;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   /// <summary>
   /// Abstract base for command classes that implement the ICommand interface.
   /// </summary>
   /// <example>
   /// 	<code lang="CS" source="C:\_code\Testing\PServerClient\PServerClient.IntegrationTests\VersionCommandTest.cs">
   /// 	</code>
   /// </example>
   /// <remarks>
   /// The Execute method is the way for all commands to perform their requests and
   /// receive their responses. There are PreExecute, PostRequest, and PostExecute methods
   /// that can be overridden as a way for commands to customize their execution.
   /// </remarks>
   /// <requirements>
   /// The Root object must be instantiated with the values necessary to communicate
   /// with the CVS server. In addition, any commands that interact with the local file system
   /// will need the WorkingDirectory and RootModule properties of the Root object
   /// populated.
   /// </requirements>
   public abstract class CommandBase : ICommand
   {
      private readonly ILog _logger;
      private AuthStatus _status;

      private IConnection _connection;

      protected CommandBase(IRoot root, IConnection connection)
      {
         BasicConfigurator.Configure();
         _logger = LogManager.GetLogger(typeof(CommandBase));
         _connection = connection;
         Responses = new List<IResponse>();
         UserMessages = new List<string>();

         Root = root;
         Requests = new List<IRequest>();
         RequiredRequests = new List<IRequest>();
         Items = new List<ICommandItem>();

         // add auth and other required requests for most commands
         RequiredRequests.Add(new AuthRequest(root));
         RequiredRequests.Add(new UseUnchangedRequest());
         RequiredRequests.Add(new ValidResponsesRequest(RequestHelper.ValidResponses));
         RequiredRequests.Add(new ValidRequestsRequest());
         ValidRequestTypes = new List<RequestType>();
      }

      /// <summary>Gets the result of CVS authentication</summary>
      public AuthStatus AuthStatus
      {
         get
         {
            return _status;
         }
      }

      /// <summary>
      /// Gets the Root instance. Contains CVS connection, command setup and local file system
      /// information.
      /// </summary>
      public IRoot Root { get; private set; }

      /// <summary>
      /// Gets the command type.
      /// </summary>
      /// <value>The command type.</value>
      public abstract CommandType Type { get; }

      /// <summary>
      /// Gets or sets the collection of required requests. These requests are performed and
      /// evaluated before the main execution begins.
      /// </summary>
      public IList<IRequest> RequiredRequests { get; set; }

      /// <summary>
      /// Gets or sets the requests to perform the command.
      /// </summary>
      /// <value></value>
      public IList<IRequest> Requests { get; set; }

      /// <summary>
      /// Gets or sets the responses that are temporarily kept during processing.
      /// </summary>
      /// <value></value>
      public IList<IResponse> Responses { get; set; }

      /// <summary>
      /// Gets or sets the requests and responses that occur during execution
      /// For testing and troubleshooting. The entire CVS client/server conversation is
      /// stored here as it was executed. The storage of the request/response items should be
      /// turned off for better performance.
      /// </summary>
      /// <value></value>
      public IList<ICommandItem> Items { get; set; }

      /// <summary>
      /// Gets or sets the exit code.
      /// The end result of the command. If there was an "ok" response, this has a value
      /// "Succeeded". If there was an "error" response, this has a value of "Failed". If neither
      /// "ok" nor "error" was received, this is set to "Failed".
      /// </summary>
      /// <value>The exit code.</value>
      public ExitCode ExitCode { get; set; }

      /// <summary>
      /// Gets any messages that should be sent back to the user.
      /// </summary>
      /// <value></value>
      public IList<string> UserMessages { get; private set; }

      /// <summary>
      /// Gets or sets the valid request types.
      /// </summary>
      /// <value>The valid request types.</value>
      internal IList<RequestType> ValidRequestTypes { get; set; }

      /// <summary>
      /// Gets the connection.
      /// </summary>
      /// <value>The connection.</value>
      protected IConnection Connection
      {
         get
         {
            return _connection;
         }
      }

      /// <summary>
      /// Handles the execution of the CVS command. Ensures that all the requests
      /// are sent, and that responses are retrieved at the appropriate time.
      /// The processing of responses is handled by the command classes.
      /// </summary>
      public virtual void Execute()
      {
         _connection.Connect(Root);
         try
         {
            ExitCode = ExecuteRequiredRequests();
            if (ExitCode == ExitCode.Succeeded)
            {
               if (!AllRequestsAreValid())
                  throw new Exception("Request list contains invalid requests");
               BeforeExecute();
               foreach (IRequest request in Requests)
               {
                  DoRequest(request);
                  if (request.ResponseExpected)
                  {
                     AfterRequest(request);
                  }
               }
            }

            AfterExecute();
         }
         catch (Exception e)
         {
            _logger.Error("Command Execute exception", e);
            throw;
         }
         finally
         {
            _connection.Close();
            CleanUp();
         }
      }

      /// <summary>
      /// Gets the XML representation of the command.
      /// </summary>
      /// <returns>XDocument xml of the command object</returns>
      public XDocument GetXDocument()
      {
         XElement commandElement = new XElement("Command", new XElement("ClassName", GetType().FullName));
         XElement requestsElement = new XElement("RequiredRequests");
         foreach (IRequest request in RequiredRequests)
         {
            requestsElement.Add(request.GetXElement());
         }

         commandElement.Add(requestsElement);
         requestsElement = new XElement("Requests");
         foreach (IRequest request in Requests)
         {
            requestsElement.Add(request.GetXElement());
         }

         commandElement.Add(requestsElement);
         XElement commandItemsElement = new XElement("CommandItems");
         commandElement.Add(commandItemsElement);
         foreach (ICommandItem item in Items)
         {
            XElement itemElement = item.GetXElement();
            commandItemsElement.Add(itemElement);
         }

         XDocument xdoc = new XDocument(commandElement);
         return xdoc;
      }

      /// <summary>
      /// Executes the required requests.
      /// </summary>
      /// <returns>the result of the required requests. Succeeded or Failed.</returns>
      internal ExitCode ExecuteRequiredRequests()
      {
         // execute authentication request and check authentication status
         // before executing other requests
         IAuthRequest authRequest = RequiredRequests.OfType<IAuthRequest>().First();
         DoRequest(authRequest);
         IResponse response = _connection.GetResponse();
         if (response is IAuthResponse)
         {
            ProcessResponse(response);
            _status = ((IAuthResponse) response).Status;
         }
         else
         {
            Responses.Add(response);
            var responses = _connection.GetAllResponses();
            Responses = Responses.Union(responses).ToList();
            _status = AuthStatus.Error;
         }

         if (_status == AuthStatus.Authenticated)
         {
            IEnumerable<IRequest> otherRequests = RequiredRequests.Where(r => r.Type != RequestType.Auth && r.Type != RequestType.VerifyAuth);
            foreach (IRequest request in otherRequests)
            {
               DoRequest(request);
               if (request.ResponseExpected)
               {
                  ProcessRequestResponses();
               }
            }
         }

         // set the valid requests list
         var validRequestResponse = (ValidRequestsResponse) Responses.Where(r => r.Type == ResponseType.ValidRequests).FirstOrDefault();
         if (validRequestResponse != null)
         {
            ValidRequestTypes = validRequestResponse.ValidRequestTypes;
         }

         ProcessMessages();
         if (!PServerHelper.IsTestMode())
            RequiredRequests.Clear(); // remove requests already processed
         bool hasErrorResponse = Responses.Where(r => r.Type == ResponseType.Error).Count() > 0 ? true : false;
         bool hasOkResponse = Responses.Where(r => r.Type == ResponseType.Ok).Count() > 0 ? true : false;
         Responses = Responses.Where(r => !r.Processed).ToList(); // removed processed responses
         ExitCode code; 
         if (hasErrorResponse)
            code = ExitCode.Failed;
         else if (hasOkResponse)
            code = ExitCode.Succeeded;
         else
            code = ExitCode.Unknown;

         return code;
      }

      internal bool AllRequestsAreValid()
      {
         foreach (IRequest request in Requests)
         {
            if (ValidRequestTypes.Count > 0)
            {
               if (!ValidRequestTypes.Contains(request.Type))
                  return false;
            }
         }

         return true;
      }

      internal void ProcessMessages()
      {
         IEnumerable<IMessageResponse> messages = Responses.OfType<IMessageResponse>();
         foreach (IMessageResponse message in messages)
         {
            if (!message.Processed)
            {
               message.Process();
            }

            UserMessages.Add(message.Message);
         }
      }

      protected internal virtual void AfterRequest(IRequest request)
      {
         ProcessRequestResponses();
      }

      protected internal virtual void BeforeExecute()
      {
         // do nothing is default
      }

      protected internal virtual void AfterExecute()
      {
         ProcessMessages();
      }

      protected void DoRequest(IRequest request)
      {
         _connection.DoRequest(request);
         if (PServerHelper.IsTestMode())
            Items.Add(request);
      }

      protected void ProcessResponse(IResponse response)
      {
         if (response != null)
         {
            response.Process();
            if (PServerHelper.IsTestMode())
               Items.Add(response);
         }
      }

      protected void RemoveProcessedResponses()
      {
         var notProcessed = Responses.Where(r => !r.Processed);
         Responses = notProcessed.ToList(); 
      }

      private void ProcessRequestResponses()
      {
         var responses = _connection.GetAllResponses();
         foreach (IResponse response in responses)
         {
            ProcessResponse(response);
            Responses.Add(response);
         }
      }

      private void CleanUp()
      {
         if (!PServerHelper.IsTestMode())
            Requests.Clear();
         RemoveProcessedResponses();
      }
   }
}