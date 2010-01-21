using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// This is an obsolete CVS request
   /// </summary>
   public class LostRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="LostRequest"/> class.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      public LostRequest(string fileName)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, fileName);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="LostRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public LostRequest(IList<string> lines)
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
      /// Gets the type.
      /// </summary>
      /// <value>The request type.</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.Lost;
         }
      }
   }
}