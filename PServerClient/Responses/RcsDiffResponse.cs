namespace PServerClient.Responses
{
   /// <summary>
   /// Rcs-diff pathname \n
   //This is just like Updated and takes the same additional data, with the one
   //difference that instead of sending a new copy of the file, the server sends an
   //RCS change text. This change text is produced by ‘diff -n’ (the GNU diff
   //‘-a’ option may also be used). The client must apply this change text to the
   //existing file. This will only be used when the client has an exact copy of an
   //earlier revision of a file. This response is only used if the update command is
   //given the ‘-u’ argument.
   /// </summary>
   public class RcsDiffResponse : FileResponseBase
   {
      public override ResponseType Type { get { return ResponseType.RcsDiff; } }
   }
}