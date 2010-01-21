using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// add \n
   /// Response expected: yes. Add a file or directory. This uses any previous
   /// Argument, Directory, Entry, or Modified requests, if they have been sent.
   /// The last Directory sent specifies the working directory at the time of the op-
   /// eration.
   /// To add a directory, send the directory to be added using Directory and
   /// Argument requests. For example:
   /// C: Root /u/cvsroot
   /// . . .
   /// C: Argument nsdir
   /// C: Directory nsdir
   /// C: /u/cvsroot/1dir/nsdir
   /// C: Directory .
   /// C: /u/cvsroot/1dir
   /// C: add
   /// S: M Directory /u/cvsroot/1dir/nsdir added to the repository
   /// S: ok
   /// You will notice that the server does not signal to the client in any particular way
   /// that the directory has been successfully added. The client is supposed to just
   /// assume that the directory has been added and update its records accordingly.
   /// Note also that adding a directory is immediate; it does not wait until a ci
   /// request as files do.
   /// To add a file, send the file to be added using a Modified request. For example:
   /// C: Argument nfile
   /// C: Directory .
   /// C: /u/cvsroot/1dir
   /// C: Modified nfile
   /// C: u=rw,g=r,o=r
   /// C: 6
   /// C: hello
   /// C: add
   /// Page 21
   /// S: E cvs server: scheduling file ‘nfile’ for addition
   /// S: Mode u=rw,g=r,o=r
   /// S: Checked-in ./
   /// S: /u/cvsroot/1dir/nfile
   /// S: /nfile/0///
   /// S: E cvs server: use ’cvs commit’ to add this file permanently
   /// S: ok
   /// Note that the file has not been added to the repository; the only effect of a
   /// successful add request, for a file, is to supply the client with a new entries
   /// line containing ‘0’ to indicate an added file. In fact, the client probably could
   /// perform this operation without contacting the server, although using add does
   /// cause the server to perform a few more checks.
   /// The client sends a subsequent ci to actually add the file to the repository.
   /// Another quirk of the add request is that with CVS 1.9 and older, a pathname
   /// specified in an Argument request cannot contain ‘/’. There is no good reason
   /// for this restriction, and in fact more recent CVS servers don’t have it. But
   /// the way to interoperate with the older servers is to ensure that all Directory
   /// requests for add (except those used to add directories, as described above), use
   /// ‘.’ for local-directory. Specifying another string for local-directory may not
   /// get an error, but it will get you strange Checked-in responses from the buggy
   /// servers.
   /// </summary>
   public class AddRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AddRequest"/> class.
      /// </summary>
      public AddRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="AddRequest"/> class.
      /// This constructor is used to recreate a request.
      /// </summary>
      /// <param name="lines">The lines that are sent to CVS.</param>
      public AddRequest(IList<string> lines)
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
            return RequestType.Add;
         }
      }
   }
}