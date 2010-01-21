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

      /// <summary>
      /// Gets the name of the original file.
      /// </summary>
      /// <value>The name of the original file.</value>
      public string OriginalFileName
      {
         get
         {
            return _originalFileName;
         }
      }

      /// <summary>
      /// Gets the new name of the file.
      /// </summary>
      /// <value>The new name of the file.</value>
      public string NewFileName
      {
         get
         {
            return _newFileName;
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
            return ResponseType.CopyFile;
         }
      }

      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      public override int LineCount
      {
         get
         {
            return 2;
         }
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         _originalFileName = Lines[0];
         _newFileName = Lines[1];
         base.Process();
      }
   }
}