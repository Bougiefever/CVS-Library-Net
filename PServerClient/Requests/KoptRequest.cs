using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Kopt option \n
   /// This indicates to the server which keyword expansion options to use for the file
   /// specified by the next Modified or Is-modified request (for example ‘-kb’ for
   /// a binary file). This is similar to Entry, but is used for a file for which there is
   /// no entries line. Typically this will be a file being added via an add or import
   /// request. The client may not send both Kopt and Entry for the same file.
   /// </summary>
   public class KoptRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="KoptRequest"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      public KoptRequest(string arg)
         : base(arg)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="KoptRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public KoptRequest(IList<string> lines)
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
            return RequestType.Kopt;
         }
      }
   }
}