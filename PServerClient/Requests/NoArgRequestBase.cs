using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Base class for a request that does not have any additional arguments
   /// </summary>
   public abstract class NoArgRequestBase : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="NoArgRequestBase"/> class.
      /// </summary>
      protected NoArgRequestBase()
      {
         Lines = new string[1];
         Lines[0] = RequestName;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="NoArgRequestBase"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      protected NoArgRequestBase(IList<string> lines)
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