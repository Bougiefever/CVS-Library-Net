using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class RequestBase : IRequest
   {
      protected string LineEnd = "\n";

      protected RequestBase()
      {
         //Responses = new List<IResponse>();
      }

      protected RequestBase(string[] lines)
      {
         Lines = lines;
         //Responses = new List<IResponse>();
      }

      public string RequestName { get { return RequestHelper.RequestNames[(int) Type]; } }

      public abstract bool ResponseExpected { get; }

      public virtual string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < Lines.Length; i++)
         {
            sb.Append(Lines[i]).Append(LineEnd);
         }
         string request = sb.ToString();
         return request;
      }

      public XElement GetXElement()
      {
         XElement requestElement = new XElement("Request",
                                       new XElement("ClassName", GetType().FullName),
                                       new XElement("Lines"));
         XElement linesElement = requestElement.Descendants("Lines").First();
         foreach (string s in Lines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }
         //XElement responsesElement = new XElement("Responses");
         //requestElement.Add(responsesElement);
         //foreach (IResponse response in Responses)
         //{
         //   XElement responseElement = response.GetXElement();
         //   responsesElement.Add(responseElement);
         //}
         return requestElement;
      }

      //public void CollapseResponses()
      //{
      //   IList<IResponse> newResponses = ResponseHelper.CollapseMessagesInResponses(Responses);
      //   Responses = newResponses;
      //}

      public abstract RequestType Type { get; }
      public string[] Lines { get; internal set; }
      //public IList<IResponse> Responses { get; set; }
   }
}