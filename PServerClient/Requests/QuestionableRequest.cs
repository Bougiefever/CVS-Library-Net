using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class QuestionableRequest : OneArgRequestBase
   {
      public QuestionableRequest(string fileName) : base(fileName) {}
      public override string RequestName { get { return "Questionable"; } }
   }
}
