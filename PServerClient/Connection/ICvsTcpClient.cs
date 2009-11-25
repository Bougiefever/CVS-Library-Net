namespace PServerClient.Connection
{
   public interface ICvsTcpClient
   {
      void Connect(string host, int port);
      void Write(byte[] buffer);
      byte[] Read();
      void Close();
   }
}