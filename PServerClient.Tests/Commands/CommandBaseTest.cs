using System.Collections.Generic;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class CommandBaseTest
   {
      private MockRepository _mocks;
      private IConnection _connection;
      private CvsRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3");
      }

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      [Test]
      public void CommandBaseExecuteTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = _mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse> { authResponse };

         IRequest vrReq = new ValidRequestsRequest();
         ValidRequestResponse vrRes = new ValidRequestResponse();
         IList<IResponse> otherResponses = new List<IResponse> { vrRes };
         vrRes.ValidRequestTypes = new List<RequestType> { RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update };

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         command.Connection = _connection;
         command.RequiredRequests = new List<IRequest> { (IRequest)authRequest, vrReq };

         IRequest otherRequest = new AddRequest();

         command.Requests = new List<IRequest> { otherRequest };

         Expect.Call(() => _connection.Connect(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(_connection.DoRequest(authRequest)).Return(authResponses).Repeat.Once();
         Expect.Call(_connection.DoRequest(vrReq)).Return(otherResponses).Repeat.Once();
         Expect.Call(authRequest.Responses).PropertyBehavior();
         Expect.Call(authRequest.Status).Return(AuthStatus.Authenticated);
         Expect.Call(authRequest.RequestType).Return(RequestType.Auth);
         Expect.Call(_connection.DoRequest(otherRequest)).Return(new List<IResponse>());
         Expect.Call(_connection.Close).Repeat.Once();

         _mocks.ReplayAll();
         command.Execute();
         _mocks.VerifyAll();
      }

      [Test]
      public void CommandBaseExecuteWhenAuthFailedTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IRequest otherRequest = _mocks.DynamicMock<IRequest>();

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         command.Connection = _connection;
         command.RequiredRequests = new List<IRequest> { authRequest, otherRequest };
         Expect.Call(() => _connection.Connect(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(_connection.Close).Repeat.Once();
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(authRequest.Status).Return(AuthStatus.NotAuthenticated);
         Expect.Call(_connection.DoRequest(otherRequest)).Repeat.Never();
         _mocks.ReplayAll();
         command.Execute();
         _mocks.VerifyAll();
      }

      [Test]
      public void AllRequestsValidTest()
      {
         IRequest r1 = new AddRequest();
         IRequest r2 = new CheckOutRequest();
         IRequest r3 = new ArgumentRequest("blah");

         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root);
         cmd.ValidRequestTypes = new List<RequestType> { RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update };
         cmd.Requests = new List<IRequest> {r1, r2, r3};

         bool result = cmd.AllRequestsAreValid();
         Assert.IsTrue(result);
         IRequest r4 = new UpdatePatchesRequest();
         cmd.Requests.Add(r4);
         Assert.IsFalse(cmd.AllRequestsAreValid());
      }

      [Test]
      public void ExecuteRequiredRequestsAuthFailExitCodeFailTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root);
         cmd.Connection = _connection;
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         cmd.RequiredRequests = new List<IRequest> {authRequest};
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(authRequest.Status).Return(AuthStatus.NotAuthenticated);
         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();
         Assert.AreEqual(ExitCode.Failed, result);
      }

      [Test]
      public void ExectuteRequiredRequestsPopulatesValidRequestTypesTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root);
         cmd.Connection = _connection;
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = _mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse> { authResponse };

         IRequest vrReq = new ValidRequestsRequest();
         ValidRequestResponse vrRes = new ValidRequestResponse();
         IList<IResponse> otherResponses = new List<IResponse> {vrRes};
         vrRes.ValidRequestTypes = new List<RequestType> { RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update };

         cmd.RequiredRequests = new List<IRequest> {(IRequest) authRequest, vrReq};

         Expect.Call(_connection.DoRequest(authRequest)).Return(authResponses).Repeat.Once();
         Expect.Call(_connection.DoRequest(vrReq)).Return(otherResponses).Repeat.Once();
         Expect.Call(authRequest.Responses).PropertyBehavior();
         Expect.Call(authRequest.Status).Return(AuthStatus.Authenticated);
         Expect.Call(authRequest.RequestType).Return(RequestType.Auth);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();

         Assert.AreEqual(ExitCode.Succeeded, result);
         Assert.AreEqual(6, cmd.ValidRequestTypes.Count);
      }

      [Test]
      public void ExecuteRequiredRequestsFailsTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root);
         cmd.Connection = _connection;
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = _mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse> { authResponse };

         IRequest otherRequest = _mocks.DynamicMock<IRequest>();
         IResponse error = new ErrorResponse();
         IList<IResponse> errorResponse = new List<IResponse> {error};


         cmd.RequiredRequests = new List<IRequest> { (IRequest)authRequest, otherRequest };

         Expect.Call(_connection.DoRequest(authRequest)).Return(authResponses).Repeat.Once();
         Expect.Call(authRequest.Responses).PropertyBehavior();
         Expect.Call(authRequest.Status).Return(AuthStatus.Authenticated);
         Expect.Call(authRequest.RequestType).Return(RequestType.Auth);
         Expect.Call(otherRequest.Responses).PropertyBehavior();
         Expect.Call(_connection.DoRequest(otherRequest)).Return(errorResponse);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();

         Assert.AreEqual(ExitCode.Failed, result);
      }
   }
}
