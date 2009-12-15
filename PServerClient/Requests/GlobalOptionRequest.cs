namespace PServerClient.Requests
{
   /// <summary>
   /// Global_option option \n
   //Response expected: no. Transmit one of the global options ‘-q’, ‘-Q’, ‘-l’,
   //‘-t’, ‘-r’, or ‘-n’. option must be one of those strings, no variations (such as
   //combining of options) are allowed. For graceful handling of valid-requests,
   //it is probably better to make new global options separate requests, rather than
   //trying to add them to this request. The Root request need not have been
   //previously sent.
   /// </summary>
   public class GlobalOptionRequest : OneArgRequestBase
   {
      public GlobalOptionRequest(string arg) : base(arg)
      {
      }

      public override RequestType RequestType { get { return RequestType.GlobalOption; } }
   }
}