using System.Collections.Generic;
using System.Xml.Linq;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   /// <summary>
   /// Inteface for CVS commands
   /// </summary>
   public interface ICommand
   {
      /// <summary>
      /// Gets the command type.
      /// </summary>
      /// <value>The command type.</value>
      CommandType Type { get; }

      /// <summary>
      /// Gets or sets the collection of required requests. These requests are performed and
      /// evaluated before the main execution begins.
      /// </summary>
      IList<IRequest> RequiredRequests { get; set; }

      /// <summary>Gets or sets the requests to perform the command.</summary>
      IList<IRequest> Requests { get; set; }

      /// <summary>Gets or sets the responses that are temporarily kept during processing.</summary>
      IList<IResponse> Responses { get; set; }

      /// <summary>
      /// Gets or sets the requests and responses that occur during execution
      /// For testing and troubleshooting. The entire CVS client/server conversation is
      /// stored here as it was executed. The storage of the request/response items should be
      /// turned off for better performance.
      /// </summary>
      IList<ICommandItem> Items { get; set; }

      /// <summary>
      /// Gets or sets the exit code.
      /// The end result of the command. If there was an "ok" response, this has a value
      /// "Succeeded". If there was an "error" response, this has a value of "Failed". If neither
      /// "ok" nor "error" was received, this is set to "Failed".
      /// </summary>
      /// <value>The exit code.</value>
      ExitCode ExitCode { get; set; }

      /// <summary>Gets any messages that should be sent back to the user.</summary>
      IList<string> UserMessages { get; }

      /// <summary>
      /// Handles the execution of the CVS command. Ensures that all the requests
      /// are sent, and that responses are retrieved at the appropriate time.
      /// The processing of responses is handled by the command classes.
      /// </summary>
       void Execute();

       /// <summary>
       /// Gets the XML representation of the command.
       /// </summary>
       /// <returns>XDocument xml of the command object</returns>
       XDocument GetXDocument();
   }
}