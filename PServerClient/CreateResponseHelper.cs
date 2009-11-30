using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient
{
   public static class CreateResponseHelper
   {
      public static readonly string[] ResponsePatterns;
      private const string AuthRegex = "(I LOVE YOU|I HATE YOU)";
      private const string OkRegex = @"^ok\s?\n";
      private const string ErrorRegex = @"^error";
      private const string MessageRegex = @"^M\s";
      private const string ValidRequestsRegex = @"^Valid-requests\s";


      static CreateResponseHelper()
      {
         ResponsePatterns = new string[5];
         ResponsePatterns[0] = AuthRegex;
         ResponsePatterns[1] = OkRegex;
         ResponsePatterns[2] = ErrorRegex;
         ResponsePatterns[3] = MessageRegex;
         ResponsePatterns[4] = ValidRequestsRegex;
      }

   }
}
