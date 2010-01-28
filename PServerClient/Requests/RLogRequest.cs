using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// rlog \n
   /// The "cvs r*" commands are cvs sub-commands, that work directly on the CVS repository.
   /// Since the "cvs r*" commands work directly on the CVS repository, you don't need a working directory to use one. 
   /// The target of a "cvs r*" command is always a combination of the CVSROOT and a module name. 
   /// cvs rlog" displays information about all the revisions, of the files in the specified CVS repository module(s). 
   /// "cvs rlog" is similar to the "cvs log" command, but works directly on the repository modules. In other words, you don't need a working directory to use the "cvs rlog" command. The general usage of the "cvs rlog" command is: 
   /// cvs rlog target 
   /// Where "target" is of the form: 
   /// modulename[/filename] 
   /// Where "modulename" is the name a CVS module, relative to CVSROOT and "filename" is optional. 
   /// </summary>
   public class RLogRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RLogRequest"/> class.
      /// </summary>
      public RLogRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="RLogRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public RLogRequest(IList<string> lines)
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
            return RequestType.RLog;
         }
      }
   }
}