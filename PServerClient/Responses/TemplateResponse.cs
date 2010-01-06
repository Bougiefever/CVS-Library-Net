using System;

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
   public class TemplateResponse : FileResponseBase, IFileResponse
   {

      public override ResponseType Type { get { return ResponseType.Template; } }

   }
}