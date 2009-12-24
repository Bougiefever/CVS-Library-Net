namespace PServerClient.Responses
{
   /// <summary>
   /// Checked-in pathname \n
   //Additional data: New Entries line, \n. This means a file pathname has been
   //successfully operated on (checked in, added, etc.). name in the Entries line is
   //the same as the last component of pathname.
   /// </summary>
   public class CheckedInResponse : FileResponseBase
   {
      public override ResponseType Type { get { return ResponseType.CheckedIn; } }
   }
}