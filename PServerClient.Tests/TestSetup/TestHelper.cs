using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Responses;

namespace PServerClient.Tests.TestSetup
{
   /// <summary>
   /// Helper methods for easier testing
   /// </summary>
   public static class TestHelper
   {
      #region SaveConversation

      /// <summary>
      /// Gets or sets the command items.
      /// </summary>
      /// <value>The command items.</value>
      public static IList<ICommandItem> CommandItems { get; set; }

      /// <summary>
      /// Adds the command item.
      /// </summary>
      /// <param name="item">The command item.</param>
      public static void AddItem(ICommandItem item)
      {
         if (CommandItems == null)
            CommandItems = new List<ICommandItem>();
         CommandItems.Add(item);
      }

      #endregion      
         
      #region XML Validation

      /// <summary>
      /// Validates the response XML.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <returns>true / false</returns>
      public static bool ValidateResponseXML(XElement response)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Response.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         XDocument xdoc = new XDocument(response);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         return true;
      }

      /// <summary>
      /// Validates the command XML.
      /// </summary>
      /// <param name="command">The command.</param>
      /// <returns>true or false</returns>
      public static bool ValidateCommandXML(XDocument command)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Command.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         command.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         return true;
      }

      /// <summary>
      /// Validates the request XML.
      /// </summary>
      /// <param name="request">The request.</param>
      /// <returns>true or false</returns>
      public static bool ValidateRequestXML(XElement request)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Request.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         XDocument xdoc = new XDocument(request);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         return true;
      }

      #endregion

      #region Objects to XML

      /// <summary>
      /// Saves the command conversation.
      /// </summary>
      /// <param name="command">The command.</param>
      /// <param name="path">The path to save the file to.</param>
      public static void SaveCommandConversation(ICommand command, string path)
      {
         FileInfo fi = new FileInfo(path);

         XDocument xdoc = command.GetXDocument(); 
         StreamWriter writer = fi.CreateText();
         xdoc.Save(writer);
      }

      #endregion

      #region Mocking

      /// <summary>
      /// Gets the mock checkout responses.
      /// </summary>
      /// <param name="time">The mod time.</param>
      /// <param name="path">The local system path.</param>
      /// <param name="file">The file name.</param>
      /// <returns>list of mock responses</returns>
      public static IList<IResponse> GetMockCheckoutResponses(string time, string path, string file)
      {
         IList<IResponse> responses = new List<IResponse>();
         IResponse r = new ModTimeResponse();
         r.Initialize(new List<string> { time });
         responses.Add(r);
         var list = GetMockMTResponseGroup(path + file);
         foreach (IResponse response in list)
         {
            responses.Add(response);
         }

         responses.Add(GetMockUpdatedResponse(path, file));

         return responses;
      }

      /// <summary>
      /// Gets the mock MT response group.
      /// </summary>
      /// <param name="fname">The fname.</param>
      /// <returns>list of mock MT responses</returns>
      public static IList<IResponse> GetMockMTResponseGroup(string fname)
      {
         IList<IResponse> responses = new List<IResponse>();
         string[] messages = new[] { "+updated", "text U", "fname " + fname, "newline", "-updated" };
         foreach (string s in messages)
         {
            MTMessageResponse m = new MTMessageResponse();
            m.Initialize(new List<string> { s });
            responses.Add(m);
         }

         return responses;
      }

      /// <summary>
      /// Gets the mock updated response.
      /// </summary>
      /// <param name="path">The file path.</param>
      /// <param name="name">The file name.</param>
      /// <returns>a mock UpdateResponse instance</returns>
      public static UpdatedResponse GetMockUpdatedResponse(string path, string name)
      {
         UpdatedResponse res = new UpdatedResponse();
         IList<string> lines = new List<string>
                                  {
                                     path,
                                     "/usr/local/cvsroot/sandbox/" + path + name,
                                     "/" + name + "/1.1.1.1///",
                                     "u=rw,g=rw,o=rw",
                                     "5"
                                  };
         res.Initialize(lines);
         string text = "abcde";
         res.Contents = text.Encode();
         return res;
      }

      /// <summary>
      /// Strings the list to mock int array.
      /// </summary>
      /// <param name="lines">The lines.</param>
      /// <param name="lastChar">The last char.</param>
      /// <returns>the int array</returns>
      public static int[] StringListToMockIntArray(IList<string> lines, int lastChar)
      {
         byte[] chars = new byte[0];
         byte[] copy;
         foreach (string line in lines)
         {
            byte[] lineBytes = line.Encode();
            copy = new byte[chars.Length];
            chars.CopyTo(copy, 0);
            chars = new byte[chars.Length + lineBytes.Length + 1];
            copy.CopyTo(chars, 0);
            lineBytes.CopyTo(chars, copy.Length);
            chars[chars.Length - 1] = 10;
         }

         int[] ints = new int[chars.Length + 1];
         chars.CopyTo(ints, 0);
         ints[ints.Length - 1] = lastChar;
         return ints;
      }

      /// <summary>
      /// Strings the list to mock byte array.
      /// </summary>
      /// <param name="lines">The lines.</param>
      /// <param name="lineSeperator">The line seperator.</param>
      /// <returns>byte array of string</returns>
      public static byte[] StringListToMockByteArray(IList<string> lines, byte[] lineSeperator)
      {
         byte[] chars = new byte[0];
         byte[] copy;
         int len = lineSeperator.Length;

         foreach (string line in lines)
         {
            byte[] lineBytes = line.Encode();
            copy = new byte[chars.Length];
            chars.CopyTo(copy, 0);
            chars = new byte[chars.Length + lineBytes.Length + len];
            copy.CopyTo(chars, 0);
            lineBytes.CopyTo(chars, copy.Length);
            for (int i = 0; i < len; i++)
            {
               int idx = chars.Length - len;
               chars[idx + i] = lineSeperator[i];
            }
            ////chars[chars.Length - 1] = 10;
         }

         ////int[] ints = new int[chars.Length + 1];
         ////chars.CopyTo(ints, 0);
         ////ints[ints.Length - 1] = lastChar;
         return chars;
      }

      #endregion
   }
}