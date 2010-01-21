using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Entry entry-line \n
   /// Response expected: no. Tell the server what version of a file is on the local
   /// machine. The name in entry-line is a name relative to the directory most
   /// recently specified with Directory. If the user is operating on only some files
   /// in a directory, Entry requests for only those files need be included. If an Entry
   /// request is sent without Modified, Is-modified, or Unchanged, it means the
   /// file is lost (does not exist in the working directory). If both Entry and one of
   /// Modified, Is-modified, or Unchanged are sent for the same file, Entry must be
   /// sent first. For a given file, one can send Modified, Is-modified, or Unchanged,
   /// but not more than one of these three.
   /// </summary>
   public class EntryRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="EntryRequest"/> class.
      /// </summary>
      /// <param name="name">The file name.</param>
      /// <param name="version">The entry version.</param>
      /// <param name="conflict">The conflict string.</param>
      /// <param name="options">The entry options.</param>
      /// <param name="tagOrDate">The tag or date.</param>
      public EntryRequest(string name, string version, string conflict, string options, string tagOrDate)
      {
         string entryLine = string.Format("/{0}/{1}/{2}/{3}/{4}", name, version, conflict, options, tagOrDate);
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, entryLine);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="EntryRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public EntryRequest(IList<string> lines)
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
            return RequestType.Entry;
         }
      }
   }
}