namespace PServerClient
{
   /// <summary>
   /// Requst type of a request class
   /// </summary>
   public enum RequestType
   {
      /// <summary>
      /// Add request. More documentation in class.
      /// </summary>
      Add = 0,

      /// <summary>
      /// Admin request. More documentation in class.
      /// </summary>
      Admin = 1,

      /// <summary>
      /// Annotate request. More documentation in class.
      /// </summary>
      Annotate = 2,

      /// <summary>
      /// Argument request. More documentation in class.
      /// </summary>
      Argument = 3,

      /// <summary>
      /// Argumentx request. More documentation in class.
      /// </summary>
      Argumentx = 4,


      /// <summary>
      /// Auth request. More documentation in class.
      /// </summary>
      Auth = 5,

      /// <summary>
      /// Case request. More documentation in class.
      /// </summary>
      Case = 6,

      /// <summary>
      /// CheckIn request. More documentation in class.
      /// </summary>
      CheckIn = 7,

      /// <summary>
      /// CheckinTime request. More documentation in class.
      /// </summary>
      CheckinTime = 8,

      /// <summary>
      /// CheckOut request. More documentation in class.
      /// </summary>
      CheckOut = 9,

      /// <summary>
      /// Diff request. More documentation in class.
      /// </summary>
      Diff = 10,

      /// <summary>
      /// Directory request. More documentation in class.
      /// </summary>
      Directory = 11,

      /// <summary>
      /// Editors request. More documentation in class.
      /// </summary>
      Editors = 12,

      /// <summary>
      /// EmptyConflicts request. More documentation in class.
      /// </summary>
      EmptyConflicts = 13,

      /// <summary>
      /// Entry request. More documentation in class.
      /// </summary>
      Entry = 14,

      /// <summary>
      /// ExpandModules request. More documentation in class.
      /// </summary>
      ExpandModules = 15,

      /// <summary>
      /// Export request. More documentation in class.
      /// </summary>
      Export = 16,

      /// <summary>
      /// GlobalOption request. More documentation in class.
      /// </summary>
      GlobalOption = 17,

      /// <summary>
      /// GssapiAuthenticate request. More documentation in class.
      /// </summary>
      GssapiAuthenticate = 18,

      /// <summary>
      /// GssapiEncrypt request. More documentation in class.
      /// </summary>
      GssapiEncrypt = 19,

      /// <summary>
      /// GzipFileContents request. More documentation in class.
      /// </summary>
      GzipFileContents = 20,

      /// <summary>
      /// GzipStream request. More documentation in class.
      /// </summary>
      GzipStream = 21,

      /// <summary>
      /// History request. More documentation in class.
      /// </summary>
      History = 22,

      /// <summary>
      /// Import request. More documentation in class.
      /// </summary>
      Import = 23,

      /// <summary>
      /// Init request. More documentation in class.
      /// </summary>
      Init = 24,

      /// <summary>
      /// IsModified request. More documentation in class.
      /// </summary>
      IsModified = 25,

      /// <summary>
      /// KerberosEncrypt request. More documentation in class.
      /// </summary>
      KerberosEncrypt = 26,

      /// <summary>
      /// Kopt request. More documentation in class.
      /// </summary>
      Kopt = 27,

      /// <summary>
      /// Log request. More documentation in class.
      /// </summary>
      Log = 28,

      /// <summary>
      /// Lost request. More documentation in class.
      /// </summary>
      Lost = 29,

      /// <summary>
      /// MaxDot request. More documentation in class.
      /// </summary>
      MaxDot = 30,

      /// <summary>
      /// Modified request. More documentation in class.
      /// </summary>
      Modified = 31,

      /// <summary>
      /// Noop request. More documentation in class.
      /// </summary>
      Noop = 32,

      /// <summary>
      /// Notify request. More documentation in class.
      /// </summary>
      Notify = 33,

      /// <summary>
      /// Questionable request. More documentation in class.
      /// </summary>
      Questionable = 34,

      /// <summary>
      /// RAnnotate request. More documentation in class.
      /// </summary>
      RAnnotate = 35,

      /// <summary>
      /// RDiff request. More documentation in class.
      /// </summary>
      RDiff = 36,

      /// <summary>
      /// Release request. More documentation in class.
      /// </summary>
      Release = 37,

      /// <summary>
      /// Remove request. More documentation in class.
      /// </summary>
      Remove = 38,

      /// <summary>
      /// Repository request. More documentation in class.
      /// </summary>
      Repository = 39,

      /// <summary>
      /// RLog request. More documentation in class.
      /// </summary>
      RLog = 40,

      /// <summary>
      /// Root request. More documentation in class.
      /// </summary>
      Root = 41,

      /// <summary>
      /// RTag request. More documentation in class.
      /// </summary>
      RTag = 42,

      /// <summary>
      /// Set request. More documentation in class.
      /// </summary>
      Set = 43,

      /// <summary>
      /// StaticDirectory request. More documentation in class.
      /// </summary>
      StaticDirectory = 44,

      /// <summary>
      /// Status request. More documentation in class.
      /// </summary>
      Status = 45,

      /// <summary>
      /// Sticky request. More documentation in class.
      /// </summary>
      Sticky = 46,

      /// <summary>
      /// Tag request. More documentation in class.
      /// </summary>
      Tag = 47,

      /// <summary>
      ///  request. More documentation in class.
      /// </summary>
      Unchanged = 48,

      /// <summary>
      /// UpdatePatches request. More documentation in class.
      /// </summary>
      UpdatePatches = 49,

      /// <summary>
      /// Update request. More documentation in class.
      /// </summary>
      Update = 50,

      /// <summary>
      /// UseUnchanged request. More documentation in class.
      /// </summary>
      UseUnchanged = 51,

      /// <summary>
      /// ValidRequests request. More documentation in class.
      /// </summary>
      ValidRequests = 52,

      /// <summary>
      /// ValidResponses request. More documentation in class.
      /// </summary>
      ValidResponses = 53,

      /// <summary>
      /// VerifyAuth request. More documentation in class.
      /// </summary>
      VerifyAuth = 54,

      /// <summary>
      /// Version request. More documentation in class.
      /// </summary>
      Version = 55,

      /// <summary>
      /// WatchAdd request. More documentation in class.
      /// </summary>
      WatchAdd = 56,

      /// <summary>
      /// Watchers request. More documentation in class.
      /// </summary>
      Watchers = 57,

      /// <summary>
      /// WatchOff request. More documentation in class.
      /// </summary>
      WatchOff = 58,

      /// <summary>
      /// WatchOn request. More documentation in class.
      /// </summary>
      WatchOn = 59,

      /// <summary>
      /// WatchRemove request. More documentation in class.
      /// </summary>
      WatchRemove = 60,

      /// <summary>
      /// WrapperSendmeRcsOptions request. More documentation in class.
      /// </summary>
      WrapperSendmeRcsOptions = 61
   }
}