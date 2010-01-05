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

         XDocument xdoc = command.GetXDocument(); //ICommandToXDocument(command);
         StreamWriter writer = fi.CreateText();
         xdoc.Save(writer);
      }

      #endregion

      #region Mocking

      public static IList<IResponse> GetMockCheckoutResponses(string time, string path, string file)
      {
         IList<IResponse> responses = new List<IResponse>();
         IResponse r = new ModTimeResponse();
         r.Process(new List<string> { time });
         responses.Add(r);
         var list = (GetMockMTResponseGroup(path + file));
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
            MessageTagResponse m = new MessageTagResponse();
            m.Process(new List<string> { s });
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
         res.Process(lines);
         string text = "abcde";
         res.Contents = text.Encode();
         return res;
      }

      #endregion
   }
}