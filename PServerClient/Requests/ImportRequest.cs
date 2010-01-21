using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// import \n 
   /// Response expected: yes. Actually do a cvs import command. This uses any
   /// previous Argument, Directory, Entry, or Modified requests, if they have been
   /// sent. The last Directory sent specifies the working directory at the time of
   /// the operation - unlike most commands, the repository field of each Directory
   /// request is ignored (it merely must point somewhere within the root). The files to
   /// be imported are sent in Modified requests (files which the client knows should
   /// be ignored are not sent; the server must still process the CVSROOT/cvsignore
   /// file unless -I ! is sent). A log message must have been specified with a -m
   /// argument.
   /// </summary>
   public class ImportRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ImportRequest"/> class.
      /// </summary>
      public ImportRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ImportRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ImportRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets a value indicating whether [response expected].
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
            return RequestType.Import;
         }
      }
   }
}