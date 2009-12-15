using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// MT tagname data \n
   //This response provides for tagged text. It is similar to SGML/HTML/XML in
   //that the data is structured and a naive application can also make some sense of
   //it without understanding the structure. The syntax is not SGML-like, however,
   //in order to fit into the CVS protocol better and (more importantly) to make it
   //easier to parse, especially in a language like perl or awk.
   //The tagname can have several forms. If it starts with ‘a’ to ‘z’ or ‘A’ to ‘Z’,
   //then it represents tagged text. If the implementation recognizes tagname, then
   //it may interpret data in some particular fashion. If the implementation does
   //not recognize tagname, then it should simply treat data as text to be sent to the
   //user (similar to an ‘M’ response). There are two tags which are general purpose.
   //The ‘text’ tag is similar to an unrecognized tag in that it provides text which
   //will ordinarily be sent to the user. The ‘newline’ tag is used without data and
   //indicates that a newline will ordinarily be sent to the user (there is no provision
   //for embedding newlines in the data of other tagged text responses).
   //If tagname starts with ‘+’ it indicates a start tag and if it starts with ‘-’ it
   //indicates an end tag. The remainder of tagname should be the same for match-
   //ing start and end tags, and tags should be nested (for example one could have
   //tags in the following order +bold +italic text -italic -bold but not +bold
   //+italic text -bold -italic). A particular start and end tag may be docu-
   //mented to constrain the tagged text responses which are valid between them.
   //Note that if data is present there will always be exactly one space between
   //tagname and data; if there is more than one space, then the spaces beyond the
   //first are part of data.
   //Here is an example of some tagged text responses. Note that there is a trailing
   //space after ‘Checking in’ and ‘initial revision:’ and there are two trailing
   //spaces after ‘<--’. Such trailing spaces are, of course, part of data.
   //MT +checking-in
   //MT text Checking in
   //MT fname gz.tst
   //MT text ;
   //MT newline
   //MT rcsfile /home/kingdon/zwork/cvsroot/foo/gz.tst,v
   //MT text
   //<--
   //MT fname gz.tst
   //MT newline
   //MT text initial revision:
   //MT init-rev 1.1
   //MT newline
   //MT text done
   //MT newline
   //MT -checking-in
   //If the client does not support the ‘MT’ response, the same responses might be
   //sent as:
   //M Checking in gz.tst;
   //M /home/kingdon/zwork/cvsroot/foo/gz.tst,v <-- gz.tst
   //M initial revision: 1.1
   //M done
   //For a list of specific tags, see Section 5.12 [Text tags], 
   /// </summary>
   public class MessageTagResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.MessageTag; } }
      public string Message { get; set; }
      public override string DisplayResponse()
      {
         return Message;
      }

      public override int LineCount { get { return 1; } }

      public override void ProcessResponse(IList<string> lines)
      {
         Message = lines[0];
      }
   }
}