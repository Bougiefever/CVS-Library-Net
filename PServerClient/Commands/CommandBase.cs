using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public abstract class CommandBase : ICommand
   {
      private IConnection _connection;
      private readonly log4net.ILog _logger;

      internal IList<RequestType> ValidRequestTypes;

      protected internal IList<IRequest> RequiredRequests { get; internal set; }

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

      protected CommandBase(Root root)
      {
         log4net.Config.BasicConfigurator.Configure();
         _logger = log4net.LogManager.GetLogger(typeof(CommandBase));

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

      public IList<IRequest> Requests { get; set; }
      public Root Root { get; private set; }
      public ExitCode ExitCode { get; private set; }

      public AuthStatus AuthStatus
      {
         get
         {
            AuthStatus status;
            try
            {
               IAuthRequest authRequest = RequiredRequests.OfType<IAuthRequest>().First();
               IAuthResponse response = (IAuthResponse)authRequest.Responses[0];
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
                  if (request.Responses.Where(r => r.ResponseType == ResponseType.Error).Count() > 0)
                  {
                     ExitCode = ExitCode.Failed;
                     break;
                  }
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

      public XDocument ResponsesXML()
      {
         XDocument xdoc = new XDocument();
         XElement responsesElement = new XElement("Responses");
         IList<IResponse> responses = GetResponses();
         foreach (IResponse response in responses)
         {
            XElement responseElement = response.ToXML();
            responsesElement.Add(responseElement);
         }
         xdoc.Add(responsesElement);
         return xdoc;
      }

      public virtual void PreExecute()
      {
         // default is do nothing
      }

      public virtual void PostExecute()
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
            IEnumerable<IRequest> otherRequests = RequiredRequests.Where(r => r.RequestType != RequestType.Auth && r.RequestType != RequestType.VerifyAuth);
            foreach (IRequest request in otherRequests)
            {
               request.Responses = Connection.DoRequest(request);
               if (request.Responses.Where(r => r.ResponseType == ResponseType.Error).Count() > 0)
               {
                  code = ExitCode.Failed;
                  break;
               }
            }
         }
         if (code == ExitCode.Succeeded && authRequest.RequestType == RequestType.Auth)
         {
            // set the valid request list
            ValidRequestsRequest validRequests = (ValidRequestsRequest)RequiredRequests.Where(rr => rr.RequestType == RequestType.ValidRequests).FirstOrDefault();
            if (validRequests != null)
            {
               ValidRequestResponse vr = (ValidRequestResponse) validRequests.Responses[0];
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
               if (!ValidRequestTypes.Contains(request.RequestType))
                  return false;
            }
         }
         return true;
      }

      internal IList<IResponse> GetResponses()
      {
         IList<IResponse> responses = new List<IResponse>();
         foreach (IRequest request in Requests)
         {
            foreach (IResponse response in request.Responses)
            {
               responses.Add(response);
            }
         }
         return responses;
      }
   }
}
