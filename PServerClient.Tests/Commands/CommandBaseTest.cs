using System.Collections.Generic;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class CommandBaseTest
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      #endregion

      private MockRepository _mocks;
      private IConnection _connection;
      private IRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      [Test]
      public void AllRequestsValidTest()
      {
         IRequest r1 = new AddRequest();
         IRequest r2 = new CheckOutRequest();
         IRequest r3 = new ArgumentRequest("blah");

         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         cmd.ValidRequestTypes = new List<RequestType> {RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update};
         cmd.Requests = new List<IRequest> {r1, r2, r3};

         bool result = cmd.AllRequestsAreValid();
         Assert.IsTrue(result);
         IRequest r4 = new UpdatePatchesRequest();
         cmd.Requests.Add(r4);
         Assert.IsFalse(cmd.AllRequestsAreValid());
      }

      [Test]
      public void AuthenticateTest()
      {
         VerifyAuthCommand cmd = new VerifyAuthCommand(_root, _connection);
         IAuthResponse authResponse = new AuthResponse();
         authResponse.Lines = new List<string> {"I LOVE YOU"};

         Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
         Expect.Call(() => _connection.Close());
         Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
         Expect.Call(_connection.GetResponse()).Return(authResponse);

         _mocks.ReplayAll();

         cmd.Execute();

         _mocks.VerifyAll();
         Assert.IsTrue(authResponse.Processed, "Auth response was not processed");
         Assert.AreEqual(AuthStatus.Authenticated, cmd.AuthStatus);
      }

      [Test]
      public void NotAuthenticatedTest()
      {
         VerifyAuthCommand cmd = new VerifyAuthCommand(_root, _connection);
         IAuthResponse authResponse = new AuthResponse();
         authResponse.Lines = new List<string> { "I HATE YOU" };

         Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
         Expect.Call(() => _connection.Close());
         Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
         Expect.Call(_connection.GetResponse()).Return(authResponse);

         _mocks.ReplayAll();

         cmd.Execute();

         _mocks.VerifyAll();
         Assert.IsTrue(authResponse.Processed, "Auth response was not processed");
         Assert.AreEqual(AuthStatus.NotAuthenticated, cmd.AuthStatus);
      }

      [Test]
      public void AuthErrorTest()
      {
         VerifyAuthCommand cmd = new VerifyAuthCommand(_root, _connection);
         IResponse res1 = new EMessageResponse();
         res1.Initialize(new List<string>{"Fatal error, aborting."});
         IResponse res2 = new ErrorResponse();
         res2.Initialize(new List<string> { "0 no-such-user: no such user" });

         Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
         Expect.Call(() => _connection.Close());
         Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
         Expect.Call(_connection.GetResponse()).Return(res1);
         Expect.Call(_connection.GetAllResponses()).Return(new List<IResponse> {res2});

         _mocks.ReplayAll();

         cmd.Execute();

         _mocks.VerifyAll();
         Assert.IsTrue(res1.Processed, "E Message response not processed");
         Assert.IsTrue(res2.Processed, "error message response not processed");
         Assert.AreEqual(AuthStatus.Error, cmd.AuthStatus);
         Assert.AreEqual(2, cmd.UserMessages.Count, "Wrong number of messages");
      }

      [Test][Ignore]
      public void CommandBaseExecuteTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = _mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse> {authResponse};

         IRequest vrReq = new ValidRequestsRequest();
         ValidRequestsResponse vrRes = new ValidRequestsResponse();
         IList<IResponse> otherResponses = new List<IResponse> {vrRes};
         vrRes.ValidRequestTypes = new List<RequestType> {RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update};

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         command.RequiredRequests = new List<IRequest> {(IRequest) authRequest, vrReq};

         IRequest otherRequest = new AddRequest();

         command.Requests = new List<IRequest> {otherRequest};

         Expect.Call(() => _connection.Connect(null))
            .IgnoreArguments()
            .Repeat.Once();
         //Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
         //Expect.Call(_connection.DoRequest(vrReq)).Return(otherResponses).Repeat.Once();
         //Expect.Call(command.Responses).PropertyBehavior();
         //xpect.Call(command.Status).Return(AuthStatus.Authenticated);
         //Expect.Call(authRequest.Type).Return(RequestType.Auth);
         //Expect.Call(_connection.DoRequest(otherRequest)).Return(new List<IResponse>());
         Expect.Call(_connection.Close).Repeat.Once();

         _mocks.ReplayAll();
         command.Execute();
         _mocks.VerifyAll();
      }

      [Test][Ignore]
      public void CommandBaseExecuteWhenAuthFailedTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IRequest otherRequest = _mocks.DynamicMock<IRequest>();

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         
         command.RequiredRequests = new List<IRequest> {authRequest, otherRequest};
         Expect.Call(() => _connection.Connect(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(_connection.Close).Repeat.Once();
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         //Expect.Call(authRequest.Status).Return(AuthStatus.NotAuthenticated);
         //Expect.Call(_connection.DoRequest(otherRequest)).Repeat.Never();
         _mocks.ReplayAll();
         command.Execute();
         _mocks.VerifyAll();
      }

      [Test][Ignore]
      public void ExectuteRequiredRequestsPopulatesValidRequestTypesTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = _mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse> {authResponse};

         IRequest vrReq = new ValidRequestsRequest();
         ValidRequestsResponse vrRes = new ValidRequestsResponse();
         IList<IResponse> otherResponses = new List<IResponse> {vrRes};
         vrRes.ValidRequestTypes = new List<RequestType> {RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update};

         cmd.RequiredRequests = new List<IRequest> {(IRequest) authRequest, vrReq};

         //Expect.Call(_connection.DoRequest(authRequest)).Return(authResponses).Repeat.Once();
         //Expect.Call(_connection.DoRequest(vrReq)).Return(otherResponses).Repeat.Once();
         //Expect.Call(authRequest.Responses).PropertyBehavior();
         //Expect.Call(authRequest.Status).Return(AuthStatus.Authenticated);
         Expect.Call(authRequest.Type).Return(RequestType.Auth);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();

         Assert.AreEqual(ExitCode.Succeeded, result);
         Assert.AreEqual(6, cmd.ValidRequestTypes.Count);
      }

      [Test][Ignore]
      public void ExecuteRequiredRequestsAuthFailExitCodeFailTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         cmd.RequiredRequests = new List<IRequest> {authRequest};
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         //Expect.Call(authRequest.Status).Return(AuthStatus.NotAuthenticated);
         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();
         Assert.AreEqual(ExitCode.Failed, result);
      }

      [Test][Ignore]
      public void ExecuteRequiredRequestsFailsTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = _mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse> {authResponse};

         IRequest otherRequest = _mocks.DynamicMock<IRequest>();
         IResponse error = new ErrorResponse();
         IList<IResponse> errorResponse = new List<IResponse> {error};


         cmd.RequiredRequests = new List<IRequest> {(IRequest) authRequest, otherRequest};

         //Expect.Call(_connection.DoRequest(authRequest)).Return(authResponses).Repeat.Once();
         //Expect.Call(authRequest.Responses).PropertyBehavior();
         //Expect.Call(authRequest.Status).Return(AuthStatus.Authenticated);
         //Expect.Call(authRequest.Type).Return(RequestType.Auth);
         //Expect.Call(otherRequest.Responses).PropertyBehavior();
         //Expect.Call(_connection.DoRequest(otherRequest)).Return(errorResponse);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();

         Assert.AreEqual(ExitCode.Failed, result);
      }

      [Test][Ignore]
      public void GetResponsesTest()
      {
         //create mock request/responses
         ValidRequestsRequest vrrequest = new ValidRequestsRequest();
         ValidRequestsResponse vrresponse = new ValidRequestsResponse();
         string process = "Root Valid-responses valid-requests Repository Directory";
         vrresponse.Initialize(new List<string> {process});
         //vrrequest.Responses = new List<IResponse> {vrresponse};

         CheckOutRequest corequest = new CheckOutRequest();
         IList<string> lines = new List<string> {"module/", "/f1/f2/f3/"};
         IResponse r1 = new ClearStickyResponse();
         r1.Initialize(lines);
         IResponse r2 = new ClearStaticDirectoryResponse();
         r2.Initialize(lines);
         //corequest.Responses = new List<IResponse> {r1, r2};

         CheckOutCommand cmd = new CheckOutCommand(_root, _connection);
         cmd.Requests = new List<IRequest> {vrrequest, corequest};
      }
   }
}