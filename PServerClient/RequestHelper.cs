using System;
using System.Collections.Generic;
using System.Linq;

namespace PServerClient
{
   /// <summary>
   /// Helper methods for requests
   /// </summary>
   public static class RequestHelper
   {
      /// <summary>
      /// Array of request string to send to CVS as part of request
      /// </summary>
      public static readonly string[] RequestNames;

      private const string AddRequest = "add";
      private const string AdminRequest = "admin";
      private const string AnnotateRequest = "annotate";
      private const string ArgumentRequest = "Argument";
      private const string ArgumentxRequest = "Argumentx";
      private const string AuthRequest = "AUTH";
      private const string CaseRequest = "Case";
      private const string CheckInRequest = "ci";
      private const string CheckinTimeRequest = "Checkin-time";
      private const string CheckOutRequest = "co";
      private const string DiffRequest = "diff";
      private const string DirectoryRequest = "Directory";
      private const string EditorsRequest = "editors";
      private const string EmptyConflictsRequest = "Empty-conflicts";
      private const string EntryRequest = "Entry";
      private const string ExpandModulesRequest = "expand-modules";
      private const string ExportRequest = "export";
      private const string GlobalOptionRequest = "Global_option";
      private const string GssapiAuthenticateRequest = "Gssapi-authenticate";
      private const string GssapiEncryptRequest = "Gssapi-encrypt";
      private const string GzipFileContentsRequest = "gzip-file-contents";
      private const string GzipStreamRequest = "Gzip-stream";
      private const string HistoryRequest = "history";
      private const string ImportRequest = "import";
      private const string InitRequest = "init";
      private const string IsModifiedRequest = "Is-modified";
      private const string KerberosEncryptRequest = "Kerberos-encrypt";
      private const string KoptRequest = "Kopt";
      private const string LogRequest = "log";
      private const string LostRequest = "Lost";
      private const string MaxDotRequest = "Max-dotdot";
      private const string ModifiedRequest = "Modified";
      private const string NoopRequest = "noop";
      private const string NotifyRequest = "Notify";
      private const string QuestionableRequest = "Questionable";
      private const string RAnnotateRequest = "rannotate";
      private const string RDiffRequest = "rdiff";
      private const string ReleaseRequest = "release";
      private const string RemoveRequest = "remove";
      private const string RepositoryRequest = "Repository";
      private const string RLogRequest = "rlog";
      private const string RootRequest = "Root";
      private const string RTagRequest = "rtag";
      private const string SetRequest = "Set";
      private const string StaticDirectoryRequest = "Static-directory";
      private const string StatusRequest = "status";
      private const string StickyRequest = "Sticky";
      private const string TagRequest = "tag";
      private const string UnchangedRequest = "Unchanged";
      private const string UpdatePatchesRequest = "update-patches";
      private const string UpdateRequest = "update";
      private const string UseUnchangedRequest = "UseUnchanged";
      private const string ValidRequestsRequest = "valid-requests";
      private const string ValidResponsesRequest = "Valid-responses";
      private const string VerifyAuthRequest = "VERIFICATION";
      private const string VersionRequest = "version";
      private const string WatchAddRequest = "watch-add";
      private const string WatchersRequest = "watchers";
      private const string WatchOffRequest = "watch-off";
      private const string WatchOnRequest = "watch-on";
      private const string WatchRemoveRequest = "watch-remove";
      private const string WrapperSendmercsOptionsRequest = "wrapper-sendme-rcsOptions";

      /// <summary>
      /// Initializes static members of the <see cref="RequestHelper"/> class.
      /// </summary>
      static RequestHelper()
      {
         RequestNames = new[]
                           {
                              AddRequest,
                              AdminRequest,
                              AnnotateRequest,
                              ArgumentRequest,
                              ArgumentxRequest,
                              AuthRequest,
                              CaseRequest,
                              CheckInRequest,
                              CheckinTimeRequest,
                              CheckOutRequest,
                              DiffRequest,
                              DirectoryRequest,
                              EditorsRequest,
                              EmptyConflictsRequest,
                              EntryRequest,
                              ExpandModulesRequest,
                              ExportRequest,
                              GlobalOptionRequest,
                              GssapiAuthenticateRequest,
                              GssapiEncryptRequest,
                              GzipFileContentsRequest,
                              GzipStreamRequest,
                              HistoryRequest,
                              ImportRequest,
                              InitRequest,
                              IsModifiedRequest,
                              KerberosEncryptRequest,
                              KoptRequest,
                              LogRequest,
                              LostRequest,
                              MaxDotRequest,
                              ModifiedRequest,
                              NoopRequest,
                              NotifyRequest,
                              QuestionableRequest,
                              RAnnotateRequest,
                              RDiffRequest,
                              ReleaseRequest,
                              RemoveRequest,
                              RepositoryRequest,
                              RLogRequest,
                              RootRequest,
                              RTagRequest,
                              SetRequest,
                              StaticDirectoryRequest,
                              StatusRequest,
                              StickyRequest,
                              TagRequest,
                              UnchangedRequest,
                              UpdatePatchesRequest,
                              UpdateRequest,
                              UseUnchangedRequest,
                              ValidRequestsRequest,
                              ValidResponsesRequest,
                              VerifyAuthRequest,
                              VersionRequest,
                              WatchAddRequest,
                              WatchersRequest,
                              WatchOffRequest,
                              WatchOnRequest,
                              WatchRemoveRequest,
                              WrapperSendmercsOptionsRequest
                           };
      }

      /// <summary>
      /// Gets the array valid responses.
      /// </summary>
      /// <value>The valid responses.</value>
      public static ResponseType[] ValidResponses
      {
         get
         {
            return new[]
                      {
                         ResponseType.Ok,
                         ResponseType.Error,
                         ResponseType.ValidRequests,
                         ResponseType.CheckedIn,
                         ResponseType.NewEntry,
                         ResponseType.Updated,
                         ResponseType.Created,
                         ResponseType.Merged,
                         ResponseType.ModTime,
                         ResponseType.Removed,
                         ResponseType.SetStaticDirectory,
                         ResponseType.ClearStaticDirectory,
                         ResponseType.SetSticky,
                         ResponseType.ClearSticky,
                         ResponseType.ModuleExpansion,
                         ResponseType.Message,
                         ResponseType.EMessage,
                         ResponseType.MTMessage
                      };
         }
      }

      /// <summary>
      /// Converts a string of requests to a list of RequestTypes
      /// </summary>
      /// <param name="requestList">The request list.</param>
      /// <returns>the list of RequestTypes</returns>
      public static IList<RequestType> RequestsToRequestTypes(string requestList)
      {
         string[] sep = new[] { " " };
         string[] requestNames = requestList.Split(sep, StringSplitOptions.RemoveEmptyEntries);
         IList<RequestType> types = new List<RequestType>();
         foreach (string s in requestNames)
         {
            var type = RequestNames.Select((r, i) => new { Name = r, Type = (RequestType) i })
               .Where(rr => rr.Name == s).Select(tt => tt.Type).First();
            types.Add(type);
         }

         return types;
      }
   }
}