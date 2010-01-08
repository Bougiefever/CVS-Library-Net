namespace PServerClient.Requests
{
   /// <summary>
   /// import \n 
   /// Response expected: yes. Actually do a cvs import command. This uses any
   /// previous Argument, Directory, Entry, or Modified requests, if they have been
   /// sent. The last Directory sent specifies the working directory at the time of
   /// the operation - unlike most commands, the repository field of each Directory
   /// request is ignored (it merely must point somewhere within the root). The files to
   /// be imported are sent in Modified requests (files which the client knows should
   /// be ignored are not sent; the server must still process the CVSROOT/cvsignore
   /// file unless -I ! is sent). A log message must have been specified with a -m
   /// argument.
   /// </summary>
   public class ImportRequest : NoArgRequestBase
   {
      public ImportRequest()
      {
      }

      public ImportRequest(string[] lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.Import;
         }
      }
   }
}