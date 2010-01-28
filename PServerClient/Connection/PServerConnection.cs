using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Connection
{
   /// <summary>
   /// The PServer connection protocol to communicate with CVS
   /// </summary>
   public class PServerConnection : IConnection
   {
      private ICVSTcpClient _cvsTcpClient;

      /// <summary>
      /// Gets or sets the TCP client instance
      /// </summary>
      /// <value>The TCP client.</value>
      public ICVSTcpClient TcpClient
      {
         get
         {
            if (_cvsTcpClient == null)
               _cvsTcpClient = new CVSTcpClient();
            return _cvsTcpClient;
         }

         set
         {
            _cvsTcpClient = value;
         }
      }

      /// <summary>
      /// Performs the tcp connection
      /// </summary>
      /// <param name="root">The CVS root.</param>
      public void Connect(IRoot root)
      {
         TcpClient.Connect(root.Host, root.Port);
      }

      /// <summary>
      /// Send the string contained in the request
      /// </summary>
      /// <param name="request">The request.</param>
      public void DoRequest(IRequest request)
      {
         string requestString = request.GetRequestString();
         Console.WriteLine("C: " + requestString);
         byte[] sendBuffer = requestString.Encode();
         TcpClient.Write(sendBuffer);

         // do file stuff for request here
      }

      /// <summary>
      /// Closes the tcp connection
      /// </summary>
      public void Close()
      {
         TcpClient.Close();
      }

      /// <summary>
      /// Gets one response.
      /// </summary>
      /// <returns>the response instance</returns>
      public IResponse GetResponse()
      {
         IResponse response = null;
         string line = _cvsTcpClient.ReadLine();  // ReadLine();
         Console.WriteLine("S: " + (line ?? string.Empty));

         if (line != null)
         {
            PServerFactory factory = new PServerFactory();
            ResponseType responseType = factory.GetResponseType(line);
            response = factory.CreateResponse(responseType);
            IList<string> responseLines = GetResponseLines(line, responseType, response.LineCount);
            response.Initialize(responseLines);
            ////if (response is IFileResponse)
            ////{
            ////   response.Process();
            ////   IFileResponse fileResponse = (IFileResponse) response;
            ////   fileResponse.Contents = TcpClient.ReadBytes((int) fileResponse.Length);
            ////}
         }

         return response;
      }

      /// <summary>
      /// Gets the file response contents.
      /// </summary>
      /// <param name="response">The response.</param>
      public void GetFileResponseContents(IFileResponse response)
      {
         response.Contents = TcpClient.ReadBytes((int) response.Length);
      }

      /////// <summary>
      /////// Gets all responses available from the CVS server
      /////// </summary>
      /////// <returns>The list of responses retrieved</returns>
      ////public IList<IResponse> GetAllResponses()
      ////{
      ////   IList<IResponse> responses = new List<IResponse>();
      ////   string line;
      ////   do
      ////   {
      ////      line = _cvsTcpClient.ReadLine(); // ReadLine();
      ////      Console.WriteLine("S: " + (line ?? string.Empty));

      ////      if (line != null)
      ////      {
      ////         PServerFactory factory = new PServerFactory();
      ////         ResponseType responseType = factory.GetResponseType(line);
      ////         IResponse response = factory.CreateResponse(responseType);
      ////         IList<string> responseLines = GetResponseLines(line, responseType, response.LineCount);
      ////         response.Initialize(responseLines);
      ////         if (response is IFileResponse)
      ////         {
      ////            IFileResponse fileResponse = (IFileResponse) response;
      ////            fileResponse.Contents = TcpClient.ReadBytes((int) fileResponse.Length);
      ////         }

      ////         responses.Add(response);
      ////      }
      ////   }
      ////   while (line != null);
      ////   return responses;
      ////}

      internal IList<string> GetResponseLines(string line, ResponseType responseType, int lineCount)
      {
         string pattern = ResponseHelper.ResponsePatterns[(int) responseType];
         Match m = Regex.Match(line, pattern);
         string responseLine = m.Groups["data"].Value;
         IList<string> responseLines = new List<string> { responseLine };
         if (lineCount > 0)
         {
            for (int i = 1; i < lineCount; i++)
            {
               responseLine = _cvsTcpClient.ReadLine();
               responseLines.Add(responseLine);
            }
         }

         return responseLines;
      }

      ////internal string ReadLine()
      ////{
      ////   int i;
      ////   string line = null;
      ////   StringBuilder sb = new StringBuilder();
      ////   do
      ////   {
      ////      i = TcpClient.ReadByte();
      ////      if (i != 0 && i != 10 && i != -1)
      ////      {
      ////         sb.Append((char) i);
      ////      }
      ////   }
      ////   while (i != 0 && i != 10 && i != -1);
      ////   if (sb.Length > 0)
      ////      line = sb.ToString();
      ////   Console.WriteLine("S: " + (line ?? string.Empty));
      ////   return line;
      ////}
   }
}