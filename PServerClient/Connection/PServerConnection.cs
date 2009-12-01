using PServerClient.Requests;
using System.Collections.Generic;
using System;
using System.Text;

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
         Console.WriteLine("Request :" + requestString);
         byte[] sendBuffer = PServerHelper.EncodeString(requestString);
         TcpClient.Write(sendBuffer);
         if (request.ResponseExpected)
         {
            string line = ReadLine();

            //byte[] receiveBuffer = TcpClient.Read();

            //cvsResponse = PServerHelper.DecodeString(receiveBuffer);
            //IList<string> lines = PServerHelper.ReadLines(TcpClient);
            //string cvsResponse = string.Empty;
            //foreach (string l in lines)
            //{
            //   cvsResponse += l + "\n";
            //   Console.WriteLine(l);
            //}
            //request.ProcessResponses(lines);
            //request.RawCvsResponse = cvsResponse;
         }
      }

      public void Close()
      {
         TcpClient.Close();
      }

      public string ReadLine()
      {
         int i = TcpClient.ReadByte();
         StringBuilder sb = new StringBuilder();
         while (i != 10)
            sb.Append((char)i);
         return sb.ToString();
      }

      public void Read()
      {
         byte[] buffer = TcpClient.Read();
         IList<string> lines = new List<string>();
         bool atEnd = false;
         int i = 0;
         StringBuilder sb = new StringBuilder();
         byte last = 0;
         while (!atEnd)
         {
            try
            {
               byte c = buffer[i++];
               if (c == 10)
               {
                  lines.Add(sb.ToString());
                  sb = new StringBuilder();
               }
               if (c != 0 && c != 10)
                  sb.Append((char)c);
               if (last == 10 && c == 0)
                  atEnd = true;
               if (i == buffer.Length)
                  //if (!TcpClient.DataAvailable)
                  //   atEnd = true;
                  //else
                  //{
                     buffer = TcpClient.Read();
                     i = 0;
                  //}
               last = c;
            }
            catch (Exception e)
            {
               Console.WriteLine(e.ToString());
               atEnd = true;
            }
         }
       //  return lines;
      }
   }
}
