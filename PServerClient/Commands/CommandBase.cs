using System;
using System.Collections.Generic;
using System.Linq;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public abstract class CommandBase : ICommand
   {
      private IConnection _connection;
      private log4net.ILog _logger;

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

      protected CommandBase(CvsRoot root)
      {
         CvsRoot = root;
         Requests = new List<IRequest>();
         log4net.Config.BasicConfigurator.Configure();
         _logger = log4net.LogManager.GetLogger(typeof(CommandBase));
      }

      public IList<IRequest> Requests { get; set; }
      public CvsRoot CvsRoot { get; private set; }

      public AuthStatus AuthStatus
      {
         get
         {
            AuthStatus status;
            try
            {
               IAuthRequest authRequest = Requests.OfType<IAuthRequest>().First();
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

      public void Execute()
      {
         Connection.Connect(CvsRoot.Host, CvsRoot.Port);


         try
         {
            // execute authentication request and check authentication status
            // before executing other requests
            IEnumerable<IRequest> authRequests = Requests.OfType<IAuthRequest>().Cast<IRequest>();
            var otherRequests = Requests.Except(authRequests);
            IAuthRequest auth = authRequests.Cast<IAuthRequest>().First();
            auth.Responses = Connection.DoRequest(auth);
            if (auth.Status == AuthStatus.Authenticated)
            {
               foreach (IRequest request in otherRequests)
               {
                  request.Responses = Connection.DoRequest(request);
               }
            }
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

      public virtual void PreExecute()
      {

      }
      public virtual void PostExecute()
      {

      }
   }
}
