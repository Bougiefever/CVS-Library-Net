namespace PServerClient.Responses
{
   /// <summary>
   /// Update-existing pathname \n
   /// This is just like Updated and takes the same additional data, but is used only if
   /// a Entry, Modified, or Unchanged request has been sent for the file in question.
   /// This response, or Merged, indicates that the server has determined that it is
   /// OK to overwrite the previous contents of the file specified by pathname. Pro-
   /// vided that the client has correctly sent Modified or Is-modified requests for
   /// a modified file, and the file was not modified while CVS was running, the server
   /// can ensure that a user’s modifications are not lost.
   /// </summary>
   public class UpdateExistingResponse : FileResponseBase
   {
      public override ResponseType Type
      {
         get
         {
            return ResponseType.UpdateExisting;
         }
      }
   }
}