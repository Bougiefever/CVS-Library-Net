using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PServerClient.Commands;

namespace PServerClient.Responses
{
   /// <summary>
   /// Base class for all responses
   /// </summary>
   public abstract class ResponseBase : IResponse
   {
      /// <summary>
      /// Gets or sets the lines containing the full request or response text
      /// </summary>
      /// <value>The lines collection.</value>
      public IList<string> Lines { get; set; }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public abstract ResponseType Type { get; }

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ICommandItem"/> is processed.
      /// </summary>
      /// <value><c>true</c> if processed; otherwise, <c>false</c>.</value>
      public bool Processed { get; set; }

      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      public virtual int LineCount
      {
         get
         {
            return 1;
         }
      }

      /// <summary>
      /// Initializes the response with the lines from CVS
      /// </summary>
      /// <param name="lines">The response lines.</param>
      public virtual void Initialize(IList<string> lines)
      {
         Lines = new List<string>(LineCount);
         for (int i = 0; i < LineCount; i++)
            Lines.Add(lines[i]);
         Processed = false;
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public virtual void Process()
      {
         Processed = true;
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public virtual string Display()
      {
         string response = String.Join("\n", Lines.ToArray());
         return response;
      }

      /// <summary>
      /// Gets the XML Xelement for this item
      /// </summary>
      /// <returns>the Linq XElement for this item</returns>
      public XElement GetXElement()
      {
         XElement responseElement = new XElement(
                                                 "Response",
                                                 new XElement("ClassName", GetType().FullName),
                                                 new XElement("Name", ResponseHelper.ResponseNames[(int) Type]),
                                                 new XElement("Lines"));
         XElement linesElement = responseElement.Descendants("Lines").First();

         foreach (string s in Lines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }

         if (this is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse) this;
            string length = fileResponse.Length.ToString();
            string contents = ResponseHelper.FileContentsToByteArrayString(fileResponse.Contents);
            XElement responseFile = new XElement(
                                                 "File",
                                                 new XElement("Length", length),
                                                 new XElement("Contents", contents));
            responseElement.Add(responseFile);
         }

         return responseElement;
      }
   }
}