using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Connection;

namespace PServerClient
{
   public static class CreateResponseHelper
   {
      public static readonly string[] ResponsePatterns;
      private const string AuthRegex = @"(I LOVE YOU|I HATE YOU)\s(.*)";
      private const string OkRegex = @"^ok\s(.*)";
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

      static CreateResponseHelper()
      {
         ResponsePatterns = new string[30]
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
            FMessageRegex
         };
      }


   }
}
