using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// expand-modules \n
   /// Response expected: yes. Expand the modules which are specified in the argu-
   /// ments. Returns the data in Module-expansion responses. Note that the server
   /// can assume that this is checkout or export, not rtag or rdiff; the latter do not
   /// access the working directory and thus have no need to expand modules on the
   /// client side.
   /// Expand may not be the best word for what this request does. It does not
   /// necessarily tell you all the files contained in a module, for example. Basically
   /// it is a way of telling you which working directories the server needs to know
   /// about in order to handle a checkout of the specified modules.
   /// For example, suppose that the server has a module defined by
   /// aliasmodule -a 1dir
   /// That is, one can check out aliasmodule and it will take 1dir in the repository
   /// and check it out to 1dir in the working directory. Now suppose the client
   /// already has this module checked out and is planning on using the co request
   /// to update it. Without using expand-modules, the client would have two bad
   /// choices: it could either send information about all working directories under
   /// the current directory, which could be unnecessarily slow, or it could be ignorant
   /// of the fact that aliasmodule stands for 1dir, and neglect to send information
   /// for 1dir, which would lead to incorrect operation.
   /// With expand-modules, the client would first ask for the module to be expanded:
   /// C: Root /home/kingdon/zwork/cvsroot
   /// . . .
   /// C: Argument aliasmodule
   /// C: Directory .
   /// C: /home/kingdon/zwork/cvsroot
   /// C: expand-modules
   /// S: Module-expansion 1dir
   /// S: ok
   /// and then it knows to check the ‘1dir’ directory and send requests such as Entry
   /// and Modified for the files in that directory.
   /// </summary>
   public class ExpandModulesRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ExpandModulesRequest"/> class.
      /// </summary>
      public ExpandModulesRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ExpandModulesRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ExpandModulesRequest(IList<string> lines)
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
            return RequestType.ExpandModules;
         }
      }
   }
}