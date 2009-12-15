using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Responses;
using PServerClient.Responses.Messages;

namespace PServerClient.Tests
{
   [TestFixture]
   public class MessageTests
   {
      [Test]
      public void MTUpdatedMessagesProcessTest()
      {
         IList<MessageTagResponse> mts = new List<MessageTagResponse>
                                            {
                                               new MessageTagResponse {Message = "+updated"},
                                               new MessageTagResponse {Message = "text U"},
                                               new MessageTagResponse {Message = "fname abougie/New Text Document.txt"},
                                               new MessageTagResponse {Message = "newline"},
                                               new MessageTagResponse {Message = "-updated"}
                                            };
         MessageFactory factory = new MessageFactory();
         IMessage msg = factory.ProcessMessageResponses(mts);
         Assert.IsInstanceOf(typeof(UpdatedMessage), msg);
         UpdatedMessage updatedMessage = (UpdatedMessage) msg;
         Assert.AreEqual("abougie", updatedMessage.Path);
         Assert.AreEqual("New Text Document.txt", updatedMessage.FileName);
      }

      [Test]
      public void MUMessageProcessTest()
      {
         IList<MessageResponse> ms = new List<MessageResponse>
                                        {
                                           new MessageResponse {Message = "M U abougie/New Text Document.txt"}
                                        };
         MessageFactory factory = new MessageFactory();
         IMessage msg = factory.ProcessMessageResponses(ms);
         Assert.IsInstanceOf(typeof(UpdatedMessage), msg);
         UpdatedMessage updatedMessage = (UpdatedMessage)msg;
         Assert.AreEqual("abougie", updatedMessage.Path);
         Assert.AreEqual("New Text Document.txt", updatedMessage.FileName);
      }

      [Test]
      public void GroupMTResponses()
      {
         IList<MessageTagResponse> mts = new List<MessageTagResponse>
                                            {
                                               new MessageTagResponse {Message = "+message1"},
                                               new MessageTagResponse {Message = "text U"},
                                               new MessageTagResponse {Message = "fname abougie/file1.cs"},
                                               new MessageTagResponse {Message = "newline"},
                                               new MessageTagResponse {Message = "-message1"},
                                               new MessageTagResponse {Message = "+message2"},
                                               new MessageTagResponse {Message = "text U"},
                                               new MessageTagResponse {Message = "fname abougie/file2.cs"},
                                               new MessageTagResponse {Message = "newline"},
                                               new MessageTagResponse {Message = "-message2"}
                                            };
         MessageFactory factory = new MessageFactory();
         IList<IList<MessageTagResponse>> groups = factory.GroupMTResponses(mts);
         Assert.AreEqual(2, groups.Count);
      }
   }
}
