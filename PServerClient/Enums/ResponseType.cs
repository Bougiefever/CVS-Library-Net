namespace PServerClient
{
    /// <summary>
    /// Response types for each kind of response that the CVS server will return
    /// </summary>
   public enum ResponseType
   {
       /// <summary>
       /// Auth response.
       /// </summary>
      Auth = 0,

      /// <summary>
      /// Checked in response.
      /// </summary>
      CheckedIn = 1,

      /// <summary>
      /// Checksum response.
      /// </summary>
      Checksum = 2,

      /// <summary>
      /// ClearStaticDirectory response.
      /// </summary>
      ClearStaticDirectory = 3,

      /// <summary>
      /// ClearSticky response.
      /// </summary>
      ClearSticky = 4,

      /// <summary>
      /// CopyFile response.
      /// </summary>
      CopyFile = 5,

      /// <summary>
      /// Created response.
      /// </summary>
      Created = 6,

      /// <summary>
      /// E Message response.
      /// </summary>
      EMessage = 7,

      /// <summary>
      /// Error response.
      /// </summary>
      Error = 8,

      /// <summary>
      /// Flush response.
      /// </summary>
      Flush = 9,

      /// <summary>
      /// Mbinary response.
      /// </summary>
      Mbinary = 10,

      /// <summary>
      /// Merged response.
      /// </summary>
      Merged = 11,

      /// <summary>
      /// M Message response.
      /// </summary>
      Message = 12,

      /// <summary>
      /// MT Message response.
      /// </summary>
      MTMessage = 13,

      /// <summary>
      /// Mode response.
      /// </summary>
      Mode = 14,

      /// <summary>
      /// ModTime response.
      /// </summary>
      ModTime = 15,

      /// <summary>
      /// ModuleExpansion response.
      /// </summary>
      ModuleExpansion = 16,

      /// <summary>
      /// NewEntry response.
      /// </summary>
      NewEntry = 17,

      /// <summary>
      /// Notified response.
      /// </summary>
      Notified = 18,

      /// <summary>
      /// ok response.
      /// </summary>
      Ok = 19,

      /// <summary>
      /// Patched response.
      /// </summary>
      Patched = 20,

      /// <summary>
      /// RcsDiff response.
      /// </summary>
      RcsDiff = 21,

      /// <summary>
      /// Removed response.
      /// </summary>
      Removed = 22,

      /// <summary>
      /// RemoveEntry response.
      /// </summary>
      RemoveEntry = 23,

      /// <summary>
      /// SetStaticDirectory response.
      /// </summary>
      SetStaticDirectory = 24,

      /// <summary>
      /// SetSticky response.
      /// </summary>
      SetSticky = 25,

      /// <summary>
      /// Template response.
      /// </summary>
      Template = 26,

      /// <summary>
      /// Updated response.
      /// </summary>
      Updated = 27,

      /// <summary>
      /// UpdateExisting response.
      /// </summary>
      UpdateExisting = 28,

      /// <summary>
      /// ValidRequests response.
      /// </summary>
      ValidRequests = 29,

      /// <summary>
      /// WrapperRscOption response.
      /// </summary>
      WrapperRscOption = 30,

      /// <summary>
      /// Unknown response. There is no class to accept and process this response in the library.
      /// </summary>
      Unknown = 31
   }
}