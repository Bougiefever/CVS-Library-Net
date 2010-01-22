namespace PServerClient
{
   /// <summary>
   /// Requst type of a request class
   /// </summary>
   public enum RequestType
   {
      /// <summary>
      /// Add request.
      /// </summary>
      Add = 0,

      /// <summary>
      /// Admin request.
      /// </summary>
      Admin = 1,

      /// <summary>
      /// Annotate request.
      /// </summary>
      Annotate = 2,

      /// <summary>
      /// Argument request.
      /// </summary>
      Argument = 3,

      /// <summary>
      /// Argumentx request.
      /// </summary>
      Argumentx = 4,

      /// <summary>
      /// Auth request.
      /// </summary>
      Auth = 5,

      /// <summary>
      /// Case request.
      /// </summary>
      Case = 6,

      /// <summary>
      /// CheckIn request.
      /// </summary>
      CheckIn = 7,

      /// <summary>
      /// CheckinTime request.
      /// </summary>
      CheckinTime = 8,

      /// <summary>
      /// CheckOut request.
      /// </summary>
      CheckOut = 9,

      /// <summary>
      /// Diff request.
      /// </summary>
      Diff = 10,

      /// <summary>
      /// Directory request.
      /// </summary>
      Directory = 11,

      /// <summary>
      /// Editors request.
      /// </summary>
      Editors = 12,

      /// <summary>
      /// EmptyConflicts request.
      /// </summary>
      EmptyConflicts = 13,

      /// <summary>
      /// Entry request.
      /// </summary>
      Entry = 14,

      /// <summary>
      /// ExpandModules request.
      /// </summary>
      ExpandModules = 15,

      /// <summary>
      /// Export request.
      /// </summary>
      Export = 16,

      /// <summary>
      /// GlobalOption request.
      /// </summary>
      GlobalOption = 17,

      /// <summary>
      /// GssapiAuthenticate request.
      /// </summary>
      GssapiAuthenticate = 18,

      /// <summary>
      /// GssapiEncrypt request.
      /// </summary>
      GssapiEncrypt = 19,

      /// <summary>
      /// GzipFileContents request.
      /// </summary>
      GzipFileContents = 20,

      /// <summary>
      /// GzipStream request.
      /// </summary>
      GzipStream = 21,

      /// <summary>
      /// History request.
      /// </summary>
      History = 22,

      /// <summary>
      /// Import request.
      /// </summary>
      Import = 23,

      /// <summary>
      /// Init request.
      /// </summary>
      Init = 24,

      /// <summary>
      /// IsModified request.
      /// </summary>
      IsModified = 25,

      /// <summary>
      /// KerberosEncrypt request.
      /// </summary>
      KerberosEncrypt = 26,

      /// <summary>
      /// Kopt request.
      /// </summary>
      Kopt = 27,

      /// <summary>
      /// Log request.
      /// </summary>
      Log = 28,

      /// <summary>
      /// Lost request.
      /// </summary>
      Lost = 29,

      /// <summary>
      /// MaxDot request.
      /// </summary>
      MaxDot = 30,

      /// <summary>
      /// Modified request.
      /// </summary>
      Modified = 31,

      /// <summary>
      /// Noop request.
      /// </summary>
      Noop = 32,

      /// <summary>
      /// Notify request.
      /// </summary>
      Notify = 33,

      /// <summary>
      /// Questionable request.
      /// </summary>
      Questionable = 34,

      /// <summary>
      /// RAnnotate request.
      /// </summary>
      RAnnotate = 35,

      /// <summary>
      /// RDiff request.
      /// </summary>
      RDiff = 36,

      /// <summary>
      /// Release request.
      /// </summary>
      Release = 37,

      /// <summary>
      /// Remove request.
      /// </summary>
      Remove = 38,

      /// <summary>
      /// Repository request.
      /// </summary>
      Repository = 39,

      /// <summary>
      /// RLog request.
      /// </summary>
      RLog = 40,

      /// <summary>
      /// Root request.
      /// </summary>
      Root = 41,

      /// <summary>
      /// RTag request.
      /// </summary>
      RTag = 42,

      /// <summary>
      /// Set request.
      /// </summary>
      Set = 43,

      /// <summary>
      /// StaticDirectory request.
      /// </summary>
      StaticDirectory = 44,

      /// <summary>
      /// Status request.
      /// </summary>
      Status = 45,

      /// <summary>
      /// Sticky request.
      /// </summary>
      Sticky = 46,

      /// <summary>
      /// Tag request.
      /// </summary>
      Tag = 47,

      /// <summary>
      /// Unchanged request.
      /// </summary>
      Unchanged = 48,

      /// <summary>
      /// UpdatePatches request.
      /// </summary>
      UpdatePatches = 49,

      /// <summary>
      /// Update request.
      /// </summary>
      Update = 50,

      /// <summary>
      /// UseUnchanged request.
      /// </summary>
      UseUnchanged = 51,

      /// <summary>
      /// ValidRequests request.
      /// </summary>
      ValidRequests = 52,

      /// <summary>
      /// ValidResponses request.
      /// </summary>
      ValidResponses = 53,

      /// <summary>
      /// VerifyAuth request.
      /// </summary>
      VerifyAuth = 54,

      /// <summary>
      /// Version request.
      /// </summary>
      Version = 55,

      /// <summary>
      /// WatchAdd request.
      /// </summary>
      WatchAdd = 56,

      /// <summary>
      /// Watchers request.
      /// </summary>
      Watchers = 57,

      /// <summary>
      /// WatchOff request.
      /// </summary>
      WatchOff = 58,

      /// <summary>
      /// WatchOn request.
      /// </summary>
      WatchOn = 59,

      /// <summary>
      /// WatchRemove request.
      /// </summary>
      WatchRemove = 60,

      /// <summary>
      /// WrapperSendmeRcsOptions request.
      /// </summary>
      WrapperSendmeRcsOptions = 61
   }
}