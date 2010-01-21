using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Max-dotdot level \n
   /// Response expected: no. Tell the server that level levels of directories above the
   /// directory which Directory requests are relative to will be needed. For example,
   /// if the client is planning to use a Directory request for ‘../../foo’, it must
   /// send a Max-dotdot request with a level of at least 2. Max-dotdot must be sent
   /// before the first Directory request.
   /// </summary>
   public class MaxDotRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="MaxDotRequest"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      public MaxDotRequest(string arg)
         : base(arg)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="MaxDotRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public MaxDotRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.MaxDot;
         }
      }
   }
}