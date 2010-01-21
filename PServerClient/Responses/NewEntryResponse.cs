namespace PServerClient.Responses
{
   /// <summary>
   /// New-entry pathname \n
   /// Additional data: New Entries line, \n. Like Checked-in, but the file is not up
   /// to date.
   /// </summary>
   public class NewEntryResponse : ResponseBase
   {
      /// <summary>
      /// Gets the name of the file.
      /// </summary>
      /// <value>The name of the file.</value>
      public string FileName { get; private set; }

      /// <summary>
      /// Gets the revision.
      /// </summary>
      /// <value>The revision.</value>
      public string Revision { get; private set; }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.NewEntry;
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
         FileName = ResponseHelper.GetFileNameFromEntryLine(Lines[1]);
         Revision = ResponseHelper.GetRevisionFromEntryLine(Lines[1]);
         base.Process();
      }
   }
}