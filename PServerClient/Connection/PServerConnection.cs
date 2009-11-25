using PServerClient.Requests;

namespace PServerClient.Connection
{
   public class PServerConnection : IConnection
   {
      private ICvsTcpClient _cvsTcpClient;
      
      public ICvsTcpClient TcpClient
      {
         get
         {
            if (_cvsTcpClient == null)
               _cvsTcpClient = new CvsTcpClient();
            return _cvsTcpClient;
         }
         set
         {
            _cvsTcpClient = value;
         }
      }

      public void Connect(string host, int port)
      {
         TcpClient.Connect(host, port);
      }

      public void DoRequest(IRequest request)
      {
         string requestString = request.GetRequestString();
         byte[] sendBuffer = PServerHelper.EncodeString(requestString);
         TcpClient.Write(sendBuffer);
         string cvsResponse = string.Empty;
         if (request.ResponseExpected)
         {
            byte[] receiveBuffer = TcpClient.Read();
            cvsResponse = PServerHelper.DecodeString(receiveBuffer);
         }
         request.SetCvsResponse(cvsResponse);
      }

      public void Close()
      {
         TcpClient.Close();
      }


   }
}
