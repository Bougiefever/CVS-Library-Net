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
      private IConnection _connection;

      internal IList<RequestType> ValidRequestTypes;

      protected CommandBase(IRoot root)
      {
         BasicConfigurator.Configure();
         _logger = LogManager.GetLogger(typeof (CommandBase));

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

      public IConnection Connection
      {
         get
         {
            if (_connection == null)
               _connection = new PServerConnection();
            return _connection;
         }
         set { _connection = value; }
      }

      public AuthStatus AuthStatus
      {
         get
         {
            AuthStatus status;
            try
            {
               IAuthRequest authRequest = RequiredRequests.OfType<IAuthRequest>().First();
               IAuthResponse response = (IAuthResponse) authRequest.Responses[0];
               status = response.Status;
            }
            catch (Exception e)
            {
               _logger.Error("Not Authenticated", e);
               status = AuthStatus.Error;
            }
            return status;
         }
      }

      #region ICommand Members

      public abstract CommandType Type { get; }
      public IList<IRequest> RequiredRequests { get; set; }

      public IList<IRequest> Requests { get; set; }
      public IRoot Root { get; private set; }
      public ExitCode ExitCode { get; set; }

      public virtual void Execute()
      {
         Connection.Connect(Root);
         try
         {
            ExitCode = ExecuteRequiredRequests();
            if (ExitCode == ExitCode.Succeeded)
            {
               if (!AllRequestsAreValid())
                  throw new Exception("Request list contains invalid requests");
               PreExecute();
               foreach (IRequest request in Requests)
               {
                  request.Responses = Connection.DoRequest(request);
                  if (request.Responses.Where(r => r.Type == ResponseType.Error).Count() > 0)
                  {
                     ExitCode = ExitCode.Failed;
                     break;
                  }
                  PostProcessRequest();
               }
            }
            if (ExitCode == ExitCode.Succeeded)
               PostExecute();
         }
         catch (Exception e)
         {
            _logger.Error("Command Execute exception", e);
            throw;
         }
         finally
         {
            Connection.Close();
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
         XDocument xdoc = new XDocument(commandElement);
         return xdoc;
      }

      #endregion

      protected internal virtual void PreExecute()
      {
         // default is do nothing
      }

      protected internal virtual void PostProcessRequest()
      {
         // default is do nothing
      }

      protected internal virtual void PostExecute()
      {
         // default is do nothing
      }

      internal ExitCode ExecuteRequiredRequests()
      {
         // execute authentication request and check authentication status
         // before executing other requests
         IAuthRequest authRequest = RequiredRequests.OfType<IAuthRequest>().First();
         authRequest.Responses = Connection.DoRequest(authRequest);
         AuthStatus status = authRequest.Status;
         ExitCode code = ExitCode.Failed;
         if (status == AuthStatus.Authenticated)
         {
            code = ExitCode.Succeeded;
            IEnumerable<IRequest> otherRequests = RequiredRequests.Where(r => r.Type != RequestType.Auth && r.Type != RequestType.VerifyAuth);
            foreach (IRequest request in otherRequests)
            {
               request.Responses = Connection.DoRequest(request);
               if (request.Responses.Where(r => r.Type == ResponseType.Error).Count() > 0)
               {
                  code = ExitCode.Failed;
                  break;
               }
            }
         }
         if (code == ExitCode.Succeeded && authRequest.Type == RequestType.Auth)
         {
            // set the valid request list
            ValidRequestsRequest validRequests = (ValidRequestsRequest) RequiredRequests.Where(rr => rr.Type == RequestType.ValidRequests).FirstOrDefault();
            if (validRequests != null)
            {
               ValidRequestsResponse vr = (ValidRequestsResponse) validRequests.Responses[0];
               ValidRequestTypes = vr.ValidRequestTypes;
            }
         }
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
   }
}