using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Template pathname \n
   //Additional data: file transmission (note: compressed file transmissions are not
   //supported). pathname ends in a slash; its purpose is to specify a directory,
   //not a file within a directory. Tell the client to store the file transmission as the
   //template log message, and then use that template in the future when prompting
   //the user for a log message.
   /// </summary>
   public class TemplateResponse : ResponseBase, IFileResponse
   {
      public long FileLength { get; set; }

      #region IFileResponse Members

      public override ResponseType ResponseType { get { return ResponseType.Template; } }
      public ReceiveFile File { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException(); ;
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      #endregion
   }
}