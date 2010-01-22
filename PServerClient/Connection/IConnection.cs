using System.Collections.Generic;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Connection
{
   /// <summary>
   /// The protocol connection. This contains all the information to communicate 
   /// between the command items and the tcp connection
   /// </summary>
   public interface IConnection
   {
      /// <summary>
      /// Gets or sets the TCP client instance
      /// </summary>
      /// <value>The TCP client.</value>
      ICVSTcpClient TcpClient { get; set; }

      /// <summary>
      /// Performs the tcp connection
      /// </summary>
      /// <param name="root">The CVS root.</param>
      void Connect(IRoot root);

      /// <summary>
      /// Send the string contained in the request
      /// </summary>
      /// <param name="request">The request.</param>
      void DoRequest(IRequest request);

      /// <summary>
      /// Gets one response.
      /// </summary>
      /// <returns>the response instance</returns>
      IResponse GetResponse();

      /// <summary>
      /// Gets all responses available from the CVS server
      /// </summary>
      /// <returns>The list of responses retrieved</returns>
      IList<IResponse> GetAllResponses();

      /// <summary>
      /// Closes the tcp connection
      /// </summary>
      void Close();
   }
}