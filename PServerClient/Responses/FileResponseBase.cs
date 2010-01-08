using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class FileResponseBase : ResponseBase, IFileResponse
   {
      private string _name;
      private string _revision;
      private string _properties;

      public abstract override ResponseType Type { get; }

      public override int LineCount
      {
         get
         {
            return 5;
         }
      }

      public string Module { get; set; }

      public string RepositoryPath { get; set; }

      public string EntryLine { get; set; }

      public string Name
      {
         get
         {
            return _name;
         }
      }

      public string Revision
      {
         get
         {
            return _revision;
         }
      }

      public string Properties
      {
         get
         {
            return _properties;
         }
      }

      public long Length { get; set; }

      public byte[] Contents { get; set; }

      public FileType FileType { get; set; }

      public override void Initialize(IList<string> lines)
      {
         string module = lines[0];
         string repositoryPath = lines[1];
         string entryLine = lines[2];
         string fileProperties = lines[3];
         string fileLength = lines[4];

         _name = ResponseHelper.GetFileNameFromEntryLine(entryLine);
         _revision = ResponseHelper.GetRevisionFromEntryLine(entryLine);
         _properties = fileProperties;
         Length = Convert.ToInt64(fileLength);
         FileType = FileType.Text;
         Lines = new List<string>(LineCount);
         for (int i = 0; i < LineCount; i++)
            Lines.Add(lines[i]);
         Module = module;
         RepositoryPath = repositoryPath;
         EntryLine = entryLine;
      }

      public override string Display()
      {
         return RepositoryPath;
      }
   }
}