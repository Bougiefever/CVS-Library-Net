namespace PServerClient.Responses
{
   public enum ResponseType
   {
      Unknown = 100,
      Auth = 0,
      Ok = 1,
      Error = 2,
      Message = 3,
      ValidRequests = 4,
      CheckedIn = 5,
      NewEntry = 6,
      Updated = 7,
      Merged = 8,
      Patched = 9,
      CheckSum = 10,
      CopyFile = 11,
      Removed = 12,
      RemoveEntry = 13,
      SetStaticDirectory = 14,
      ClearStaticDirectory = 15,
      SetSticky = 16,
      ClearSticky = 17,
      Created = 18,
      MessageTag = 19,
      UpdateExisting = 20,
      RcsDiff = 21,
      Mode = 22,
      ModTime = 23,
      Template = 24,
      Notified = 25,
      ModuleExpansion = 26,
      Mbinary = 27,
      EMessage = 28,
      Flush = 29,
      WrapperRscOption = 30
   }
}