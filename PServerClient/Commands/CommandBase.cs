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
   public abstract class CommandBase : ICommand
   {
      private readonly ILog _logger;
      private AuthStatus _status;

      private IConnection _connection;
      internal IList<RequestType> ValidRequestTypes;

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

         // add auth and other required requests for most commands
         RequiredRequests.Add(new AuthRequest(root));
         RequiredRequests.Add(new UseUnchangedRequest());
         RequiredRequests.Add(new ValidResponsesRequest(RequestHelper.ValidResponses));
         RequiredRequests.Add(new ValidRequestsRequest());
         ValidRequestTypes = new List<RequestType>();
      }

      protected IConnection Connection 
      { 
         get
         {
            return _connection;
         }
      }

      public AuthStatus AuthStatus { get { return _status; } }
      public IRoot Root { get; private set; }

      public abstract CommandType Type { get; }
      public IList<IRequest> RequiredRequests { get; set; }

      public IList<IRequest> Requests { get; set; }
      public IList<IResponse> Responses { get; set; }
      public IList<string> UserMessages { get; private set; }
      public ExitCode ExitCode { get; set; }

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
                  _connection.DoRequest(request);
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
         }
      }

      public XDocument GetXDocument()
      {
         XElement commandElement = new XElement("Command",
                                       new XElement("ClassName", GetType().FullName)
                                       );
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
         XElement responsesElement = new XElement("Responses");
         commandElement.Add(responsesElement);
         foreach (IResponse response in Responses)
         {
            XElement responseElement = response.GetXElement();
            responsesElement.Add(responseElement);
         }

         XDocument xdoc = new XDocument(commandElement);
         return xdoc;
      }

      protected internal virtual void BeforeExecute()
      {
         //
      }

      protected internal virtual void AfterRequest(IRequest request)
      {
         ProcessRequestResponses(request);
      }

      protected internal virtual void AfterExecute()
      {
         ProcessMessages();
      }

      internal ExitCode ExecuteRequiredRequests()
      {
         // execute authentication request and check authentication status
         // before executing other requests
         IAuthRequest authRequest = RequiredRequests.OfType<IAuthRequest>().First();
         _connection.DoRequest(authRequest);
         IResponse response = _connection.GetResponse();
         if (response is IAuthResponse)
         {
            response.Process();
            _status = ((IAuthResponse)response).Status;
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
               _connection.DoRequest(request);
               if (request.ResponseExpected)
               {
                  ProcessRequestResponses(request);
               }
            }
         }

         // set the valid requests list
         var validRequestResponse = (ValidRequestsResponse)Responses.Where(r => r.Type == ResponseType.ValidRequests).FirstOrDefault();
         if (validRequestResponse != null)
         {
            ValidRequestTypes = validRequestResponse.ValidRequestTypes;
         }
         ProcessMessages();
         ExitCode code = Responses.Where(r => r.Type == ResponseType.Error).Count() > 0 ? ExitCode.Failed : ExitCode.Succeeded;
         Responses = Responses.Where(r => !r.Processed).ToList(); // removed processed responses
         RequiredRequests.Clear(); // remove requests already processed
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

      private void ProcessRequestResponses(IRequest request)
      {
         var responses = _connection.GetAllResponses();
         foreach (IResponse res in responses)
         {
            res.Process();
            Responses.Add(res);
         }
      }
   }
}