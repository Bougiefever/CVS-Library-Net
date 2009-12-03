using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Connection;

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
      private const string ErrorName = "error ";
      private const string MessageName = "M ";
      private const string ValidRequestsName = "Valid-requests ";
      private const string CheckedInName = "Checked-in ";
      private const string NewEntryName = "New-entry ";
      private const string UpdatedName = "Updated ";
      private const string MergedName = "Merged ";
      private const string PatchedName = "Patched ";
      private const string ChecksumName = "Checksum ";
      private const string CopyFileName = "Copy-file ";
      private const string RemovedName = "Removed ";
      private const string RemoveEntryName = "Remove-entry ";
      private const string SetStaticDirectoryName = "Set-static-directory ";
      private const string ClearStaticDirectoryName = "Clear-static-directory ";
      private const string SetStickyName = "Set-sticky ";
      private const string ClearStickyName = "Clear-sticky ";
      private const string CreatedName = "Created ";
      private const string MessageTagName = "MT ";
      private const string UpdateExistingName = "Update-existing ";
      private const string RcsDiffName = "Rcs-diff ";
      private const string ModeName = "Mode ";
      private const string ModTimeName = "Mod-time ";
      private const string TemplateName = "Template ";
      private const string NotifiedName = "Notified ";
      private const string ModuleExpansionName = "Module-expansion ";
      private const string MbinaryName = "Mbinary ";
      private const string EMessageName = "E ";
      private const string FMessageName = "F ";
      private const string WrapperRscOptionName = "Wrapper-rcsOption";

      static ResponseHelper()
      {
         ResponsePatterns = new string[31]
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

         ResponseNames = new string[31]
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


   }
}
