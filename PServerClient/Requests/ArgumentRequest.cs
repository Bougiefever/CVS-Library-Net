namespace PServerClient.Requests
{
   /// <summary>
   /// Argument text \n
   //Response expected: no. Save argument for use in a subsequent command.
   //Arguments accumulate until an argument-using command is given, at which
   //point they are forgotten.
   /// </summary>
   public class ArgumentRequest : OneArgRequestBase
   {
      public ArgumentRequest(string arg) : base(arg)
      {
      }
      public ArgumentRequest(string[] lines) : base(lines) {}

      public override RequestType Type { get { return RequestType.Argument; } }
   }
}