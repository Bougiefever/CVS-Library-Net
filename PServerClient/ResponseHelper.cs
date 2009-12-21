using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using PServerClient.Responses;

namespace PServerClient
{
   public static class ResponseHelper
   {
      public static readonly string[] ResponsePatterns;
      private const string AuthRegex = @"(I LOVE YOU|I HATE YOU)(.*)";
      private const string OkRegex = @"^ok(.*)";
      private const string ErrorRegex = @"^error\s(.*)";
      private const string MessageRegex = @"^M\s(.*)";
      private const string ValidRequestsRegex = @"^Valid-requests\s(.*)";
      private const string CheckedInRegex = @"^Checked-in\s(.*)";
      private const string NewEntryRegex = @"^New-entry\s(.*)";
      private const string UpdatedRegex = @"^Updated\s(.*)";
      private const string MergedRegex = @"^Merged\s(.*)";
      private const string PatchedRegex = @"^Patched\s(.*)";
      private const string ChecksumRegex = @"^Checksum\s(.*)";
      private const string CopyFileRegex = @"^Copy-file\s(.*)";
      private const string RemovedRegex = @"^Removed\s(.*)";
      private const string RemoveEntryRegex = @"^Remove-entry\s(.*)";
      private const string SetStaticDirectoryRegex = @"^Set-static-directory\s(.*)";
      private const string ClearStaticDirectoryRegex = @"^Clear-static-directory\s(.*)";
      private const string SetStickyRegex = @"^Set-sticky\s(.*)";
      private const string ClearStickyRegex = @"^Clear-sticky\s(.*)";
      private const string CreatedRegex = @"^Created\s(.*)";
      private const string MessageTagRegex = @"^MT\s(.*)";
      private const string UpdateExistingRegex = @"^Update-existing\s(.*)";
      private const string RcsDiffRegex = @"^Rcs-diff\s(.*)";
      private const string ModeRegex = @"^Mode\s(.*)";
      private const string ModTimeRegex = @"^Mod-time\s(.*)";
      private const string TemplateRegex = @"^Template\s(.*)";
      private const string NotifiedRegex = @"^Notified\s(.*)";
      private const string ModuleExpansionRegex = @"^Module-expansion\s(.*)";
      private const string MbinaryRegex = @"^Mbinary\s(.*)";
      private const string EMessageRegex = @"^E\s(.*)";
      private const string FMessageRegex = @"^F\s(.*)";
      private const string WrapperRscOptionRegex = @"^Wrapper-rcsOption\s(.*)";

      public static readonly string[] ResponseNames;
      private const string AuthName = "";
      private const string OkName = "ok";
      private const string ErrorName = "error";
      private const string MessageName = "M";
      private const string ValidRequestsName = "Valid-requests";
      private const string CheckedInName = "Checked-in";
      private const string NewEntryName = "New-entry";
      private const string UpdatedName = "Updated";
      private const string MergedName = "Merged";
      private const string PatchedName = "Patched";
      private const string ChecksumName = "Checksum";
      private const string CopyFileName = "Copy-file";
      private const string RemovedName = "Removed";
      private const string RemoveEntryName = "Remove-entry";
      private const string SetStaticDirectoryName = "Set-static-directory";
      private const string ClearStaticDirectoryName = "Clear-static-directory";
      private const string SetStickyName = "Set-sticky";
      private const string ClearStickyName = "Clear-sticky";
      private const string CreatedName = "Created";
      private const string MessageTagName = "MT";
      private const string UpdateExistingName = "Update-existing";
      private const string RcsDiffName = "Rcs-diff";
      private const string ModeName = "Mode";
      private const string ModTimeName = "Mod-time";
      private const string TemplateName = "Template";
      private const string NotifiedName = "Notified";
      private const string ModuleExpansionName = "Module-expansion";
      private const string MbinaryName = "Mbinary";
      private const string EMessageName = "E";
      private const string FMessageName = "F";
      private const string WrapperRscOptionName = "Wrapper-rcsOption";

      private const string ResponseXMLSchema =
         @"<xsd:schema 
   attributeFormDefault=""unqualified"" 
   elementFormDefault=""qualified"" 
   version=""1.0"" 
   xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">

   <xsd:element name=""Responses"" type=""ResponsesType"" />
   <xsd:complexType name=""ResponsesType"">
      <xsd:sequence>
         <xsd:element maxOccurs=""unbounded"" name=""Response"" type=""ResponseType"" />
      </xsd:sequence>
   </xsd:complexType>
   <xsd:complexType name=""ResponseType"">
      <xsd:sequence>
         <xsd:element name=""Name"" type=""xsd:string"" />
         <xsd:element name=""ResponseType"" type=""xsd:int"" />
         <xsd:element name=""ProcessLines"" type=""ProcessLinesType"" />
         <xsd:element name=""ResponseFile"" type=""ResponseFileType"" maxOccurs=""1"" minOccurs=""0"" />
      </xsd:sequence>
   </xsd:complexType>
   <xsd:complexType name=""ResponseFileType"">
      <xsd:sequence>
         <xsd:element name=""Length"" type=""xsd:int"" />
         <xsd:element name=""Contents"" type=""xsd:string"" />
      </xsd:sequence>
   </xsd:complexType>
   <xsd:complexType name=""ProcessLinesType"">
      <xsd:sequence>
         <xsd:element maxOccurs=""unbounded"" name=""Line"" type=""xsd:string"" />
      </xsd:sequence>
   </xsd:complexType>
</xsd:schema>";
      static ResponseHelper()
      {
         ResponsePatterns = new []
         {
            AuthRegex,
            OkRegex,
            ErrorRegex,
            MessageRegex,
            ValidRequestsRegex,
            CheckedInRegex,
            NewEntryRegex,
            UpdatedRegex,
            MergedRegex,
            PatchedRegex,
            ChecksumRegex,
            CopyFileRegex,
            RemovedRegex,
            RemoveEntryRegex,
            SetStaticDirectoryRegex,
            ClearStaticDirectoryRegex,
            SetStickyRegex,
            ClearStickyRegex,
            CreatedRegex,
            MessageTagRegex,
            UpdateExistingRegex,
            RcsDiffRegex,
            ModeRegex,
            ModTimeRegex,
            TemplateRegex,
            NotifiedRegex,
            ModuleExpansionRegex,
            MbinaryRegex,
            EMessageRegex,
            FMessageRegex,
            WrapperRscOptionRegex
         };

         ResponseNames = new []
         {
            AuthName,
            OkName,
            ErrorName,
            MessageName,
            ValidRequestsName,
            CheckedInName,
            NewEntryName,
            UpdatedName,
            MergedName,
            PatchedName,
            ChecksumName,
            CopyFileName,
            RemovedName,
            RemoveEntryName,
            SetStaticDirectoryName,
            ClearStaticDirectoryName,
            SetStickyName,
            ClearStickyName,
            CreatedName,
            MessageTagName,
            UpdateExistingName,
            RcsDiffName,
            ModeName,
            ModTimeName,
            TemplateName,
            NotifiedName,
            ModuleExpansionName,
            MbinaryName,
            EMessageName,
            FMessageName,
            WrapperRscOptionName
         };
      }

      public static string GetFileNameFromUpdatedLine(string line)
      {
         string regex = @"^/(.+?)/";
         Match m = Regex.Match(line, regex);
         if (!m.Success)
            throw new ArgumentException("Unexpected response string format");
         return m.Groups[1].ToString();
      }

      public static string GetRevisionFromUpdatedLine(string line)
      {
         string regex = @"/(\d.+?)/";
         Match m = Regex.Match(line, regex);
         if (!m.Success)
            throw new ArgumentException("Unexpected response string format");
         return m.Groups[1].ToString();
      }

      public static string GetValidResponsesString(ResponseType[] validResponses)
      {
         IEnumerable<string> responses = validResponses.Select(vr => ResponseNames[(int) vr]);
         string resString = String.Join(" ", responses.ToArray());
         return resString;
      }

      public static string FileContentsToByteArrayString(byte[] fileContents)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(fileContents[0]);
         for (int i = 1; i < fileContents.Length; i++)
         {
            sb.Append(",").Append(fileContents[i]);
         }
         return sb.ToString();
      }

      public static bool ValidateResponseXML(XDocument response)
      {
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add("", XmlReader.Create(new StringReader(ResponseXMLSchema)));
         bool isValid = true;
         response.Validate(schemas, (o, e) =>
                                       {
                                          isValid = false;
                                       });
         return isValid;
      }

      public static IList<IResponse> ResponsesFromXML(XDocument xdoc)
      {
         IList<IResponse> responses = new List<IResponse>();
         IEnumerable<XElement> responseElements = xdoc.Element("Responses").Elements("Response");
         foreach (XElement element in responseElements)
         {
            IResponse response = ResponseFromXElement(element);
            responses.Add(response);
         }
         return responses;
      }

      public static IResponse ResponseFromXElement(XElement responseElement)
      {
         ResponseType rtype = (ResponseType)Convert.ToInt32(responseElement.Element("ResponseType").Value);
         ResponseFactory factory = new ResponseFactory();
         IResponse response = factory.CreateResponse(rtype);
         IList<string> lines = new List<string>();
         XElement linesElement = responseElement.Descendants("ProcessLines").First();
         foreach (XElement lineElement in linesElement.Elements())
         {
            lines.Add(lineElement.Value);
         }
         response.ProcessResponse(lines);
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse) response;
            XElement fileElement = responseElement.Descendants("ResponseFile").First();
            long len = Convert.ToInt64(fileElement.Element("Length").Value);
            string byteString = fileElement.Element("Contents").Value;
            byte[] buffer = new byte[len];
            string[] bytes = byteString.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < bytes.Length; i++)
            {
               buffer[i] = Convert.ToByte(bytes[i]);
            }
            fileResponse.File.Length = len;
            fileResponse.File.Contents = buffer;
         }
         return response;
      }
   }
}
