namespace PServerClient.Requests
{
   /// <summary>
   /// Argumentx text \n
   //Response expected: no. Append \n followed by text to the current argument
   //being saved.
   /// </summary>
   public class ArgumentxRequest : OneArgRequestBase
   {
      public ArgumentxRequest(string arg) : base(arg)
      {
      }
      public ArgumentxRequest(string[] lines) : base(lines) {}
      public override RequestType Type { get { return RequestType.Argumentx; } }
   }
}