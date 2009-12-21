using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// M text \n A one-line message for the user. Note that the format of text is not designed
   //for machine parsing. Although sometimes scripts and clients will have little
   //choice, the exact text which is output is subject to vary at the discretion of
   //the server and the example output given in this document is just that, example
   //output. Servers are encouraged to use the ‘MT’ response, and future versions of
   //this document will hopefully standardize more of the ‘MT’ tags; see Section 5.12
   //[Text tags]
   /// </summary>
   public class MessageResponse : MessageResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Message; } }
   }
}