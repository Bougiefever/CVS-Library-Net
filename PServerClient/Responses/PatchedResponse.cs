using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Patched pathname \n
   //This is just like Rcs-diff and takes the same additional data, except that it
   //sends a standard patch rather than an RCS change text. The patch is produced
   //by ‘diff -c’ for cvs 1.6 and later (see POSIX.2 for a description of this format),
   //or ‘diff -u’ for previous versions of cvs; clients are encouraged to accept either
   //format. Like Rcs-diff, this response is only used if the update command is
   //given the ‘-u’ argument.
   //The Patched response is deprecated in favor of the Rcs-diff response. How-
   //ever, older clients (CVS 1.9 and earlier) only support Patched.
   /// </summary>
   public class PatchedResponse : FileResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Patched; } }
   }
}