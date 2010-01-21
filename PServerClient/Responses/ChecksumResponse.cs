namespace PServerClient.Responses
{
   /// <summary>
   /// Checksum checksum\n
   /// The checksum applies to the next file sent (that is, Checksum is a file update
   /// modifying response as described in Section 5.9 [Response intro], page 22). In
   /// the case of Patched, the checksum applies to the file after being patched, not to
   /// the patch itself. The client should compute the checksum itself, after receiving
   /// the file or patch, and signal an error if the checksums do not match. The
   /// checksum is the 128 bit MD5 checksum represented as 32 hex digits (MD5 is
   /// described in RFC1321). This response is optional, and is only used if the client
   /// supports it (as judged by the Valid-responses request).
   /// </summary>
   public class ChecksumResponse : ResponseBase
   {
      private string _checkSum;

      /// <summary>
      /// Gets the check sum.
      /// </summary>
      /// <value>The check sum.</value>
      public string CheckSum
      {
         get
         {
            return _checkSum;
         }
      }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.Checksum;
         }
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         _checkSum = Lines[0];
         base.Process();
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return _checkSum;
      }
   }
}