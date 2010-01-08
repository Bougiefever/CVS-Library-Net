namespace PServerClient.Requests
{
   /// <summary>
   /// rlog \n
   /// Response expected: yes. Actually do a cvs command. This uses any previ-
   /// ous Argument requests, if they have been sent. The client should not send
   /// Directory, Entry, or Modified requests for these commands; they are not
   /// used. Arguments to these commands are module names, as described for co.
   /// </summary>
   public class RLogRequest : NoArgRequestBase
   {
      public RLogRequest()
      {
      }

      public RLogRequest(string[] lines)
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
            return RequestType.RLog;
         }
      }
   }
}