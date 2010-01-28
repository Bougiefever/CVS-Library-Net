namespace PServerClient.Connection
{
   /// <summary>
   /// Interface for Tcp Client for CVS commands
   /// </summary>
   public interface ICVSTcpClient
   {
      /// <summary>
      /// Connects the specified host.
      /// </summary>
      /// <param name="host">The CVS host machine.</param>
      /// <param name="port">The port for CVS.</param>
      void Connect(string host, int port);

      /// <summary>
      /// Writes the specified buffer to the stream.
      /// </summary>
      /// <param name="buffer">The buffer.</param>
      void Write(byte[] buffer);

      /////// <summary>
      /////// Reads the bytes from the stream
      /////// </summary>
      /////// <returns>the byte array from the stream</returns>
      ////byte[] Read();

      /////// <summary>
      /////// Reads one byte.
      /////// </summary>
      /////// <returns>one byte -----</returns>
      ////int ReadByte();

      /// <summary>
      /// Reads the specified number of bytes from the stream into the buffer
      /// </summary>
      /// <param name="length">The number of bytes to read into the buffer</param>
      /// <returns>the byte array</returns>
      byte[] ReadBytes(int length);

      /// <summary>
      /// Closes the Tcp connection
      /// </summary>
      void Close();

      /// <summary>
      /// Reads the stream until a line end character is received.
      /// </summary>
      /// <returns>the contents of the buffer as a string</returns>
      string ReadLine();
   }
}