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
   public static class TestHelper
   {
      #region SaveConversation

      public static IList<ICommandItem> CommandItems { get; set; }

      public static void AddItem(ICommandItem item)
      {
         if (CommandItems == null)
            CommandItems = new List<ICommandItem>();
         CommandItems.Add(item);
      }

      #endregion      
         
      #region XML Validation

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

      public static void SaveCommandConversation(ICommand command, string path)
      {
         FileInfo fi = new FileInfo(path);

         XDocument xdoc = command.GetXDocument(); 
         StreamWriter writer = fi.CreateText();
         xdoc.Save(writer);
      }

      #endregion

      #region Mocking

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