using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Argumentx text \n
   /// Response expected: no. Append \n followed by text to the current argument
   /// being saved.
   /// </summary>
   public class ArgumentxRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentxRequest"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      public ArgumentxRequest(string arg)
         : base(arg)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentxRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ArgumentxRequest(IList<string> lines)
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
            return RequestType.Argumentx;
         }
      }
   }
}