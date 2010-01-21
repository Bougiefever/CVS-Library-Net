using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// init root-name \n
   /// Response expected: yes. If it doesn’t already exist, create a cvs repository
   /// root-name. Note that root-name is a local directory and not a fully qualified
   /// CVSROOT variable. The Root request need not have been previously sent.
   /// update \n Response expected: yes. Actually do a cvs update command. This uses any
   /// previous Argument, Directory, Entry, or Modified requests, if they have been
   /// sent. The last Directory sent specifies the working directory at the time of the
   /// operation. The -I option is not used–files which the client can decide whether
   /// to ignore are not mentioned and the client sends the Questionable request for
   /// others.
   /// </summary>
   public class InitRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="InitRequest"/> class.
      /// </summary>
      /// <param name="rootName">Name of the new CVS root.</param>
      public InitRequest(string rootName)
         : base(rootName)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="InitRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public InitRequest(IList<string> lines)
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
            return true;
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
            return RequestType.Init;
         }
      }
   }
}