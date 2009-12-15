using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Wrapper-rcsOption pattern -k ’option’ \n
   //Transmit to the client a filename pattern which implies a certain keyword ex-
   //pansion mode. The pattern is a wildcard pattern (for example, ‘*.exe’. The
   //option is ‘b’ for binary, and so on. Note that although the syntax happens to
   //resemble the syntax in certain CVS configuration files, it is more constrained;
   //there must be exactly one space between pattern and ‘-k’ and exactly one
   //space between ‘-k’ and ‘’’, and no string is permitted in place of ‘-k’ (exten-
   //sions should be done with new responses, not by extending this one, for graceful
   //handling of Valid-responses).
   /// </summary>
   public class WrapperRscOptionResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.WrapperRscOption; } }
   }
}
