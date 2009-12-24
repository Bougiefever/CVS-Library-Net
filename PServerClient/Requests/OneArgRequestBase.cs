namespace PServerClient.Requests
{
   public abstract class OneArgRequestBase : RequestBase
   {
      protected OneArgRequestBase(string arg)
      {
         RequestLines = new string[1];
         RequestLines[0] = string.Format("{0} {1}", RequestName, arg);
      }
      protected OneArgRequestBase(string[] lines) : base(lines) {}

      public override bool ResponseExpected { get { return false; } }
   }
}