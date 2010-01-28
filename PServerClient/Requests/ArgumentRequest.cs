using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Argument text \n
   /// Response expected: no. Save argument for use in a subsequent command.
   /// Arguments accumulate until an argument-using command is given, at which
   /// point they are forgotten.
   /// </summary>
   public class ArgumentRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentRequest"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      public ArgumentRequest(string arg)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, arg);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentRequest"/> class.
      /// </summary>
      /// <param name="option">The option type.</param>
      public ArgumentRequest(CommandOption option)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, option);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ArgumentRequest"/> class.
      /// </summary>
      /// <param name="option">The option type.</param>
      /// <param name="arg">The additional arg string.</param>
      public ArgumentRequest(CommandOption option, string arg)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1} {2}", RequestName, option, arg);
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
            return RequestType.Argument;
         }
      }
   }
}