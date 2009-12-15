namespace PServerClient.Requests
{
   /// <summary>
   /// Directory local-directory \n
   //Additional data: repository \n. Response expected: no. Tell the server what
   //directory to use. The repository should be a directory name from a previous
   //server response. Note that this both gives a default for Entry and Modified
   //and also for ci and the other commands; normal usage is to send Directory
   //for each directory in which there will be an Entry or Modified, and then a final
   //Directory for the original directory, then the command. The local-directory
   //is relative to the top level at which the command is occurring (i.e. the last
   //Directory which is sent before the command); to indicate that top level, ‘.’
   //should be sent for local-directory.
   /// </summary>
   public class DirectoryRequest : RequestBase
   {
      private readonly CvsRoot _root;

      public DirectoryRequest(CvsRoot root)
      {
         _root = root;
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Directory; } }

      public override string GetRequestString()
      {
         return string.Format("{4} .{0}{1}/{2}{3}", LineEnd, _root.Root, _root.Module, LineEnd, RequestName);
      }
   }
}