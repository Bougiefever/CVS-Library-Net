namespace PServerClient.Responses
{
   /// <summary>
   /// ok \n
   ///The command completed successfully.
   /// </summary>
   public class OkResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Ok; } }
   }
}