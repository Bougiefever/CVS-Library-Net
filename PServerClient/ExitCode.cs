namespace PServerClient
{
   /// <summary>
   /// Code for the ending state of executed commands
   /// </summary>
   public enum ExitCode
   {
      /// <summary>
      /// The command completed without any errors
      /// </summary>
      Succeeded,

      /// <summary>
      /// The command completed with error message
      /// </summary>
      Failed
   }
}