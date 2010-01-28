using System;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// Tags files in CVS
   /// </summary>
   public class RTagCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RTagCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      public RTagCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets or sets the tag action to perform
      /// </summary>
      /// <value>The tag action.</value>
      public TagAction TagAction { get; set; }

      /// <summary>
      /// Gets or sets the tag.
      /// </summary>
      /// <value>The cvs tag.</value>
      public string Tag { get; set; }

      /// <summary>
      /// Gets the command type. 
      /// </summary>
      /// <value></value>
      public override CommandType Type
      {
         get
         {
            return CommandType.RTag;
         }
      }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {

         Requests.Add(new RootRequest(Root.Repository));
         Requests.Add(new GlobalOptionRequest(GlobalOption.Quiet)); // somewhat quiet
         if (TagAction == TagAction.Delete)
            Requests.Add(new ArgumentRequest("-d")); // -d Delete the tag instead of creating it.
         if (TagAction == TagAction.Branch)
            Requests.Add(new ArgumentRequest("-b")); // -b Make the tag a branch tag. 
         Requests.Add(new ArgumentRequest(Tag));
         Requests.Add(new ArgumentRequest(Root.Module));
         Requests.Add(new RTagRequest());
      }
   }
}