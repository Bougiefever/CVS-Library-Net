namespace PServerClient.Requests
{
   public abstract class NoArgRequestBase : RequestBase
   {
      protected NoArgRequestBase()
      {
         Lines = new string[1];
         Lines[0] = RequestName;
      }

      protected NoArgRequestBase(string[] lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return false;
         }
      }
   }
}