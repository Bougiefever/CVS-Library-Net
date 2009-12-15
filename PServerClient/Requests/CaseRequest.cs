namespace PServerClient.Requests
{
   /// <summary>
   /// Case \n
   //Response expected: no. Tell the server that filenames should be matched in
   //a case-insensitive fashion. Note that this is not the primary mechanism for
   //achieving case-insensitivity; for the most part the client keeps track of the case
   //________________________________________
   //Page 16
   //which the server wants to use and takes care to always use that case regardless of
   //what the user specifies. For example the filenames given in Entry and Modified
   //requests for the same file must match in case regardless of whether the Case
   //request is sent. The latter mechanism is more general (it could also be used for
   //8.3 filenames, VMS filenames with more than one ‘.’, and any other situation in
   //which there is a predictable mapping between filenames in the working directory
   //and filenames in the protocol), but there are some situations it cannot handle
   //(ignore patterns, or situations where the user specifies a filename and the client
   //does not know about that file).
   //Though this request will be supported into the forseeable future, it has been
   //the source of numerous bug reports in the past due to the complexity of testing
   //this functionality via the test suite and client developers are encouraged not to
   //use it. Instead, please consider munging conflicting names and maintaining a
   //map for communicating with the server. For example, suppose the server sends
   //files ‘case’, ‘CASE’, and ‘CaSe’. The client could write all three files to names
   //such as, ‘case’, ‘case_prefix_case’, and ‘case_prefix_2_case’ and maintain
   //a mapping between the file names in, for instance a new ‘CVS/Map’ file.
   /// </summary>
   public class CaseRequest : NoArgRequestBase
   {
      public override RequestType RequestType { get { return RequestType.Case; } }
   }
}