using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PServerClient
{
   public static class ResponseHelper
   {
      #region Response cvs name

      private const string NameAuth = "";
      private const string NameCheckedIn = "Checked-in";
      private const string NameChecksum = "Checksum";
      private const string NameClearStaticDirectory = "Clear-static-directory";
      private const string NameClearSticky = "Clear-sticky";
      private const string NameCopyFile = "Copy-file";
      private const string NameCreated = "Created";
      private const string NameEMessage = "E";
      private const string NameError = "error";
      private const string NameFlush = "F";
      private const string NameMbinary = "Mbinary";
      private const string NameMerged = "Merged";
      private const string NameMessage = "M";
      private const string NameMessageTag = "MT";
      private const string NameMode = "Mode";
      private const string NameModTime = "Mod-time";
      private const string NameModuleExpansion = "Module-expansion";
      private const string NameNewEntry = "New-entry";
      private const string NameNotified = "Notified";
      private const string NameOk = "ok";
      private const string NamePatched = "Patched";
      private const string NameRcsDiff = "Rcs-diff";
      private const string NameRemoved = "Removed";
      private const string NameRemoveEntry = "Remove-entry";
      private const string NameSetStaticDirectory = "Set-static-directory";
      private const string NameSetSticky = "Set-sticky";
      private const string NameTemplate = "Template";
      private const string NameUpdated = "Updated";
      private const string NameUpdateExisting = "Update-existing";
      private const string NameValidRequests = "Valid-requests";
      private const string NameWrapperRscOption = "Wrapper-rcsOption";

      #endregion

      #region Regular expressions pattern to match response name

      private const string RegexAuth = @"(I LOVE YOU|I HATE YOU)(.*)";
      private const string RegexCheckedIn = @"^Checked-in\s(.*)";
      private const string RegexChecksum = @"^Checksum\s(.*)";
      private const string RegexClearStaticDirectory = @"^Clear-static-directory\s(.*)";
      private const string RegexClearSticky = @"^Clear-sticky\s(.*)";
      private const string RegexCopyFile = @"^Copy-file\s(.*)";
      private const string RegexCreated = @"^Created\s(.*)";
      private const string RegexEMessage = @"^E\s(.*)";
      private const string RegexError = @"^error\s(.*)";
      private const string RegexFlush = @"^F\s(.*)";
      private const string RegexMbinary = @"^Mbinary\s(.*)";
      private const string RegexMerged = @"^Merged\s(.*)";
      private const string RegexMessage = @"^M\s(.*)";
      private const string RegexMessageTag = @"^MT\s(.*)";
      private const string RegexMode = @"^Mode\s(.*)";
      private const string RegexModTime = @"^Mod-time\s(.*)";
      private const string RegexModuleExpansion = @"^Module-expansion\s(.*)";
      private const string RegexNewEntry = @"^New-entry\s(.*)";
      private const string RegexNotified = @"^Notified\s(.*)";
      private const string RegexOk = @"^ok(.*)";
      private const string RegexPatched = @"^Patched\s(.*)";
      private const string RegexRcsDiff = @"^Rcs-diff\s(.*)";
      private const string RegexRemoved = @"^Removed\s(.*)";
      private const string RegexRemoveEntry = @"^Remove-entry\s(.*)";
      private const string RegexSetStaticDirectory = @"^Set-static-directory\s(.*)";
      private const string RegexSetSticky = @"^Set-sticky\s(.*)";
      private const string RegexTemplate = @"^Template\s(.*)";
      private const string RegexUpdated = @"^Updated\s(.*)";
      private const string RegexUpdateExisting = @"^Update-existing\s(.*)";
      private const string RegexValidRequests = @"^Valid-requests\s(.*)";
      private const string RegexWrapperRscOption = @"^Wrapper-rcsOption\s(.*)";

      #endregion

      public static readonly string[] ResponseNames;
      public static readonly string[] ResponsePatterns;

      static ResponseHelper()
      {
         ResponsePatterns = new[]
                               {
                                  RegexAuth,
                                  RegexCheckedIn,
                                  RegexChecksum,
                                  RegexClearStaticDirectory,
                                  RegexClearSticky,
                                  RegexCopyFile,
                                  RegexCreated,
                                  RegexEMessage,
                                  RegexError,
                                  RegexFlush,
                                  RegexMbinary,
                                  RegexMerged,
                                  RegexMessage,
                                  RegexMessageTag,
                                  RegexMode,
                                  RegexModTime,
                                  RegexModuleExpansion,
                                  RegexNewEntry,
                                  RegexNotified,
                                  RegexOk,
                                  RegexPatched,
                                  RegexRcsDiff,
                                  RegexRemoved,
                                  RegexRemoveEntry,
                                  RegexSetStaticDirectory,
                                  RegexSetSticky,
                                  RegexTemplate,
                                  RegexUpdated,
                                  RegexUpdateExisting,
                                  RegexValidRequests,
                                  RegexWrapperRscOption
                               };

         ResponseNames = new[]
                            {
                                  NameAuth,
                                  NameCheckedIn,
                                  NameChecksum,
                                  NameClearStaticDirectory,
                                  NameClearSticky,
                                  NameCopyFile,
                                  NameCreated,
                                  NameEMessage,
                                  NameError,
                                  NameFlush,
                                  NameMbinary,
                                  NameMerged,
                                  NameMessage,
                                  NameMessageTag,
                                  NameMode,
                                  NameModTime,
                                  NameModuleExpansion,
                                  NameNewEntry,
                                  NameNotified,
                                  NameOk,
                                  NamePatched,
                                  NameRcsDiff,
                                  NameRemoved,
                                  NameRemoveEntry,
                                  NameSetStaticDirectory,
                                  NameSetSticky,
                                  NameTemplate,
                                  NameUpdated,
                                  NameUpdateExisting,
                                  NameValidRequests,
                                  NameWrapperRscOption
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
   }
}