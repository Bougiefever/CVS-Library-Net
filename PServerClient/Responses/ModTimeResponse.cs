using System;

namespace PServerClient.Responses
{
   /// <summary>
   /// Mod-time time \n<br/>
   /// Set the modification time of the next file sent to time. Mod-time is a file
   /// update<br/>
   /// modifying response as described in Section 5.9 [Response intro], page 22. The<br/>
   /// time is in the format specified by RFC822 as modified by RFC1123. The server<br/>
   /// may specify any timezone it chooses; clients will want to convert that to their<br/>
   /// own timezone as appropriate. An example of this format is:<br/>
   /// 26 May 1997 13:01:40 -0400<br/>
   /// There is no requirement that the client and server clocks be synchronized. The<br/>
   /// server just sends its recommendation for a timestamp (based on its own clock,<br/>
   /// presumably), and the client should just believe it (this means that the time<br/>
   /// might be in the future, for example).<br/>
   /// If the server does not send Mod-time for a given file, the client should pick a<br/>
   /// modification time in the usual way (usually, just let the operating system set<br/>
   /// the modification time to the time that the CVS command is running).<br/>
   /// </summary>
   public class ModTimeResponse : ResponseBase
   {
      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.ModTime;
         }
      }

      /// <summary>
      /// Gets or sets the date and time sent from CVS
      /// </summary>
      /// <value>The date and time.</value>
      public DateTime ModTime { get; set; }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         string date = Lines[0];
         ModTime = date.Rfc822ToDateTime();
         base.Process();
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return ModTime.ToString();
      }
   }
}