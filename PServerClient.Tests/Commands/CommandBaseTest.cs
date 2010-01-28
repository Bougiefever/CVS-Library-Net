using System.Collections.Generic;
using System.Configuration;
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
      private CommandBase _cmdbase;

      /// <summary>
      /// Sets up mocks
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
         _cmdbase = _mocks.PartialMock<CommandBase>(new object[] { _root, _connection });
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
      /// Tests the ProcessResponse method.
      /// </summary>
      [Test]
      public void TestProcessResponse()
      {
         IResponse response = _mocks.DynamicMock<IResponse>();

         Expect.Call(response.Process);

         _mocks.ReplayAll();
         _cmdbase.ProcessResponse(response);
         _mocks.VerifyAll();
         if (PServerHelper.IsTestMode())
            Assert.AreEqual(1, _cmdbase.Items.Count);
         else
            Assert.AreEqual(0, _cmdbase.Items.Count);
      }

      /// <summary>
      /// Tests the ProcessResponse method with file-type response.
      /// </summary>
      [Test]
      public void TestProcessResponseWithFileResponse()
      {
         IFileResponse response = _mocks.DynamicMock<IFileResponse>();

         Expect.Call(response.Process);
         Expect.Call(() => _connection.GetFileResponseContents(null))
            .IgnoreArguments();

         _mocks.ReplayAll();
         _cmdbase.ProcessResponse(response);
         _mocks.VerifyAll();
         if (PServerHelper.IsTestMode())
            Assert.AreEqual(1, _cmdbase.Items.Count);
         else
            Assert.AreEqual(0, _cmdbase.Items.Count);
      }

      /// <summary>
      /// Tests the do request.
      /// </summary>
      [Test]
      public void TestDoRequest()
      {
         IRequest request = _mocks.DynamicMock<IRequest>();

         Expect.Call(() => _connection.DoRequest(request));

         _mocks.ReplayAll();
         _cmdbase.DoRequest(request);
         _mocks.VerifyAll();
         if (PServerHelper.IsTestMode())
            Assert.AreEqual(1, _cmdbase.Items.Count);
         else
            Assert.AreEqual(0, _cmdbase.Items.Count);
      }

      /// <summary>
      /// Tests the ProcessMessages.
      /// </summary>
      [Test]
      public void TestProcessMessages()
      {
         IMessageResponse mresponse = _mocks.DynamicMock<IMessageResponse>();
         IResponse response = _mocks.DynamicMock<IResponse>();

         _cmdbase.Responses = new List<IResponse> { mresponse, response };

         Expect.Call(mresponse.Process);
         Expect.Call(response.Process).Repeat.Never();
         Expect.Call(mresponse.Message).Return("my message");

         _mocks.ReplayAll();
         _cmdbase.ProcessMessages();
         _mocks.VerifyAll();

         Assert.AreEqual(1, _cmdbase.UserMessages.Count);
      }

      /// <summary>
      /// Tests the execute required requests when auth fails.
      /// </summary>
      [Test]
      public void TestExecuteRequiredRequestsWhenAuthFails()
      {
         IRequest r1 = new AuthRequest(_root);

         ////_cmdbase.RequiredRequests = 
      }

      /// <summary>
      /// Tests the remove processed responses.
      /// </summary>
      [Test]
      public void TestRemoveProcessedResponses()
      {
         IResponse r1 = new MTMessageResponse();
         IResponse r2 = new OkResponse();
         r1.Processed = true;
         r2.Processed = false;

         _cmdbase.Responses = new List<IResponse> { r1, r2 };
         _cmdbase.RemoveProcessedResponses();
         Assert.AreEqual(1, _cmdbase.Responses.Count);
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

         _cmdbase.ValidRequestTypes = new List<RequestType> { RequestType.Add, RequestType.CheckOut, RequestType.CheckIn, RequestType.Modified, RequestType.Argument, RequestType.Update };
         _cmdbase.Requests = new List<IRequest> { r1, r2, r3 };

         bool result = _cmdbase.AllRequestsAreValid();
         Assert.IsTrue(result);
         IRequest r4 = new UpdatePatchesRequest();
         _cmdbase.Requests.Add(r4);
         Assert.IsFalse(_cmdbase.AllRequestsAreValid());
      }

      /// <summary>
      /// Test for cvs authentication when authentication succeeds
      /// </summary>
      [Test][Ignore]
      public void AuthenticateTest()
      {
         IAuthResponse authResponse = new AuthResponse();
         authResponse.Lines = new List<string> { "I LOVE YOU" };

         Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
         Expect.Call(() => _connection.Close());
         Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
         Expect.Call(_connection.GetResponse()).Return(authResponse);

         _mocks.ReplayAll();

         _cmdbase.Execute();

         _mocks.VerifyAll();
         Assert.IsTrue(authResponse.Processed, "Auth response was not processed");
         Assert.AreEqual(AuthStatus.Authenticated, _cmdbase.AuthStatus);
      }

      /// <summary>
      /// Test for cvs authentication when authentication fails
      /// </summary>
      [Test][Ignore]
      public void NotAuthenticatedTest()
      {
         IAuthResponse authResponse = new AuthResponse();
         authResponse.Lines = new List<string> { "I HATE YOU" };

         Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
         Expect.Call(() => _connection.Close());
         Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
         Expect.Call(_connection.GetResponse()).Return(authResponse);

         _mocks.ReplayAll();

         _cmdbase.Execute();

         _mocks.VerifyAll();
         Assert.IsTrue(authResponse.Processed, "Auth response was not processed");
         Assert.AreEqual(AuthStatus.NotAuthenticated, _cmdbase.AuthStatus);
      }

      /////// <summary>
      /////// Test for cvs authentication when authentication produces an error
      /////// </summary>
      ////[Test]
      ////public void AuthErrorTest()
      ////{
      ////   VerifyAuthCommand cmd = new VerifyAuthCommand(_root, _connection);
      ////   IResponse res1 = new EMessageResponse();
      ////   res1.Initialize(new List<string> { "Fatal error, aborting." });
      ////   IResponse res2 = new ErrorResponse();
      ////   res2.Initialize(new List<string> { "0 no-such-user: no such user" });

      ////   Expect.Call(() => _connection.Connect(_root)).IgnoreArguments();
      ////   Expect.Call(() => _connection.Close());
      ////   Expect.Call(() => _connection.DoRequest(null)).IgnoreArguments();
      ////   Expect.Call(_connection.GetResponse()).Return(res1);
      ////   Expect.Call(_connection.GetAllResponses()).Return(new List<IResponse> { res2 });

      ////   _mocks.ReplayAll();

      ////   cmd.Execute();

      ////   _mocks.VerifyAll();
      ////   Assert.IsTrue(res1.Processed, "E Message response not processed");
      ////   Assert.IsTrue(res2.Processed, "error message response not processed");
      ////   Assert.AreEqual(AuthStatus.Error, cmd.AuthStatus);
      ////   Assert.AreEqual(2, cmd.UserMessages.Count, "Wrong number of messages");
      ////}

      /////// <summary>
      /////// Test of the command base execute method
      /////// </summary>
      ////[Test]
      ////public void CommandBaseExecuteTest()
      ////{
      ////   IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
      ////   IAuthResponse authResponse = new AuthResponse();
      ////   IList<string> lines = new List<string> { "I LOVE YOU" };
      ////   authResponse.Initialize(lines);
      ////   authResponse.Process();

      ////   IRequest validRequestsRequest = new ValidRequestsRequest();
      ////   ValidRequestsResponse validRequestsResponse = new ValidRequestsResponse();
      ////   IList<IResponse> otherResponses = new List<IResponse> { validRequestsResponse };
      ////   lines = new List<string> { "Root Valid-responses valid-requests Repository Directory Max-dotdot Static-directory Sticky Entry Kopt Checkin-time Modified Is-modified Empty-conflicts UseUnchanged Unchanged Notify Questionable Argument Argumentx Global_option Gzip-stream wrapper-sendme-rcsOptions Set expand-modules ci co update diff log rlog add remove update-patches gzip-file-contents status rdiff tag rtag import admin export history release watch-on watch-off watch-add watch-remove watchers editors init annotate rannotate noop version" };
      ////   validRequestsResponse.Initialize(lines);
      ////   validRequestsResponse.Process();

      ////   ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
      ////   command.RequiredRequests = new List<IRequest> { (IRequest) authRequest, validRequestsRequest };

      ////   IRequest otherRequest = new AddRequest();

      ////   command.Requests = new List<IRequest> { otherRequest };

      ////   Expect.Call(() => _connection.Connect(null))
      ////      .IgnoreArguments()
      ////      .Repeat.Once();
      ////   Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
      ////   Expect.Call(_connection.GetResponse()).Return(authResponse);
      ////   Expect.Call(_connection.GetAllResponses()).Return(otherResponses);
      ////   Expect.Call(() => _connection.DoRequest(otherRequest)).Repeat.Once();
      ////   Expect.Call(() => _connection.Close()).Repeat.Once();

      ////   _mocks.ReplayAll();
      ////   command.Execute();
      ////   _mocks.VerifyAll();
      ////}

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

         _cmdbase.RequiredRequests = new List<IRequest> { authRequest, otherRequest };
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
         _cmdbase.Execute();
         _mocks.VerifyAll();
      }

      /////// <summary>
      /////// Tests that the ExecuteRequiredRequests populates the ValidRequestTypes list
      /////// </summary>
      ////[Test]
      ////public void ExectuteRequiredRequestsPopulatesValidRequestTypesTest()
      ////{
      ////   ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
      ////   IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
      ////   IAuthResponse authResponse = new AuthResponse();
      ////   IList<string> lines = new List<string> { "I LOVE YOU" };
      ////   authResponse.Initialize(lines);
      ////   authResponse.Process();

      ////   IRequest validRequestsRequest = new ValidRequestsRequest();
      ////   ValidRequestsResponse validRequestsResponse = new ValidRequestsResponse();
      ////   IList<IResponse> otherResponses = new List<IResponse> { validRequestsResponse };
      ////   lines = new List<string> { "Root Valid-responses valid-requests Repository Directory Max-dotdot" };
      ////   validRequestsResponse.Initialize(lines);
      ////   validRequestsResponse.Process();

      ////   cmd.RequiredRequests = new List<IRequest> { (IRequest) authRequest, validRequestsRequest };

      ////   Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
      ////   Expect.Call(_connection.GetResponse()).Return(authResponse);
      ////   Expect.Call(() => _connection.DoRequest(validRequestsRequest)).Repeat.Once();
      ////   Expect.Call(_connection.GetAllResponses()).Return(otherResponses);
      ////   Expect.Call(authRequest.Type).Return(RequestType.Auth);

      ////   _mocks.ReplayAll();
      ////   ExitCode result = cmd.ExecuteRequiredRequests();
      ////   _mocks.VerifyAll();

      ////   Assert.AreEqual(ExitCode.Succeeded, result);
      ////   Assert.AreEqual(6, cmd.ValidRequestTypes.Count);
      ////}

      /// <summary>
      /// Tests that the ExecuteRequiredRequests has ExitCode of fail when authentication fails
      /// </summary>
      [Test]
      public void ExecuteRequiredRequestsAuthFailExitCodeFailTest()
      {
         IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
         IAuthResponse authResponse = new AuthResponse();
         IList<string> lines = new List<string> { "I HATE YOU" };
         authResponse.Initialize(lines);
         authResponse.Process();

         _cmdbase.RequiredRequests = new List<IRequest> { authRequest };
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(_connection.GetResponse()).Return(authResponse);

         _mocks.ReplayAll();
         ExitCode result = _cmdbase.ExecuteRequiredRequests();
         _mocks.VerifyAll();
         Assert.AreEqual(ExitCode.Failed, result);
      }

      /////// <summary>
      /////// Tests that ExecuteRequiredRequestshas ExitCode of fail when errors are in the responses
      /////// </summary>
      ////[Test]
      ////public void ExecuteRequiredRequestsWhenErrorResponseTest()
      ////{
      ////   ValidRequestsListCommand cmd = new ValidRequestsListCommand(_root, _connection);
      ////   IAuthRequest authRequest = _mocks.DynamicMock<IAuthRequest>();
      ////   IAuthResponse authResponse = new AuthResponse();
      ////   IList<string> lines = new List<string> { "I LOVE YOU" };
      ////   authResponse.Initialize(lines);
      ////   authResponse.Process(); 

      ////   IRequest otherRequest = _mocks.DynamicMock<IRequest>();
      ////   IResponse error = new ErrorResponse();
      ////   lines = new List<string> { "error message" };
      ////   error.Initialize(lines);
      ////   IList<IResponse> errorResponse = new List<IResponse> { error };

      ////   cmd.RequiredRequests = new List<IRequest> { (IRequest) authRequest, otherRequest };

      ////   Expect.Call(() => _connection.DoRequest(authRequest)).Repeat.Once();
      ////   Expect.Call(authRequest.Type).Return(RequestType.Auth);
      ////   Expect.Call(_connection.GetResponse()).Return(authResponse);
      ////   Expect.Call(otherRequest.ResponseExpected).Return(true);
      ////   Expect.Call(() => _connection.DoRequest(otherRequest)).Repeat.Once();
      ////   Expect.Call(_connection.GetAllResponses()).Return(errorResponse);

      ////   _mocks.ReplayAll();
      ////   ExitCode result = cmd.ExecuteRequiredRequests();
      ////   _mocks.VerifyAll();

      ////   Assert.AreEqual(ExitCode.Failed, result);
      ////}
   }
}