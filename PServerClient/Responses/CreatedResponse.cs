namespace PServerClient.Responses
{
   /// <summary>
   /// Created pathname \n
   //This is just like Updated and takes the same additional data, but is used only if
   //no Entry, Modified, or Unchanged request has been sent for the file in question.
   //The distinction between Created and Update-existing is so that the client
   //can give an error message in several cases: (1) there is a file in the working
   //directory, but not one for which Entry, Modified, or Unchanged was sent (for
   //example, a file which was ignored, or a file for which Questionable was sent),
   //(2) there is a file in the working directory whose name differs from the one
   //mentioned in Created in ways that the client is unable to use to distinguish
   //files. For example, the client is case-insensitive and the names differ only in
   //case.
   /// </summary>
   public class CreatedResponse : FileResponseBase
   {
      public override ResponseType Type { get { return ResponseType.Created; } }
   }
}