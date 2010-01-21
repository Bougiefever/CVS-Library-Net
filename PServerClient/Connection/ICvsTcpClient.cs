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

      /// <summary>
      /// Reads the bytes from the stream
      /// </summary>
      /// <returns>the byte array from the stream</returns>
      byte[] Read();

      /// <summary>
      /// Reads one byte.
      /// </summary>
      /// <returns>one byte -----</returns>
      int ReadByte();

      /// <summary>
      /// Reads the bytes.
      /// </summary>
      /// <param name="length">The length.</param>
      /// <returns>the byte array</returns>
      byte[] ReadBytes(int length);

      /// <summary>
      /// Closes the Tcp connection
      /// </summary>
      void Close();
   }
}