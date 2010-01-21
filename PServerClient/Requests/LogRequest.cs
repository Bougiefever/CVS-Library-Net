using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// log \n
   /// Response expected: yes. Show information for past revisions. This uses any
   /// previous Directory, Entry, or Modified requests, if they have been sent. The
   /// last Directory sent specifies the working directory at the time of the operation.
   /// Also uses previous Argument’s of which the canonical forms are the following
   /// (cvs 1.10 and older clients sent what the user specified, but clients are encour-
   /// aged to use the canonical forms and other forms are deprecated):
   /// -b, -h, -l, -N, -R, -t
   /// These options go by themselves, one option per Argument request.
   /// -d date1<date2
   /// Select revisions between date1 and date2. Either date may be omit-
   /// ted in which case there is no date limit at that end of the range
   /// (clients may specify dates such as 1 Jan 1970 or 1 Jan 2038 for
   /// similar purposes but this is problematic as it makes assumptions
   /// about what dates the server supports). Dates are in RFC822/1123
   /// format. The ‘-d’ is one Argument request and the date range is a
   /// second one.
   /// -d date1<=date2
   /// Likewise but compare dates for equality.
   /// -d singledate
   /// Select the single, latest revision dated singledate or earlier.
   /// To include several date ranges and/or singledates, repeat the ‘-d’
   /// option as many times as necessary.
   /// -rrev1:rev2
   /// -rbranch
   /// -rbranch.
   /// -r
   /// Specify revisions (note that rev1 or rev2 can be omitted, or can
   /// refer to branches). Send both the ‘-r’ and the revision information
   /// in a single Argument request. To include several revision selections,
   /// repeat the ‘-r’ option.
   /// -s state
   /// -w
   /// -wlogin
   /// Select on states or users. To include more than one state or user,
   /// repeat the option. Send the ‘-s’ option as a separate argument
   /// from the state being selected. Send the ‘-w’ option as part of the
   /// same argument as the user being selected.
   /// </summary>
   public class LogRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="LogRequest"/> class.
      /// </summary>
      public LogRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="LogRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public LogRequest(IList<string> lines)
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
            return RequestType.Log;
         }
      }
   }
}