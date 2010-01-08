using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Connection
{
   public class PServerConnection : IConnection
   {
      private ICvsTcpClient _cvsTcpClient;

      #region IConnection Members

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

      public void Connect(IRoot root)
      {
         TcpClient.Connect(root.Host, root.Port);
      }

      public void DoRequest(IRequest request)
      {
         string requestString = request.GetRequestString();
         Console.WriteLine("C: " + requestString);
         byte[] sendBuffer = requestString.Encode();
         TcpClient.Write(sendBuffer);

         // do file stuff for request here
      }

      public void Close()
      {
         TcpClient.Close();
      }

      #endregion

      public IResponse GetResponse()
      {
         string line;
         IResponse response = null;
         line = ReadLine();
         if (line != null)
         {
            PServerFactory factory = new PServerFactory();
            ResponseType responseType = factory.GetResponseType(line);
            response = factory.CreateResponse(responseType);
            IList<string> responseLines = GetResponseLines(line, responseType, response.LineCount);
            response.Initialize(responseLines);
            if (response is IFileResponse)
            {
               IFileResponse fileResponse = (IFileResponse) response;
               fileResponse.Contents = TcpClient.ReadBytes((int) fileResponse.Length);
            }
         }

         return response;
      }

      public IList<IResponse> GetAllResponses()
      {
         IList<IResponse> responses = new List<IResponse>();
         string line;
         do
         {
            line = ReadLine();
            if (line != null)
            {
               PServerFactory factory = new PServerFactory();
               ResponseType responseType = factory.GetResponseType(line);
               IResponse response = factory.CreateResponse(responseType);
               IList<string> responseLines = GetResponseLines(line, responseType, response.LineCount);
               response.Initialize(responseLines);
               if (response is IFileResponse)
               {
                  IFileResponse fileResponse = (IFileResponse) response;
                  fileResponse.Contents = TcpClient.ReadBytes((int) fileResponse.Length);
               }

               responses.Add(response);
            }
         }
         while (line != null);
         return responses;
      }

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
               responseLine = ReadLine();
               responseLines.Add(responseLine);
            }
         }

         return responseLines;
      }

      internal string ReadLine()
      {
         int i;
         string line = null;
         StringBuilder sb = new StringBuilder();
         do
         {
            i = TcpClient.ReadByte();
            if (i != 0 && i != 10 && i != -1)
            {
               sb.Append((char) i);
            }
         }
         while (i != 0 && i != 10 && i != -1);
         if (sb.Length > 0)
            line = sb.ToString();
         Console.WriteLine("S: " + (line ?? string.Empty));
         return line;
      }
   }
}