using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses.Messages
{
   public enum MessageType
   {
      Updated
   }
   public interface IMessage
   {
      MessageType Type { get; }
      string MessageData();
   }
   public class UpdatedMessage : IMessage
   {

      public MessageType Type { get { return MessageType.Updated; } }
      public string MessageData()
      {
         return "";
      }
      public string Path { get; set; }
      public string FileName { get; set; }
   }

   public class MessageFactory
   {
      public IMessage ProcessMessageResponses(IList<MessageTagResponse> responses)
      {
         IMessage message = null;
         MessageTagResponse first = responses[0];
         if (first.Message == "+updated")
         {
            UpdatedMessage updatedMessage = new UpdatedMessage();
            string fname = responses[2].Message;
            string[] names = MessageHelper.GetUpdatedFnamePathFile(fname);
            updatedMessage.Path = names[0];
            updatedMessage.FileName = names[1];
            message = updatedMessage;
         }

         return message;
      }

      public IMessage ProcessMessageResponses(IList<MessageResponse> responses)
      {
         IMessage message = null;
         MessageResponse first = responses[0];
         if (first.Message.StartsWith("M U"))
         {
            UpdatedMessage updatedMessage = new UpdatedMessage();
            string mu = first.Message;
            string[] names = MessageHelper.GetMUPathFile(mu);
            updatedMessage.Path = names[0];
            updatedMessage.FileName = names[1];
            message = updatedMessage;
         }
         return message;
      }

      public IList<IList<MessageTagResponse>> GroupMTResponses(IList<MessageTagResponse> responses)
      {
         IList<IList<MessageTagResponse>> groups = new List<IList<MessageTagResponse>>();
         IList<MessageTagResponse> group = new List<MessageTagResponse>();;
         foreach (MessageTagResponse mt in responses)
         {
            if (mt.Message.StartsWith("+"))
               group = new List<MessageTagResponse>();
            group.Add(mt);
            if (mt.Message.StartsWith("-"))
               groups.Add(group);
         }
         return groups;
      }
   }
}
