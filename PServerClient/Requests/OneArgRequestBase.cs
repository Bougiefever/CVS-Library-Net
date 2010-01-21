using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Base class for a request that has only one argument
   /// </summary>
   public abstract class OneArgRequestBase : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="OneArgRequestBase"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      protected OneArgRequestBase(string arg)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, arg);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="OneArgRequestBase"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      protected OneArgRequestBase(IList<string> lines)
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
   }
}