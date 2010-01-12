using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Is-modified filename \n
   /// Response expected: no. Additional data: none. Like Modified, but used if the
   /// server only needs to know whether the file is modified, not the contents.
   /// The commands which can take Is-modified instead of Modified with no
   /// known change in behavior are: admin, diff (if and only if two ‘-r’ or ‘-D’
   /// options are specified), watch-on, watch-off, watch-add, watch-remove,
   /// watchers, editors, log, and annotate.
   /// For the status command, one can send Is-modified but if the client is using
   /// imperfect mechanisms such as timestamps to determine whether to consider a
   /// file modified, then the behavior will be different. That is, if one sends Modified,
   /// then the server will actually compare the contents of the file sent and the one
   /// it derives from to determine whether the file is genuinely modified. But if one
   /// sends Is-modified, then the server takes the client’s word for it. A similar
   /// situation exists for tag, if the ‘-c’ option is specified.
   /// Commands for which Modified is necessary are co, ci, update, and import.
   /// Commands which do not need to inform the server about a working directory,
   /// and thus should not be sending either Modified or Is-modified: rdiff, rtag,
   /// history, init, and release.
   /// Commands for which further investigation is warranted are: remove, add, and
   /// export. Pending such investigation, the more conservative course of action is
   /// to stick to Modified.
   /// </summary>
   public class IsModifiedRequest : OneArgRequestBase
   {
      public IsModifiedRequest(string fileName)
         : base(fileName)
      {
      }

      public IsModifiedRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.IsModified;
         }
      }
   }
}