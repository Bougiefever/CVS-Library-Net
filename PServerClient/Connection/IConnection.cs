using PServerClient.Requests;

namespace PServerClient.Connection
{
   public interface IConnection
   {
      ICvsTcpClient TcpClient { get; set; }
      void Connect(string host, int port);
      void DoRequest(IRequest request);
      void Close();
   }
}
