using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Responses;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseXMLTest
   {
      [Test]
      public void SetStaticDirectoryToXMLTest()
      {
         SetStaticDirectoryResponse response = new SetStaticDirectoryResponse();
         IList<string> lines = new List<string>();
         lines.Add("abougie/");
         lines.Add("/usr/local/cvsroot/sandbox/abougie/");
         response.ProcessResponse(lines);
         XDocument xdoc = response.ToXML();
      }

      [Test]
      public void ResponseBaseToXMLTest()
      {

      }

      [Test]
      public void Test()
      {

      }
      
   }
}