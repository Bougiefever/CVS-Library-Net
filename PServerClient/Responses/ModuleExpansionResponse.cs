namespace PServerClient.Responses
{
   /// <summary>
   /// Module-expansion pathname \n
   /// Return a file or directory which is included in a particular module. pathname
   /// is relative to cvsroot, unlike most pathnames in responses. pathname should
   /// be used to look and see whether some or all of the module exists on the client
   /// side; it is not necessarily suitable for passing as an argument to a co request
   /// (for example, if the modules file contains the ‘-d’ option, it will be the directory
   /// specified with ‘-d’, not the name of the module).
   /// </summary>
   public class ModuleExpansionResponse : ResponseBase
   {
      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.ModuleExpansion;
         }
      }

      /// <summary>
      /// Gets or sets the name of the module.
      /// </summary>
      /// <value>The name of the module.</value>
      public string ModuleName { get; set; }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         ModuleName = Lines[0];
         base.Process();
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return ModuleName;
      }
   }
}