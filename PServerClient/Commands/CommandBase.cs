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

      protected CommandBase(CvsRoot cvsRoot)
      {
         CvsRoot = cvsRoot;
         Requests = new List<IRequest>();
      }

      public IList<IRequest> Requests { get; set; }
      public CvsRoot CvsRoot { get; private set; }

      public string ErrorMessage
      {
         get
         {
            string message = string.Empty;
            foreach (IRequest request in Requests)
            {
               IResponse response = request.Response;
               message += response.ErrorMessage;
            }
            return message;
         }
      }

      public AuthStatus AuthStatus
      {
         get
         {
            IAuthRequest authRequest = Requests.OfType<IAuthRequest>().First();
            IAuthResponse response = (IAuthResponse)authRequest.Response;
            return response.Status;
         }
      }

      public void Execute()
      {
         Connection.Connect(CvsRoot.Host, CvsRoot.Port);
         try
         {
            bool success = true;
            foreach (IRequest request in Requests)
            {
               if (success)
               {
                  Connection.DoRequest(request);
                  request.GetResponse();
                  IResponse response = request.Response;
                  response.ProcessResponse();
                  success = response.Success;
               }
            }
         }
         //catch (Exception e)
         //{

         //}
         finally
         {
            Connection.Close();
         }
      }

      public bool Success
      {
         get
         {
            bool success = true;
            foreach (IRequest request in Requests)
            {
               IResponse response = request.Response;
               if (success)
                  success = response.Success;
            }
            return success;
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
