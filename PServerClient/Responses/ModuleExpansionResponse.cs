using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Module-expansion pathname \n
   //Return a file or directory which is included in a particular module. pathname
   //is relative to cvsroot, unlike most pathnames in responses. pathname should
   //be used to look and see whether some or all of the module exists on the client
   //side; it is not necessarily suitable for passing as an argument to a co request
   //(for example, if the modules file contains the ‘-d’ option, it will be the directory
   //specified with ‘-d’, not the name of the module).
   /// </summary>
   public class ModuleExpansionResponse : ResponseBase
   {
      public override ResponseType Type { get { return ResponseType.ModuleExpansion; } }
      public string ModuleName { get; set; }

      public override void Process()
      {
         ModuleName = Lines[0];
         base.Process();
      }

      public override string Display()
      {
         return ModuleName;
      }
   }
}