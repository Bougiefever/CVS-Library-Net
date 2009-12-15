namespace PServerClient.Requests
{
   /// <summary>
   /// init root-name \n
   //Response expected: yes. If it doesn’t already exist, create a cvs repository
   //root-name. Note that root-name is a local directory and not a fully qualified
   //CVSROOT variable. The Root request need not have been previously sent.
   //update \n Response expected: yes. Actually do a cvs update command. This uses any
   //previous Argument, Directory, Entry, or Modified requests, if they have been
   //sent. The last Directory sent specifies the working directory at the time of the
   //operation. The -I option is not used–files which the client can decide whether
   //to ignore are not mentioned and the client sends the Questionable request for
   //others.
   /// </summary>
   public class InitRequest : OneArgRequestBase
   {
      public InitRequest(string rootName) : base(rootName)
      {
      }

      public override bool ResponseExpected { get { return true; } }
      public override RequestType RequestType { get { return RequestType.Init; } }
   }
}