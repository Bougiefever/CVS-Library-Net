namespace PServerClient.Tests.TestSetup
{
   public static class TestStrings
   {
      public const string UpdatedResponseXML = @"<Response>
    <ClassName>PServerClient.Responses.UpdatedResponse</ClassName>
    <Name>Updated</Name>
    <Lines>
      <Line>Updated mod1/</Line>
      <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
      <Line>/file1.cs/1.2.3.4///</Line>
      <Line>u=rw,g=rw,o=rw</Line>
      <Line>74</Line>
    </Lines>
    <File>
      <Length>74</Length>
      <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
    </File>
  </Response>";

      public const string AuthResponseXML = @"
  <Response>
    <ClassName>PServerClient.Responses.AuthResponse</ClassName>
    <Name/>
    <Lines>
      <Line>I LOVE YOU</Line>
    </Lines>
  </Response>";


      public const string XMLWithTargetNamespace = @"<?xml version='1.0' encoding='utf-8'?>
            <psvr:Lines xmlns:psvr='http://www.pserverclient.org' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
               xsi:schemaLocation='http://www.pserverclient.org XMLSchemaTest.xsd'>
                  <psvr:Line>my line 1</psvr:Line>
                  <psvr:Line>line 2</psvr:Line>
            </psvr:Lines>";
      public const string CheckedInWithFileContentsResponse = @"<Response>
  <ClassName>PServerClient.Responses.CheckedInResponse</ClassName>
  <Name>Checked-in</Name>
  <Lines>
    <Line>mod1/</Line>
    <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
    <Line>/file1.cs/1.2.3.4///</Line>
    <Line>u=rw,g=rw,o=rw</Line>
    <Line>74</Line>
  </Lines>
  <File>
    <Length>74</Length>
    <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
  </File>
</Response>";

      public const string CheckedInResponse = @"<Response>
          <ClassName>PServerClient.Responses.CheckedInResponse</ClassName>
          <Name>Checked-in</Name>
          <Lines>
            <Line>Checked-in mod1/</Line>
            <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
            <Line>/file1.cs/1.2.3.4///</Line>
            <Line>u=rw,g=rw,o=rw</Line>
            <Line>74</Line>
          </Lines>
        </Response>";

      public const string CheckOutRequest = @"<Request>
      <ClassName>PServerClient.Requests.CheckOutRequest</ClassName>
      <Lines>
         <Line>co</Line>
      </Lines>
      </Request>";

      public const string CommandXMLFile = @"<?xml version='1.0' encoding='utf-8'?>
<Command>
  <ClassName>PServerClient.Commands.CheckOutCommand</ClassName>
  <RequiredRequests>
    <Request>
      <ClassName>PServerClient.Requests.AuthRequest</ClassName>
      <Lines>
        <Line>BEGIN AUTH REQUEST</Line>
        <Line>/f1/f2/f3</Line>
        <Line>username</Line>
        <Line>A:yZZ30 e</Line>
        <Line>END AUTH REQUEST</Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.UseUnchangedRequest</ClassName>
      <Lines>
        <Line>UseUnchanged</Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.ValidResponsesRequest</ClassName>
      <Lines>
        <Line>Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT</Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.ValidRequestsRequest</ClassName>
      <Lines>
        <Line>valid-requests</Line>
      </Lines>
    </Request>
  </RequiredRequests>
  <Requests>
    <Request>
      <ClassName>PServerClient.Requests.RootRequest</ClassName>
      <Lines>
        <Line>Root /f1/f2/f3</Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.GlobalOptionRequest</ClassName>
      <Lines>
        <Line>Global_option -q</Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.ArgumentRequest</ClassName>
      <Lines>
        <Line>Argument </Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.DirectoryRequest</ClassName>
      <Lines>
        <Line>Directory .</Line>
        <Line>/f1/f2/f3/</Line>
      </Lines>
    </Request>
    <Request>
      <ClassName>PServerClient.Requests.CheckOutRequest</ClassName>
      <Lines>
        <Line>co</Line>
      </Lines>
    </Request>
  </Requests>
   <Responses>
     <Response>
       <ClassName>PServerClient.Responses.AuthResponse</ClassName>
       <Name/>
       <Lines>
         <Line>I LOVE YOU</Line>
       </Lines>
     </Response>        
     <Response>
       <ClassName>PServerClient.Responses.ValidRequestsResponse</ClassName>
       <Name>Valid-requests</Name>
       <Lines>
         <Line>Root Valid-responses valid-requests Global_option</Line>
       </Lines>
     </Response>
        <Response>
          <ClassName>PServerClient.Responses.ModTimeResponse</ClassName>
          <Name>Mod-time</Name>
          <Lines>
            <Line>8 Dec 2009 15:26:27 -0000</Line>
          </Lines>
        </Response>
        <Response>
          <ClassName>PServerClient.Responses.MTMessageResponse</ClassName>
          <Name>MT</Name>
          <Lines>
            <Line>+updated</Line>
          </Lines>
        </Response>
        <Response>
          <ClassName>PServerClient.Responses.MTMessageResponse</ClassName>
          <Name>MT</Name>
          <Lines>
            <Line>text U</Line>
          </Lines>
        </Response>
        <Response>
          <ClassName>PServerClient.Responses.MTMessageResponse</ClassName>
          <Name>MT</Name>
          <Lines>
            <Line>fname mymod/file1.cs</Line>
          </Lines>
        </Response>
        <Response>
          <ClassName>PServerClient.Responses.MTMessageResponse</ClassName>
          <Name>MT</Name>
          <Lines>
            <Line>newline</Line>
          </Lines>
        </Response>
        <Response>
          <ClassName>PServerClient.Responses.MTMessageResponse</ClassName>
          <Name>MT</Name>
          <Lines>
            <Line>-updated</Line>
          </Lines>
        </Response>
        <Response>
          <ClassName>PServerClient.Responses.UpdatedResponse</ClassName>
          <Name>Updated</Name>
          <Lines>
            <Line>Updated mymod/</Line>
            <Line>/usr/local/cvsroot/sandbox/mymod/file1.cs</Line>
            <Line>/file1.cs/1.1.1.1///</Line>
            <Line>u=rw,g=rw,o=rw</Line>
            <Line>5</Line>
          </Lines>
          <File>
            <Length>5</Length>
            <Contents>97,98,99,100,101</Contents>
          </File>
        </Response>
   </Responses>
</Command>";

      public const string CommandXMLFileWithManyItems = @"<?xml version='1.0' encoding='utf-8'?>
<Command>
   <ClassName>PServerClient.Commands.CheckOutCommand</ClassName>
   <RequiredRequests>
      <Request>
         <ClassName>PServerClient.Requests.AuthRequest</ClassName>
         <Lines>
            <Line>BEGIN AUTH REQUEST</Line>
            <Line>/usr/local/cvsroot/sandbox</Line>
            <Line>abougie</Line>
            <Line>AB4%o=wSobI4w</Line>
            <Line>END AUTH REQUEST</Line>
         </Lines>
           <Responses>
              <Response>
               <ClassName>PServerClient.Responses.AuthResponse</ClassName>
               <Name/>
               <Lines>
                 <Line>I LOVE YOU</Line>
               </Lines>
            </Response>
         </Responses>
      </Request>
      <Request>
         <ClassName>PServerClient.Requests.UseUnchangedRequest</ClassName>
         <Lines>
            <Line>UseUnchanged</Line>
         </Lines>
         <Responses />
      </Request>
   </RequiredRequests>
   <Requests>
      <Request>
         <ClassName>PServerClient.Requests.RootRequest</ClassName>
         <Lines>
            <Line>Root /usr/local/cvsroot/sandbox</Line>
         </Lines>
         <Responses />
      </Request>
      <Request>
         <ClassName>PServerClient.Requests.GlobalOptionRequest</ClassName>
         <Lines>
            <Line>Global_option -q</Line>
         </Lines>
         <Responses />
      </Request>
     <Request>
       <ClassName>PServerClient.Requests.CheckOutRequest</ClassName>
       <Lines>
         <Line>co</Line>
       </Lines>
       <Responses>
         <Response>
           <ClassName>PServerClient.Responses.ClearStickyResponse</ClassName>
           <Name>Clear-sticky</Name>
           <Lines>
             <Line>Clear-sticky abougie/</Line>
             <Line>/usr/local/cvsroot/sandbox/abougie/</Line>
           </Lines>
         </Response>
         <Response>
           <ClassName>PServerClient.Responses.SetStaticDirectoryResponse</ClassName>
           <Name>Set-static-directory</Name>
           <Lines>
             <Line>Set-static-directory abougie/</Line>
             <Line>/usr/local/cvsroot/sandbox/abougie/</Line>
           </Lines>
         </Response>
       </Responses>
     </Request>
   </Requests>
</Command>";
      public const string MTResponse = @"<Response>
          <ClassName>PServerClient.Responses.MTMessageResponse</ClassName>
          <Name>MT</Name>
          <Lines>
            <Line>+updated</Line>
            <Line>text U </Line>
            <Line>fname abougie/cvstest/NewTestApp.sln</Line>
            <Line>newline</Line>
            <Line>-updated</Line>
          </Lines>
        </Response>";
   }
}