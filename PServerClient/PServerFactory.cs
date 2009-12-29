using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using PServerClient.Commands;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient
{
   public class PServerFactory
   {
      public IResponse CreateResponse(ResponseType type)
      {
         string responseName = GetResponseClassNameFromType(type);
         ObjectHandle handle = Activator.CreateInstance("PServerClient", responseName);
         IResponse response = (IResponse) handle.Unwrap();
         return response;
      }

      public IResponse CreateResponse(string className)
      {
         Type type = Type.GetType(className);
         IResponse response = (IResponse) Activator.CreateInstance(type);
         return response;
      }

      public ResponseType GetResponseType(string rawResponse)
      {
         ResponseType responseType = ResponseType.Null;
         for (int i = 0; i < ResponseHelper.ResponsePatterns.Length; i++)
         {
            Match m = Regex.Match(rawResponse, ResponseHelper.ResponsePatterns[i]);
            if (m.Success)
               responseType = (ResponseType)i;
         }
         return responseType;
      }

      public IRequest CreateRequest(RequestType requestType, object[] args)
      {
         string requestName = GetRequestClassNameFromType(requestType);
         Type type = Type.GetType(requestName);
         IRequest request = (IRequest) Activator.CreateInstance(type, args);
         return request;
      }

      public IRequest CreateRequest(string className, string[] lines)
      {
         Type type = Type.GetType(className);
         IRequest request = (IRequest)Activator.CreateInstance(type, new object[] { lines });
         return request;
      }

      public ICommand CreateCommand(string className, object[] args)
      {
         Type type = Type.GetType(className);
         ICommand cmd = (ICommand)Activator.CreateInstance(type, args);
         return cmd;
      }      
      
      private static string GetResponseClassNameFromType(ResponseType type)
      {
         string responseName = "PServerClient.Responses." + type + "Response";
         return responseName;
      }

      private static string GetRequestClassNameFromType(RequestType type)
      {
         string requestName = "PServerClient.Requests." + type + "Request";
         return requestName;
      }

      private static string GetCommandClassNameFromType(CommandType type)
      {
         string commandName = "PServerClient.Commands." + type + "Command";
         return commandName;
      }


   }
}