using System.Collections.Generic;
using System.Text;
using PServerClient.CVS;

namespace PServerClient.Requests
{
   /// <summary>
   /// Base class for authentication requests
   /// </summary>
   public abstract class AuthRequestBase : RequestBase, IAuthRequest
   {
      private readonly IRoot _root;

      /// <summary>
      /// Initializes a new instance of the <see cref="AuthRequestBase"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="type">The auth request type.</param>
      protected AuthRequestBase(IRoot root, RequestType type)
      {
         _root = root;
         Lines = new string[5];
         string requestName = RequestHelper.RequestNames[(int) type];
         Lines[0] = string.Format("BEGIN {0} REQUEST", requestName);
         Lines[1] = _root.Repository;
         Lines[2] = _root.Username;
         Lines[3] = _root.Password;
         Lines[4] = string.Format("END {0} REQUEST", requestName);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="AuthRequestBase"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      protected AuthRequestBase(IList<string> lines)
      {
         Lines = lines;
      }

      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request.
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      /// <summary>
      /// Gets the full request string with all the parameters that will be sent to CVS
      /// Uses the request type to build the right request string
      /// </summary>
      /// <returns>The CVS request</returns>
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