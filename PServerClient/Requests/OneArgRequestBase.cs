namespace PServerClient.Requests
{
   public abstract class OneArgRequestBase : RequestBase
   {
      protected OneArgRequestBase(string arg)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, arg);
      }

      protected OneArgRequestBase(string[] lines)
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