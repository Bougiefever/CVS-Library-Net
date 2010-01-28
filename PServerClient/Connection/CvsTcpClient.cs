using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace PServerClient.Connection
{
   /// <summary>
   /// TCP client to transmit and receive CVS data
   /// </summary>
   public class CVSTcpClient : ICVSTcpClient
   {
      private readonly TcpClient _tcpClient;
      ////private int _lastByte;
      private NetworkStream _stream;
      private byte[] _buffer; // holds the bytes from the client
      private int _bufferSize = 1024; // size of buffer
      private int _position; // what to start reading from the buffer
      private int _timeout = 100; // milliseconds to timeout from reading the stream

      /// <summary>
      /// Initializes a new instance of the <see cref="CVSTcpClient"/> class.
      /// </summary>
      public CVSTcpClient()
      {
         _tcpClient = new TcpClient();
      }

      /// <summary>
      /// Connects the specified host.
      /// </summary>
      /// <param name="host">The CVS host machine.</param>
      /// <param name="port">The port for CVS.</param>
      public void Connect(string host, int port)
      {
         _tcpClient.Connect(host, port);
         _stream = _tcpClient.GetStream();
         _stream.ReadTimeout = _timeout;
      }

      /// <summary>
      /// Writes the specified buffer to the stream.
      /// </summary>
      /// <param name="buffer">The buffer.</param>
      public void Write(byte[] buffer)
      {
         _stream.Write(buffer, 0, buffer.Length);
         _position = _bufferSize; // set initial position so the next read request starts reading bytes from the stream
      }

      /////// <summary>
      /////// Reads the bytes from the stream
      /////// </summary>
      /////// <returns>the byte array from the stream</returns>
      ////public byte[] Read()
      ////{
      ////   byte[] buffer = new byte[_bufferSize];
      ////   _stream.Read(buffer, 0, _bufferSize);
      ////   return buffer;
      ////}

      /// <summary>
      /// Closes the Tcp connection
      /// </summary>
      public void Close()
      {
         _stream.Close();
         _tcpClient.Close();
      }

      /////// <summary>
      /////// Reads one byte.
      /////// </summary>
      /////// <returns>one byte -----</returns>
      ////public int ReadByte()
      ////{
      ////   _tcpClient.Client.Blocking = true;
      ////   int b = 0;
      ////   try
      ////   {
      ////      b = _stream.ReadByte();
      ////   }
      ////   catch (IOException)
      ////   {
      ////      b = -1;
      ////   }
      ////   catch (Exception e)
      ////   {
      ////      Console.WriteLine(e);
      ////   }

      ////   if (b == 0 && _lastByte == 10)
      ////      b = -1;
      ////   else
      ////      _lastByte = b;
      ////   return b;
      ////}

      /// <summary>
      /// Reads the specified number of bytes from the stream into the buffer
      /// </summary>
      /// <param name="length">The number of bytes to read into the buffer</param>
      /// <returns>the byte array</returns>
      public byte[] ReadBytes(int length)
      {
         byte[] buffer = new byte[length];
         byte b = 0;
         for (int i = 0; i < length; i++)
         {
            if (_position == _bufferSize)
            {
               if (!ReadFromStream())
                  break;
            }

            b = _buffer[_position++];
            buffer[i] = b;
         }

         return buffer;
      }

      /// <summary>
      /// Reads the stream until a line end character is received.
      /// </summary>
      /// <returns>the contents of the buffer as a string</returns>
      public string ReadLine()
      {
         byte b;
         StringBuilder sb = new StringBuilder();
         do
         {
            if (_position == _bufferSize)
               if (!ReadFromStream())
                  break;

            b = _buffer[_position++];
            sb.Append((char) b);
         }
         while (b != 10 && b != 0);

         string line = sb.ToString();
         if (line == "\0")
            line = null;
         return line;
      }

      private bool ReadFromStream()
      {
         bool readSuccess = true;
         try
         {
            _buffer = new byte[_bufferSize];
            _tcpClient.Client.Blocking = true;
            _stream.Read(_buffer, 0, _bufferSize);
         }
         catch (IOException)
         {
            readSuccess = false;
         }
         finally
         {
            _position = 0;
         }
         return readSuccess;
      }
   }
}