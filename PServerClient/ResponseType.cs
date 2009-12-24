namespace PServerClient
{
   public enum ResponseType
   {
      Auth = 0,
      CheckedIn = 5,
      Checksum = 10,
      ClearStaticDirectory = 15,
      ClearSticky = 17,
      CopyFile = 11,
      Created = 18,
      EMessage = 28,
      Error = 2,
      Flush = 29,
      Mbinary = 27,
      Merged = 8,
      Message = 3,
      MessageTag = 19,
      Mode = 22,
      ModTime = 23,
      ModuleExpansion = 26,
      NewEntry = 6,
      Notified = 25,
      Null = 100,
      Ok = 1,
      Patched = 9,
      RcsDiff = 21,
      Removed = 12,
      RemoveEntry = 13,
      SetStaticDirectory = 14,
      SetSticky = 16,
      Template = 24,
      Updated = 7,
      UpdateExisting = 20,
      ValidRequests = 4,
      WrapperRscOption = 30
   }
}