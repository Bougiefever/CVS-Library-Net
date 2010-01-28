namespace PServerClient
{
   /// <summary>
   /// Status of CVS authentication before commands can be started
   /// </summary>
   public enum AuthStatus
   {
      /// <summary>
      /// CVS authenticated failed
      /// </summary>
      NotAuthenticated = 0,

      /// <summary>
      /// CVS authenticate succeeded
      /// </summary>
      Authenticated = 1,

      /// <summary>
      /// There was an error with authentication
      /// </summary>
      Error = 2
   }
}