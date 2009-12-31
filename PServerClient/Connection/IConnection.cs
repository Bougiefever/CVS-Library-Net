using System.Collections.Generic;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Connection
{
   public interface IConnection
   {
      ICvsTcpClient TcpClient { get; set; }
      void Connect(IRoot root);
      IList<IResponse> DoRequest(IRequest request);
      void Close();
   }
}