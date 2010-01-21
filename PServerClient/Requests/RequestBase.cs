using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PServerClient.Commands;

namespace PServerClient.Requests
{
   /// <summary>
   /// Base class for all requests
   /// </summary>
   public abstract class RequestBase : IRequest
   {
      protected RequestBase()
      {
      }

      protected RequestBase(IList<string> lines)
      {
         Lines = lines;
      }

      /// <summary>
      /// Gets the request string that will be sent to CVS
      /// </summary>
      /// <value>The request string.</value>
      public string RequestName
      {
         get
         {
            return RequestHelper.RequestNames[(int) Type];
         }
      }

      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request.
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      public abstract bool ResponseExpected { get; }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public abstract RequestType Type { get; }

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ICommandItem"/> is processed.
      /// </summary>
      /// <value><c>true</c> if processed; otherwise, <c>false</c>.</value>
      public bool Processed { get; set; }

      /// <summary>
      /// Gets or sets the lines containing the full request or response text
      /// </summary>
      /// <value>The lines collection.</value>
      public IList<string> Lines { get; set; }

      /// <summary>
      /// Gets the full request string with all the parameters that will be sent to CVS
      /// </summary>
      /// <returns>The CVS request</returns>
      public virtual string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < Lines.Count; i++)
         {
            sb.Append(Lines[i]).Append(PServerHelper.UnixLineEnd);
         }

         string request = sb.ToString();
         return request;
      }

      /// <summary>
      /// Gets the XML Xelement for this item
      /// </summary>
      /// <returns>the Linq XElement for this item</returns>
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