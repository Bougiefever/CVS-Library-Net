namespace PServerClient.Requests
{
   /// <summary>
   /// Max-dotdot level \n
   ///Response expected: no. Tell the server that level levels of directories above the
   ///directory which Directory requests are relative to will be needed. For example,
   ///if the client is planning to use a Directory request for ‘../../foo’, it must
   ///send a Max-dotdot request with a level of at least 2. Max-dotdot must be sent
   ///before the first Directory request.
   /// </summary>
   public class MaxDotRequest : OneArgRequestBase
   {
      public MaxDotRequest(string arg)
         : base(arg)
      {
      }

      public override RequestType RequestType { get { return RequestType.MaxDot; } }
   }
}