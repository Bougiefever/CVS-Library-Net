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

      public override RequestType RequestType { get { return RequestType.Argumentx; } }
   }
}