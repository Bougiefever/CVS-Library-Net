namespace PServerClient.Responses
{
   /// <summary>
   /// Copy-file pathname \n
   /// Additional data: newname \n. Copy file pathname to newname in the same
   /// directory where it already is. This does not affect CVS/Entries.
   /// This can optionally be implemented as a rename instead of a copy. The only
   /// use for it which currently has been identified is prior to a Merged response as
   /// described under Merged. Clients can probably assume that is how it is being
   /// used, if they want to worry about things like how long to keep the newname
   /// file around.
   /// </summary>
   public class CopyFileResponse : ResponseBase
   {
      private string _newFileName;
      private string _originalFileName;

      public string OriginalFileName
      {
         get
         {
            return _originalFileName;
         }
      }

      public string NewFileName
      {
         get
         {
            return _newFileName;
         }
      }

      public override ResponseType Type
      {
         get
         {
            return ResponseType.CopyFile;
         }
      }

      public override int LineCount
      {
         get
         {
            return 2;
         }
      }

      public override void Process()
      {
         _originalFileName = Lines[0];
         _newFileName = Lines[1];
         base.Process();
      }
   }
}