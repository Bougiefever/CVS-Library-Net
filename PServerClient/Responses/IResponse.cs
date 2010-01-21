using System.Collections.Generic;
using System.Xml.Linq;
using PServerClient.Commands;

namespace PServerClient.Responses
{
   /// <summary>
   /// Interface for responses
   /// </summary>
   public interface IResponse : ICommandItem
   {
      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      int LineCount { get; }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      ResponseType Type { get; }

      /// <summary>
      /// Initializes the response with the lines from CVS
      /// </summary>
      /// <param name="lines">The response lines.</param>
      void Initialize(IList<string> lines);

      /// <summary>
      /// Processes this instance.
      /// </summary>
      void Process();

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      string Display();
   }
}