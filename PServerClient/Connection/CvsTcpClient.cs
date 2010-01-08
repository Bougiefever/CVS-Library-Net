using System;
using System.IO;
using System.Net.Sockets;

namespace PServerClient.Connection
{
   public class CvsTcpClient : ICvsTcpClient
   {
      private readonly TcpClient _tcpClient;
      private int _lastByte;
      private NetworkStream _stream;

      public CvsTcpClient()
      {
         _tcpClient = new TcpClient();
         _lastByte = 0;
      }

      #region ICvsTcpClient Members

      public void Connect(string host, int port)
      {
         _tcpClient.Connect(host, port);
         _stream = _tcpClient.GetStream();
         _stream.ReadTimeout = 100;
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
         _stream.Close();
         _tcpClient.Close();
      }

      public int ReadByte()
      {
         _tcpClient.Client.Blocking = true;
         int b = 0;
         try
         {
            b = _stream.ReadByte();
         }
         catch (IOException)
         {
            b = -1;
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }

         if (b == 0 && _lastByte == 10)
            b = -1;
         else
            _lastByte = b;
         return b;
      }

      public byte[] ReadBytes(int length)
      {
         byte[] buffer = new byte[length];
         for (int i = 0; i < length; i++)
         {
            _tcpClient.Client.Blocking = true;
            int b = _stream.ReadByte();
            if (b == -1)
               throw new Exception("Unexpected end of stream");
            buffer[i] = (byte) b;
         }

         return buffer;
      }

      #endregion
   }
}