namespace PServerClient.Responses
{
   /// <summary>
   /// Updated pathname \n
   //Additional data: New Entries line, \n, mode, \n, file transmission. A new copy
   //of the file is enclosed. This is used for a new revision of an existing file, or
   //for a new file, or for any other case in which the local (client-side) copy of the
   //file needs to be updated, and after being updated it will be up to date. If any
   //directory in pathname does not exist, create it. This response is not used if
   //Created and Update-existing are supported.
   /// </summary>
   public class UpdatedResponse : FileResponseBase
   {
      public override ResponseType Type { get { return ResponseType.Updated; } }
   }
}