namespace PServerClient.Connection
{
   public interface ICvsTcpClient
   {
      void Connect(string host, int port);
      void Write(byte[] buffer);
      byte[] Read();
      bool DataAvailable { get; }
      void Close();
   }
}