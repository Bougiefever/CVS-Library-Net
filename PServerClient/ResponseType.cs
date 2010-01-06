namespace PServerClient
{
   public enum ResponseType
   {
      Auth = 0,
      CheckedIn = 1,
      Checksum = 2,
      ClearStaticDirectory = 3,
      ClearSticky = 4,
      CopyFile = 5,
      Created = 6,
      EMessage = 7,
      Error = 8,
      Flush = 9,
      Mbinary = 10,
      Merged = 11,
      Message = 12,
      MTMessage = 13,
      Mode = 14,
      ModTime = 15,
      ModuleExpansion = 16,
      NewEntry = 17,
      Notified = 18,
      Ok = 19,
      Patched = 20,
      RcsDiff = 21,
      Removed = 22,
      RemoveEntry = 23,
      SetStaticDirectory = 24,
      SetSticky = 25,
      Template = 26,
      Updated = 27,
      UpdateExisting = 28,
      ValidRequests = 29,
      WrapperRscOption = 30,
      Unknown = 31
   }
}