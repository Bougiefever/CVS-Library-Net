using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      protected RequestBase()
      {
      }

      protected RequestBase(string[] lines)
      {
         Lines = lines;
      }

      public string RequestName
      {
         get
         {
            return RequestHelper.RequestNames[(int) Type];
         }
      }

      public abstract bool ResponseExpected { get; }

      public abstract RequestType Type { get; }

      public string[] Lines { get; internal set; }

      public virtual string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < Lines.Length; i++)
         {
            sb.Append(Lines[i]).Append(PServerHelper.UnixLineEnd);
         }

         string request = sb.ToString();
         return request;
      }

      public XElement GetXElement()
      {
         XElement requestElement = new XElement( 
                                                "Request",
                                                new XElement("ClassName", GetType().FullName),
                                                new XElement("Lines"));
         XElement linesElement = requestElement.Descendants("Lines").First();
         foreach (string s in Lines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }

         return requestElement;
      }
   }
}