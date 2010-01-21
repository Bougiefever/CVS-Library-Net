namespace PServerClient
{
   /// <summary>
   /// Command type of a class the implements the ICommand interface
   /// </summary>
   public enum CommandType
   {
      /// <summary>
      /// CheckOut command
      /// </summary>
      CheckOut = 0,

      /// <summary>
      /// Import command
      /// </summary>
      Import = 1,

      /// <summary>
      /// ValidRequestsList command
      /// </summary>
      ValidRequestsList = 2,

      /// <summary>
      /// VerifyAuth command
      /// </summary>
      VerifyAuth = 3,

      /// <summary>
      /// Version command
      /// </summary>
      Version = 4,

      /// <summary>
      /// Add command
      /// </summary>
      Add = 5,

      /// <summary>
      /// Export command
      /// </summary>
      Export = 6,

      /// <summary>
      /// Log command
      /// </summary>
      Log = 7,

      /// <summary>
      /// Diff command
      /// </summary>
      Diff = 8,

      /// <summary>
      /// Tag command
      /// </summary>
      Tag = 9
   }
}