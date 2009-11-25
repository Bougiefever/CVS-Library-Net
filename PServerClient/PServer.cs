using System;
using System.Net.Sockets;
using System.Text;
using PServerClient.Connection;

namespace PServerClient
{
   public class PServer
   {
      private CvsRoot _cvsRoot;
      private IConnection _connection;
      

      public PServer(CvsRoot cvsRoot)
      {
         _cvsRoot = cvsRoot;
      }

      public CvsRoot CvsRoot
      {
         get { return _cvsRoot; }
         set { _cvsRoot = value; }
      }

      //public IConnection Connection
      //{
      //   get
      //   {
      //      if (_connection == null)
      //         _connection = new PServerConnection();
      //      return _connection;
      //   }
      //   set { _connection = value; }
      //}

      public void DoCommand()
      {
         

      }
   }

   

   //public class PServer
   //{
   //   private ITcpConnection _connection;
   //   public string Host { get; set; }
   //   public int Port { get; set; }
   //   public string User { get; set; }
   //   public string Password { get; set; }
   //   public CvsRoot CvsRoot { get; set; }
   //   public ITcpConnection Connection
   //   {
   //      get
   //      {
   //         if (_connection == null)
   //         {
   //            _connection = new PServerTcpConnection(Host, Port);
   //         }
   //         return _connection;
   //      }
   //      set
   //      {
   //         _connection = value;
   //      }
   //   }


   //   public PServer(string host, string port, string user, string password, string repositoryPath)
   //   {
   //      Host = host;
   //      User = user;
   //      Password = password.ScramblePassword();
   //      CvsRoot = new CvsRoot(Host, User, repositoryPath);
   //      if (port.Length == 0)
   //         Port = 2401;
   //      else
   //         Port = Convert.ToInt32(port);
   //   }

   //   public ICommandResponse DoCommand(ICommandRequest request)
   //   {
   //      return null;
   //   }

   //   public string VerifyAuthentication()
   //   {
   //      string verificationRequest = "BEGIN VERIFICATION REQUEST\n"
   //                                   + CvsRoot.RepositoryPath + "\n"
   //                                   + User + "\n"
   //                                   + Password + "\n"
   //                                   + "END VERIFICATION REQUEST\n";
   //      this.Connection.SendRequest(verificationRequest);
   //      return this.Connection.Response;
   //   }

   //   public string AuthenticationRequest()
   //   {
   //      return "BEGIN AUTH REQUEST\n"
   //                                   + CvsRoot.RepositoryPath + "\n"
   //                                   + User + "\n"
   //                                   + Password + "\n"
   //                                   + "END AUTH REQUEST\n";
   //   }

   //   public string ValidRequestsRequest()
   //   {
   //      string request = "valid-requests \n";
   //      TcpClient tcpClient = new TcpClient();
   //      tcpClient.Connect(Host, Port);
   //      NetworkStream stream = tcpClient.GetStream();
   //      byte[] sendBuffer = Encoding.ASCII.GetBytes(AuthenticationRequest());
   //      string response = string.Empty;
   //      if (stream.CanRead && stream.CanWrite)
   //      {
   //         try
   //         {
   //            stream.Write(sendBuffer, 0, sendBuffer.Length);
   //            byte[] receiveBuffer = new byte[tcpClient.ReceiveBufferSize];
   //            stream.Read(receiveBuffer, 0, tcpClient.ReceiveBufferSize);
   //            response = Encoding.ASCII.GetString(receiveBuffer);
   //            if (response.Contains("I LOVE YOU"))
   //            {
   //               sendBuffer = Encoding.ASCII.GetBytes(request);
   //               stream.Write(sendBuffer, 0, sendBuffer.Length);
   //               receiveBuffer = new byte[tcpClient.ReceiveBufferSize];
   //               stream.Read(receiveBuffer, 0, tcpClient.ReceiveBufferSize);
   //               response = Encoding.ASCII.GetString(receiveBuffer);
   //            }
   //         }
   //         finally
   //         {
   //            tcpClient.Close();
   //         }
   //      }
   //      return response;
   //   }
   //}

   public interface ICommandRequest
   {
      string RequestString { get; set; }
   }

   public interface ICommandResponse
   {
      string ResponseString { get; set; }
   }

   public class MyVerifyAuthenticationRequest : ICommandRequest
   {
      private CvsRoot _root;
      private string _username;
      private string _password;

      public MyVerifyAuthenticationRequest(CvsRoot root, string username, string password)
      {
         
      }
      
      public string RequestString
      {
         get { throw new NotImplementedException(); }
         set { throw new NotImplementedException(); }
      }
   }

   public interface ITcpConnection
   {
      string Host { get; set; }
      int Port { get; set; }
      string Response { get; }
      void SendRequest(string request);
   }

   public class PServerTcpConnection : ITcpConnection
   {
      public PServerTcpConnection(string host, int port)
      {
         Host = host;
         Port = port;
      }
      public string Host { get; set; }
      public int Port { get; set; }
      public string Response { get; private set; }

      public void SendRequest(string request)
      {
         TcpClient tcpClient = new TcpClient();
         tcpClient.Connect(Host, Port);
         NetworkStream stream = tcpClient.GetStream();
         byte[] sendBuffer = Encoding.ASCII.GetBytes(request);
         if (stream.CanRead && stream.CanWrite)
         {
            try
            {
               stream.Write(sendBuffer, 0, sendBuffer.Length);
               byte[] receiveBuffer = new byte[tcpClient.ReceiveBufferSize];
               stream.Read(receiveBuffer, 0, tcpClient.ReceiveBufferSize);
               Response = Encoding.ASCII.GetString(receiveBuffer);
            }
            finally
            {
               tcpClient.Close();
            }
         }
      }
   }

   public interface IPServerRequest
   {
      string RequestString { get; }

   }

}
