namespace PServerClient.Requests
{
   /// <summary>
   /// watch-add \n
   /// Response expected: yes. Actually do the cvs watch on, cvs watch off, cvs
   /// watch add, and cvs watch remove commands, respectively. This uses any pre-
   /// vious Argument, Directory, Entry, or Modified requests, if they have been
   /// sent. The last Directory sent specifies the working directory at the time of the
   /// operation.
   /// </summary>
   public class WatchAddRequest : NoArgRequestBase
   {
      public WatchAddRequest()
      {
      }

      public WatchAddRequest(string[] lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.WatchAdd;
         }
      }
   }
}