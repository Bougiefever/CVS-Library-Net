using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Argument text \n
   /// Response expected: no. Save argument for use in a subsequent command.
   /// Arguments accumulate until an argument-using command is given, at which
   /// point they are forgotten.
   /// </summary>
   public class ArgumentRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentRequest"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      public ArgumentRequest(string arg)
         : base(arg)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ArgumentRequest(IList<string> lines)
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
            return RequestType.Argument;
         }
      }
   }
}