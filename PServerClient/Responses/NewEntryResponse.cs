using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// New-entry pathname \n
   //Additional data: New Entries line, \n. Like Checked-in, but the file is not up
   //to date.
   /// </summary>
   public class NewEntryResponse : ResponseBase
   {
      public string FileName { get; private set; }
      public string Revision { get; private set; }
      public override ResponseType ResponseType { get { return ResponseType.NewEntry; } }
      public override int LineCount { get { return 2; } }

      public override void ProcessResponse(IList<string> lines)
      {
         FileName = ResponseHelper.GetFileNameFromUpdatedLine(lines[1]);
         Revision = ResponseHelper.GetRevisionFromUpdatedLine(lines[1]);
         base.ProcessResponse(lines);
      }
   }
}