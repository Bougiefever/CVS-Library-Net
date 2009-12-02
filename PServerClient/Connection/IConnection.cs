using PServerClient.Requests;
using PServerClient.Responses;
using System.Collections.Generic;

namespace PServerClient.Connection
{
   public interface IConnection
   {
      ICvsTcpClient TcpClient { get; set; }
      void Connect(string host, int port);
      IList<IResponse> DoRequest(IRequest request);
      void Close();
   }
}
