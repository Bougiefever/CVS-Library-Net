using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Gzip-stream level \n
   /// Response expected: no. Use zlib (RFC 1950/1951) compression to compress all
   /// further communication between the client and the server. After this request is
   /// sent, all further communication must be compressed. All further data received
   /// from the server will also be compressed. The level argument suggests to the
   /// server the level of compression that it should apply; it should be an integer
   /// between 1 and 9, inclusive, where a higher number indicates more compression.
   /// </summary>
   public class GzipStreamRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="GzipStreamRequest"/> class.
      /// </summary>
      /// <param name="level">The level.</param>
      public GzipStreamRequest(string level)
         : base(level)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="GzipStreamRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public GzipStreamRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.GzipStream;
         }
      }
   }
}