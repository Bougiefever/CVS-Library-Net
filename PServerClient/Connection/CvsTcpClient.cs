using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PServerClient.Connection
{
   public class CvsTcpClient : ICvsTcpClient
   {
      private TcpClient _tcpClient;
      private NetworkStream _stream;
      public CvsTcpClient()
      {
         _tcpClient = new TcpClient();
      }
      public void Connect(string host, int port)
      {
         _tcpClient.Connect(host, port);
         _stream = _tcpClient.GetStream();
      }

      public void Write(byte[] buffer)
      {
         _stream.Write(buffer, 0, buffer.Length);
      }

      public byte[] Read()
      {
         byte[] buffer = new byte[_tcpClient.ReceiveBufferSize];
         _stream.Read(buffer, 0, _tcpClient.ReceiveBufferSize);
         return buffer;
      }

      public void Close()
      {
         _tcpClient.Close();
      }

      public bool DataAvailable { get { return _stream.DataAvailable; } }
   }

}
