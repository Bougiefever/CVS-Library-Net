using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// E text \n Same as M but send to stderr not stdout.
   /// 
   /// error errno-code ‘ ’ text \n
   //The command completed with an error. errno-code is a symbolic error code
   //(e.g. ENOENT); if the server doesn’t support this feature, or if it’s not appropriate
   //for this particular message, it just omits the errno-code (in that case there are
   //two spaces after ‘error’). Text is an error message such as that provided by
   //strerror(), or any other message the server wants to use. The text is like the M
   //response, in the sense that it is not particularly intended to be machine-parsed;
   //servers may wish to print an error message with MT responses, and then issue
   //a error response without text (although it should be noted that MT currently
   //has no way of flagging the output as intended for standard error, the way that
   //the E response does).
   /// </summary>
   public class ErrorResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         foreach (string s in lines)
         {
            ErrorMessage += s + Environment.NewLine;
         }
      }

      public override string DisplayResponse()
      {
         return ErrorMessage;
      }

      public override ResponseType ResponseType { get { return ResponseType.Error; } }
      public override int LineCount { get { return 0; } }
      public string ErrorMessage { get; set; }
   }
}