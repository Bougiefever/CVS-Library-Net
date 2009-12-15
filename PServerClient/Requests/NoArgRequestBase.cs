namespace PServerClient.Requests
{
   public abstract class NoArgRequestBase : RequestBase
   {
      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         return string.Format("{0}{1}", RequestName, LineEnd);
      }
   }
}