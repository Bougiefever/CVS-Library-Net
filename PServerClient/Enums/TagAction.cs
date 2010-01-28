namespace PServerClient
{
   /// <summary>
   /// What action to perform when doing a tag coommand
   /// </summary>
   public enum TagAction
   {
      /// <summary>
      /// Create a new tag
      /// </summary>
      Create = 0,

      /// <summary>
      /// Delete a tag
      /// </summary>
      Delete = 1,

      /// <summary>
      /// Create a branch
      /// </summary>
      Branch = 2,

      /// <summary>
      /// Move a tag
      /// </summary>
      Move = 3
   }
}