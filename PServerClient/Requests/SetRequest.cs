namespace PServerClient.Requests
{
   /// <summary>
   /// Set variable=value \n
   //Response expected: no. Set a user variable variable to value. The Root request
   //need not have been previously sent.
   /// </summary>
   public class SetRequest : RequestBase
   {
      private readonly string _value;
      private readonly string _variable;

      public SetRequest(string variableName, string value)
      {
         _variable = variableName;
         _value = value;
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Set; } }

      public override string GetRequestString()
      {
         return string.Format("{3} {0}={1}{2}", _variable, _value, LineEnd, RequestName);
      }
   }
}