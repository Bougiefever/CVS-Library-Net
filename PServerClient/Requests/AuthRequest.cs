using System;
using System.Text;
using PServerClient.Responses;
using System.Linq;

namespace PServerClient.Requests
{
   public class AuthRequest : AuthRequestBase
   {
      public AuthRequest(CvsRoot root) : base(root, "AUTH")
      {
      }
   }
}
