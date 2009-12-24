namespace PServerClient.Requests
{
   /// <summary>
   /// gzip-file-contents level \n
   //Response expected: no. Note that this request does not follow the response
   //convention stated above. Gzip-stream is suggested instead of gzip-file-
   //contents as it gives better compression; the only reason to implement the
   //latter is to provide compression with cvs 1.8 and earlier. The gzip-file-
   //contents request asks the server to compress files it sends to the client using
   //gzip (RFC1952/1951) compression, using the specified level of compression. If
   //this request is not made, the server must not compress files.
   //This is only a hint to the server. It may still decide (for example, in the case
   //of very small files, or files that already appear to be compressed) not to do the
   //compression. Compression is indicated by a ‘z’ preceding the file length.
   //Availability of this request in the server indicates to the client that it may
   //compress files sent to the server, regardless of whether the client actually uses
   //this request.
   /// </summary>
   public class GzipFileContentsRequest : OneArgRequestBase
   {
      public GzipFileContentsRequest(string level) : base(level)
      {
      }
      public GzipFileContentsRequest(string[] lines) : base(lines){}

      public override RequestType Type { get { return RequestType.GzipFileContents; } }
   }
}