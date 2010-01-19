namespace PServerClient
{
    /// <summary>
    /// Response types for each kind of response that the CVS server will return
    /// </summary>
   public enum ResponseType
   {
       /// <summary>
       /// Auth response. Full documentation is in response class
       /// </summary>
      Auth = 0,

      /// <summary>
      /// Checked in response. Full documentation is in response class
      /// </summary>
      CheckedIn = 1,

      /// <summary>
      /// Checksum response. Full documentation is in response class
      /// </summary>
      Checksum = 2,

      /// <summary>
      /// ClearStaticDirectory response. Full documentation is in response class
      /// </summary>
      ClearStaticDirectory = 3,

      /// <summary>
      /// ClearSticky response. Full documentation is in response class
      /// </summary>
      ClearSticky = 4,

      /// <summary>
      /// CopyFile response. Full documentation is in response class
      /// </summary>
      CopyFile = 5,

      /// <summary>
      /// Created response. Full documentation is in response class
      /// </summary>
      Created = 6,

      /// <summary>
      /// E Message response. Full documentation is in response class
      /// </summary>
      EMessage = 7,

      /// <summary>
      /// Error response. Full documentation is in response class
      /// </summary>
      Error = 8,

      /// <summary>
      /// Flush response. Full documentation is in response class
      /// </summary>
      Flush = 9,

      /// <summary>
      /// Mbinary response. Full documentation is in response class
      /// </summary>
      Mbinary = 10,

      /// <summary>
      /// Merged response. Full documentation is in response class
      /// </summary>
      Merged = 11,

      /// <summary>
      /// M Message response. Full documentation is in response class
      /// </summary>
      Message = 12,

      /// <summary>
      /// MT Message response. Full documentation is in response class
      /// </summary>
      MTMessage = 13,

      /// <summary>
      /// Mode response. Full documentation is in response class
      /// </summary>
      Mode = 14,

      /// <summary>
      /// ModTime response. Full documentation is in response class
      /// </summary>
      ModTime = 15,

      /// <summary>
      /// ModuleExpansion response. Full documentation is in response class
      /// </summary>
      ModuleExpansion = 16,

      /// <summary>
      /// NewEntry response. Full documentation is in response class
      /// </summary>
      NewEntry = 17,

      /// <summary>
      /// Notified response. Full documentation is in response class
      /// </summary>
      Notified = 18,

      /// <summary>
      /// ok response. Full documentation is in response class
      /// </summary>
      Ok = 19,

      /// <summary>
      /// Patched response. Full documentation is in response class
      /// </summary>
      Patched = 20,

      /// <summary>
      /// RcsDiff response. Full documentation is in response class
      /// </summary>
      RcsDiff = 21,

      /// <summary>
      /// Removed response. Full documentation is in response class
      /// </summary>
      Removed = 22,

      /// <summary>
      /// RemoveEntry response. Full documentation is in response class
      /// </summary>
      RemoveEntry = 23,

      /// <summary>
      /// SetStaticDirectory response. Full documentation is in response class
      /// </summary>
      SetStaticDirectory = 24,

      /// <summary>
      /// SetSticky response. Full documentation is in response class
      /// </summary>
      SetSticky = 25,

      /// <summary>
      /// Template response. Full documentation is in response class
      /// </summary>
      Template = 26,

      /// <summary>
      /// Updated response. Full documentation is in response class
      /// </summary>
      Updated = 27,

      /// <summary>
      /// UpdateExisting response. Full documentation is in response class
      /// </summary>
      UpdateExisting = 28,

      /// <summary>
      /// ValidRequests response. Full documentation is in response class
      /// </summary>
      ValidRequests = 29,

      /// <summary>
      /// WrapperRscOption response. Full documentation is in response class
      /// </summary>
      WrapperRscOption = 30,

      /// <summary>
      /// Unknown response. There is no class to accept and process this response in the library.
      /// </summary>
      Unknown = 31
   }
}