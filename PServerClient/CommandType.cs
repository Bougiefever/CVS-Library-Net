namespace PServerClient
{
   /// <summary>
   /// Command type of a class the implements the ICommand interface
   /// </summary>
   public enum CommandType
   {
      /// <summary>
      /// 
      /// </summary>
      CheckOut = 0,

      /// <summary>
      /// 
      /// </summary>
      Import = 1,

      /// <summary>
      /// 
      /// </summary>
      ValidRequestsList = 2,

      /// <summary>
      /// 
      /// </summary>
      VerifyAuth = 3,

      /// <summary>
      /// 
      /// </summary>
      Version = 4,

      /// <summary>
      /// 
      /// </summary>
      Add = 5,

      /// <summary>
      /// 
      /// </summary>
      Export = 6,

      /// <summary>
      /// 
      /// </summary>
      Log = 7,

      /// <summary>
      /// 
      /// </summary>
      Diff = 8,

      /// <summary>
      /// 
      /// </summary>
      Tag = 9
   }
}