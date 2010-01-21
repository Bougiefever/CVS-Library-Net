using System.Collections.Generic;
using PServerClient.CVS;

namespace PServerClient.Requests
{
   /// <summary>
   /// If the client wishes to merely authenticate without starting the cvs
   /// protocol,<br/>
   /// the procedure is the same, except BEGIN AUTH REQUEST is replaced with<br/>
   /// BEGIN VERIFICATION REQUEST, END AUTH REQUEST is replaced with<br/>
   /// END VERIFICATION REQUEST, and upon receipt of I LOVE YOU the con-<br/>
   /// nection is closed rather than continuing.
   /// </summary>
   public class VerifyAuthRequest : AuthRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="VerifyAuthRequest"/> class.
      /// Passes the RequestType to the AuthRequestBase initializer
      /// </summary>
      /// <param name="root">The CVS root.</param>
      public VerifyAuthRequest(IRoot root)
         : base(root, RequestType.VerifyAuth)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="VerifyAuthRequest"/> class.
      /// </summary>
      /// <param name="lines">The request string.</param>
      public VerifyAuthRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets the RequestType 
      /// </summary>
      /// <value>The RequestType.</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.VerifyAuth;
         }
      }
   }
}