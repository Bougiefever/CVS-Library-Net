using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      public abstract void ProcessResponse(IList<string> lines);
      public abstract ResponseType ResponseType { get; }
      public abstract string DisplayResponse();
      public virtual int LineCount { get { return 1; } }
   }
}