using System;
using System.Collections.Generic;
using System.Text;

namespace PServerClient.Requests
{
   /// <summary>
   /// Global_option option \n
   /// Response expected: no. Transmit one of the global options ‘-q’, ‘-Q’, ‘-l’,
   /// ‘-t’, ‘-r’, or ‘-n’. option must be one of those strings, no variations (such as
   /// combining of options) are allowed. For graceful handling of valid-requests,
   /// it is probably better to make new global options separate requests, rather than
   /// trying to add them to this request. The Root request need not have been
   /// previously sent.
   /// </summary>
   public class GlobalOptionRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="GlobalOptionRequest"/> class.
      /// </summary>
      /// <param name="option">The global option type.</param>
      public GlobalOptionRequest(GlobalOption option)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, option);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="GlobalOptionRequest"/> class.
      /// </summary>
      /// <param name="option">The global option type.</param>
      /// <param name="arg">The additional argument.</param>
      public GlobalOptionRequest(GlobalOption option, string arg)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1} {2}", RequestName, option, arg);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="GlobalOptionRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public GlobalOptionRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request.
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      public override bool ResponseExpected
      {
         get
         {
            return false;
         }
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.GlobalOption;
         }
      }

      /// <summary>
      /// Gets the full request string with all the parameters that will be sent to CVS
      /// </summary>
      /// <returns>The CVS request string</returns>
      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < Lines.Count; i++)
         {
            sb.Append(Lines[i]).Append(PServerHelper.UnixLineEnd);
         }

         string request = sb.ToString();
         return request;
      }
   }
}