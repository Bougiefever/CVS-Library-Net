namespace PServerClient.Requests
{
   public abstract class OneArgRequestBase : RequestBase
   {
      internal string _arg;

      public OneArgRequestBase(string arg)
      {
         _arg = arg;
      }

      public override bool ResponseExpected { get { return false; } }

      public override string GetRequestString()
      {
         return string.Format("{0} {1}{2}", RequestName, _arg, LineEnd);
      }
   }
}