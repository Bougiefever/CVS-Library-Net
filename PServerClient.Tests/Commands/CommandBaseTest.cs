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
   /// <summary>
   /// Tests the CommandBase abstract class
   /// </summary>
   [TestFixture]
   public class CommandBaseTest
   {
      private MockRepository _mocks;
      private IConnection _connection;
      private IRoot _root;

      /// <summary>
      /// Sets up mocks
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      /// <summary>
      /// Sets up cvs root with test info
      /// </summary>
      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      /// <summary>
      /// Test for AllRequestsAreValid 
      /// </summary>
      [Test]
      public void AllRequestsValidTest()
      {
         IRequest r1 = new AddRequest();
         IRequest r2 = new CheckOutRequest();
         IRequest r3 = new ArgumentRequest("blah");

         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         cmd.ValidRequestTypes = new List<RequestType> { RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update };
         cmd.Requests = new List<IRequest> { r1, r2, r3 };

         bool result = cmd.AllRequestsAreValid();
         Assert.IsTrue(result);
         IRequest r4 = new UpdatePatchesRequest();
         cmd.Requests.Add(r4);
         Assert.IsFalse(cmd.AllRequestsAreValid());
      }

      /// <summary>
      /// Test for cvs authentication when authentication succeeds
      /// </summary>
      [Test]
      public void AuthenticateTest()
      {
         VerifyAuthCommand cmd = new VerifyAuthCommand(_root, _connection);
         IAuthResponse authResponse = new AuthResponse();
         authResponse.Lines = new List<string> { "I LOVE YOU" };

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

      /// <summary>
      /// Test for cvs authentication when authentication fails
      /// </summary>
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

      /// <summary>
      /// Test for cvs authentication when authentication produces an error
      /// </summary>
      [Test]
      public void AuthErrorTest()
      {
         VerifyAuthCommand cmd = new VerifyAuthCommand(_root, _connection);
         IResponse res1 = new EMessageResponse();
         res1.Initialize(new List<string> { "Fatal error, aborting." });
         IResponse res2 = new ErrorResponse();
         res2.Initialize(new List<string> { "0 no-such-user: no such user" });

         Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
         Expect.Call(() => _connection.Close());
         Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
         Expect.Call(_connection.GetResponse()).Return(res1);
         Expect.Call(_connection.GetAllResponses()).Return(new List<IResponse> { res2 });

         _mocks.ReplayAll();

         cmd.Execute();

         _mocks.VerifyAll();
         Assert.IsTrue(res1.Processed, "E Message response not processed");
         Assert.IsTrue(res2.Processed, "error message response not processed");
         Assert.AreEqual(AuthStatus.Error, cmd.AuthStatus);
         Assert.AreEqual(2, cmd.UserMessages.Count, "Wrong number of messages");
      }

      /// <summary>
      /// Test of the command base execute method
      /// </summary>
      [Test]
      public void CommandBaseExecuteTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IAuthResponse authResponse = new AuthResponse();
         IList<string> lines = new List<string> { "I LOVE YOU" };
         authResponse.Initialize(lines);
         authResponse.Process();

         IRequest validRequestsRequest = new ValidRequestsRequest();
         ValidRequestsResponse validRequestsResponse = new ValidRequestsResponse();
         IList<IResponse> otherResponses = new List<IResponse> { validRequestsResponse };
         lines = new List<string> { "Root Valid-responses valid-requests Repository Directory Max-dotdot Static-directory Sticky Entry Kopt Checkin-time Modified Is-modified Empty-conflicts UseUnchanged Unchanged Notify Questionable Argument Argumentx Global_option Gzip-stream wrapper-sendme-rcsOptions Set expand-modules ci co update diff log rlog add remove update-patches gzip-file-contents status rdiff tag rtag import admin export history release watch-on watch-off watch-add watch-remove watchers editors init annotate rannotate noop version" };
         validRequestsResponse.Initialize(lines);
         validRequestsResponse.Process();

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         command.RequiredRequests = new List<IRequest> { (IRequest) authRequest, validRequestsRequest };

         IRequest otherRequest = new AddRequest();

         command.Requests = new List<IRequest> { otherRequest };

         Expect.Call(() => _connection.Connect(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
         Expect.Call(_connection.GetResponse()).Return(authResponse);
         Expect.Call(_connection.GetAllResponses()).Return(otherResponses);
         Expect.Call(() => _connection.DoRequest(otherRequest)).Repeat.Once();
         Expect.Call(() => _connection.Close()).Repeat.Once();

         _mocks.ReplayAll();
         command.Execute();
         _mocks.VerifyAll();
      }

      /// <summary>
      /// Test of the command base execute method when authentication has failed
      /// </summary>
      [Test]
      public void CommandBaseExecuteWhenAuthFailedTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IAuthResponse authResponse = new AuthResponse();
         IList<string> lines = new List<string> { "I HATE YOU" };
         authResponse.Initialize(lines);
         authResponse.Process();
         IRequest otherRequest = _mocks.DynamicMock<IRequest>();

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         
         command.RequiredRequests = new List<IRequest> { authRequest, otherRequest };
         Expect.Call(() => _connection.Connect(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(_connection.GetResponse()).Return(authResponse);
         Expect.Call(() => _connection.DoRequest(otherRequest)).Repeat.Never();
         Expect.Call(_connection.Close).Repeat.Once();
         _mocks.ReplayAll();
         command.Execute();
         _mocks.VerifyAll();
      }

      /// <summary>
      /// Tests that the ExecuteRequiredRequests populates the ValidRequestTypes list
      /// </summary>
      [Test]
      public void ExectuteRequiredRequestsPopulatesValidRequestTypesTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IAuthResponse authResponse = new AuthResponse();
         IList<string> lines = new List<string> { "I LOVE YOU" };
         authResponse.Initialize(lines);
         authResponse.Process();

         IRequest validRequestsRequest = new ValidRequestsRequest();
         ValidRequestsResponse validRequestsResponse = new ValidRequestsResponse();
         IList<IResponse> otherResponses = new List<IResponse> { validRequestsResponse };
         lines = new List<string> { "Root Valid-responses valid-requests Repository Directory Max-dotdot" };
         validRequestsResponse.Initialize(lines);
         validRequestsResponse.Process();

         cmd.RequiredRequests = new List<IRequest> { (IRequest) authRequest, validRequestsRequest };

         Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
         Expect.Call(_connection.GetResponse()).Return(authResponse);
         Expect.Call(() => _connection.DoRequest(validRequestsRequest)).Repeat.Once();
         Expect.Call(_connection.GetAllResponses()).Return(otherResponses);
         Expect.Call(authRequest.Type).Return(RequestType.Auth);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();

         Assert.AreEqual(ExitCode.Succeeded, result);
         Assert.AreEqual(6, cmd.ValidRequestTypes.Count);
      }

      /// <summary>
      /// Tests that the ExecuteRequiredRequests has ExitCode of fail when authentication fails
      /// </summary>
      [Test]
      public void ExecuteRequiredRequestsAuthFailExitCodeFailTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IAuthResponse authResponse = new AuthResponse();
         IList<string> lines = new List<string> { "I HATE YOU" };
         authResponse.Initialize(lines);
         authResponse.Process();

         cmd.RequiredRequests = new List<IRequest> { authRequest };
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(_connection.GetResponse()).Return(authResponse);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();
         Assert.AreEqual(ExitCode.Failed, result);
      }

      /// <summary>
      /// Tests that ExecuteRequiredRequestshas ExitCode of fail when errors are in the responses
      /// </summary>
      [Test]
      public void ExecuteRequiredRequestsWhenErrorResponseTest()
      {
         ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IAuthResponse authResponse = new AuthResponse();
         IList<string> lines = new List<string> { "I LOVE YOU" };
         authResponse.Initialize(lines);
         authResponse.Process(); 

         IRequest otherRequest = _mocks.DynamicMock<IRequest>();
         IResponse error = new ErrorResponse();
         lines = new List<string> { "error message" };
         error.Initialize(lines);
         IList<IResponse> errorResponse = new List<IResponse> { error };

         cmd.RequiredRequests = new List<IRequest> { (IRequest) authRequest, otherRequest };

         Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
         Expect.Call(authRequest.Type).Return(RequestType.Auth);
         Expect.Call(_connection.GetResponse()).Return(authResponse);
         Expect.Call(otherRequest.ResponseExpected).Return(true);
         Expect.Call(() => _connection.DoRequest(otherRequest)).Repeat.Once();
         Expect.Call(_connection.GetAllResponses()).Return(errorResponse);

         _mocks.ReplayAll();
         ExitCode result = cmd.ExecuteRequiredRequests();
         _mocks.VerifyAll();

         Assert.AreEqual(ExitCode.Failed, result);
      }
   }
}