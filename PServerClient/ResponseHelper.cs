using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PServerClient.Responses;

namespace PServerClient
{
   public static class ResponseHelper
   {
      public static readonly string[] ResponseNames;

      public static readonly string[] ResponsePatterns;

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
      private const string NameMTMessage = "MT";
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
      private const string NameNull = "";

      #endregion

      #region Regular expressions pattern to match response name

      private const string RegexAuth = @"(?<data>I LOVE YOU|I HATE YOU).*";
      private const string RegexCheckedIn = @"^Checked-in\s(?<data>.*)";
      private const string RegexChecksum = @"^Checksum\s(?<data>.*)";
      private const string RegexClearStaticDirectory = @"^Clear-static-directory\s(?<data>.*)";
      private const string RegexClearSticky = @"^Clear-sticky\s(?<data>.*)";
      private const string RegexCopyFile = @"^Copy-file\s(?<data>.*)";
      private const string RegexCreated = @"^Created\s(?<data>.*)";
      private const string RegexEMessage = @"^E\s(?<data>.*)";
      private const string RegexError = @"^error\s(?<data>.*)";
      private const string RegexFlush = @"^F\s(?<data>.*)";
      private const string RegexMbinary = @"^Mbinary\s(?<data>.*)";
      private const string RegexMerged = @"^Merged\s(?<data>.*)";
      private const string RegexMessage = @"^M\s(?<data>.*)";
      private const string RegexMTMessage = @"^MT\s(?<data>.*)";
      private const string RegexMode = @"^Mode\s(?<data>.*)";
      private const string RegexModTime = @"^Mod-time\s(?<data>.*)";
      private const string RegexModuleExpansion = @"^Module-expansion\s(?<data>.*)";
      private const string RegexNewEntry = @"^New-entry\s(?<data>.*)";
      private const string RegexNotified = @"^Notified\s(?<data>.*)";
      private const string RegexOk = @"^ok(?<data>.*)";
      private const string RegexPatched = @"^Patched\s(?<data>.*)";
      private const string RegexRcsDiff = @"^Rcs-diff\s(?<data>.*)";
      private const string RegexRemoved = @"^Removed\s(?<data>.*)";
      private const string RegexRemoveEntry = @"^Remove-entry\s(?<data>.*)";
      private const string RegexSetStaticDirectory = @"^Set-static-directory\s(?<data>.*)";
      private const string RegexSetSticky = @"^Set-sticky\s(?<data>.*)";
      private const string RegexTemplate = @"^Template\s(?<data>.*)";
      private const string RegexUpdated = @"^Updated\s(?<data>.*)";
      private const string RegexUpdateExisting = @"^Update-existing\s(?<data>.*)";
      private const string RegexValidRequests = @"^Valid-requests\s(?<data>.*)";
      private const string RegexWrapperRscOption = @"^Wrapper-rcsOption\s(?<data>.*)";
      private const string RegexNull = @"(?<data>.*)"; // matches anything

      #endregion

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
                                  RegexMTMessage,
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
                                  RegexWrapperRscOption,
                                  RegexNull
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
                               NameMTMessage,
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
                               NameWrapperRscOption,
                               NameNull
                            };
      }

      public static string GetFileNameFromEntryLine(string line)
      {
         string regex = @"^/(.+?)/";
         Match m = Regex.Match(line, regex);
         if (!m.Success)
            throw new ArgumentException("Unexpected response string format");
         return m.Groups[1].ToString();
      }

      public static string FixResponseModuleSlashes(string module)
      {
         string newModule = module;
         if (module.Substring(module.Length - 1, 1) == "/")
            newModule = module.Substring(0, module.Length - 1);
         if (module.Substring(0, 1) == "/")
            newModule = newModule.Substring(1);
         return newModule;
      }

      public static string GetRevisionFromEntryLine(string line)
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

      public static IList<IResponse> CollapseMessagesInResponses(IList<IResponse> responses)
      {
         IList<IResponse> condensed = new List<IResponse>();
         Type type = null;
         IMessageResponse firstMessage = null;
         for (int i = 0; i < responses.Count; i++)
         {
            IResponse response = responses[i];
            if (type == response.GetType() && response is IMessageResponse)
            {
               // add the lines to the first message response in group
               if (firstMessage != null) firstMessage.Lines.Add(response.Lines[0]);
            }
            else
            {
               condensed.Add(response);
               if (response is IMessageResponse)
                  firstMessage = (IMessageResponse) response;
            }

            type = response.GetType();
         }

         return condensed;
      }

      public static string GetLastModuleName(string module)
      {
         string mod = FixResponseModuleSlashes(module);
         string name;
         if (mod.LastIndexOf("/") > 0)
            name = mod.Substring(mod.LastIndexOf("/") + 1);
         else
            name = mod;
         return name;
      }
   }
}