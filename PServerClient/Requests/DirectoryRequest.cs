using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Directory local-directory \n
   /// Additional data: repository \n. Response expected: no. Tell the server what
   /// directory to use. The repository should be a directory name from a previous
   /// server response. Note that this both gives a default for Entry and Modified
   /// and also for ci and the other commands; normal usage is to send Directory
   /// for each directory in which there will be an Entry or Modified, and then a final
   /// Directory for the original directory, then the command. The local-directory
   /// is relative to the top level at which the command is occurring (i.e. the last
   /// Directory which is sent before the command); to indicate that top level, ‘.’
   /// should be sent for local-directory.
   /// </summary>
   public class DirectoryRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DirectoryRequest"/> class.
      /// </summary>
      /// <param name="name">The repository name.</param>
      /// <param name="directory">The directory to the current cvs module.</param>
      public DirectoryRequest(string name, string directory)
      {
         Lines = new string[2];
         Lines[0] = string.Format("{0} {1}", RequestName, name);
         Lines[1] = directory;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="DirectoryRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public DirectoryRequest(IList<string> lines)
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
            return RequestType.Directory;
         }
      }
   }
}