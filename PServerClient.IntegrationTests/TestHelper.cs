using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using PServerClient.Commands;

namespace PServerClient.IntegrationTests
{
   public static class TestHelper
   {
      public static void SaveResponsesToFile(ICommand command, FileInfo file)
      {
         TextWriter writer = file.CreateText();
         XDocument xdoc = command.ResponsesXML();
         xdoc.Save(writer);
      }
   }
}