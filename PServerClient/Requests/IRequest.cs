using System.Xml.Linq;
using PServerClient.Commands;

namespace PServerClient.Requests
{
   /// <summary>
   /// Request interface to process cvs requests
   /// </summary>
   public interface IRequest : ICommandItem
   {
      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      bool ResponseExpected { get; }

      /// <summary>
      /// Gets the RequestType of the request 
      /// </summary>
      /// <value>The RequestType value</value>
      RequestType Type { get; }

      /// <summary>
      /// Gets the full request string with all the parameters that will be sent to CVS
      /// </summary>
      /// <returns>The CVS request</returns>
      string GetRequestString();
   }
}