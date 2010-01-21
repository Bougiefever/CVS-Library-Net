using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Set variable=value \n
   /// Response expected: no. Set a user variable variable to value. The Root request
   /// need not have been previously sent.
   /// </summary>
   public class SetRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="SetRequest"/> class.
      /// </summary>
      /// <param name="variableName">Name of the variable.</param>
      /// <param name="value">The value.</param>
      public SetRequest(string variableName, string value)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}={2}", RequestName, variableName, value);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="SetRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public SetRequest(IList<string> lines)
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
            return RequestType.Set;
         }
      }
   }
}