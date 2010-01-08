namespace PServerClient.Requests
{
   /// <summary>
   /// co \n
   /// Response expected: yes. Get files from the repository. This uses any previous
   /// Argument, Directory, Entry, or Modified requests, if they have been sent.
   /// Arguments to this command are module names; the client cannot know what
   /// directories they correspond to except by (1) just sending the co request, and
   /// then seeing what directory names the server sends back in its responses, and
   /// (2) the expand-modules request.
   /// </summary>
   public class CheckOutRequest : NoArgRequestBase
   {
      public CheckOutRequest()
      {
      }

      public CheckOutRequest(string[] lines)
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
            return RequestType.CheckOut;
         }
      }
   }
}