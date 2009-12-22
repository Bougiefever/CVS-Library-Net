namespace PServerClient.Requests
{
   public abstract class NoArgRequestBase : RequestBase
   {
      protected NoArgRequestBase()
      {
         RequestLines = new string[1];
         RequestLines[0] = RequestName;
      }

      public override bool ResponseExpected { get { return false; } }
   }
}