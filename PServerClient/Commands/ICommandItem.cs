using System.Collections.Generic;
using System.Xml.Linq;

namespace PServerClient.Commands
{
   /// <summary>
   /// A request or response item
   /// </summary>
   public interface ICommandItem
   {
      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ICommandItem"/> is processed.
      /// </summary>
      /// <value><c>true</c> if processed; otherwise, <c>false</c>.</value>
      bool Processed { get; set; }

      /// <summary>
      /// Gets or sets the lines containing the full request or response text
      /// </summary>
      /// <value>The lines collection.</value>
      IList<string> Lines { get; set; }

      /// <summary>
      /// Gets the XML Xelement for this item
      /// </summary>
      /// <returns>the Linq XElement for this item</returns>
      XElement GetXElement();
   }
}