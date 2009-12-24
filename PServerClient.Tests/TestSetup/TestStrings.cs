namespace PServerClient.Tests.TestSetup
{
   public static class TestStrings
   {
      public const string UpdatedResponseXML = @"<Response>
    <Name>Updated</Name>
    <ResponseType>7</ResponseType>
    <ProcessLines>
      <Line>Updated mod1/</Line>
      <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
      <Line>/file1.cs/1.2.3.4///</Line>
      <Line>u=rw,g=rw,o=rw</Line>
      <Line>74</Line>
    </ProcessLines>
    <ResponseFile>
      <Length>74</Length>
      <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
    </ResponseFile>
  </Response>";

      public const string AuthResponseXML = @"
  <Response>
    <ResponseName>Auth</ResponseName>
    <ResponseType>0</ResponseType>
    <ProcessLines>
      <Line>I LOVE YOU</Line>
    </ProcessLines>
  </Response>";

      public const string AuthCheckedInResponses = @"
<Responses>
  <Response>
    <Name>Auth</Name>
    <ResponseType>0</ResponseType>
    <ProcessLines>
      <Line>I LOVE YOU</Line>
    </ProcessLines>
  </Response>
  <Response>
    <Name>CheckedIn</Name>
    <ResponseType>5</ResponseType>
    <ProcessLines>
      <Line>Checked-in mod1/</Line>
      <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
      <Line>/file1.cs/1.2.3.4///</Line>
      <Line>u=rw,g=rw,o=rw</Line>
      <Line>74</Line>
    </ProcessLines>
    <ResponseFile>
      <Length>74</Length>
      <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
    </ResponseFile>
  </Response>
</Responses>";

      public const string CommandXML = @"<Command>
  <Name>CheckOut</Name>
  <Type>0</Type>
  <RequiredRequests>
    <Request>
      <Name>Auth</Name>
      <Type>5</Type>
      <Lines>
        <Line>BEGIN AUTH REQUEST</Line>
        <Line>/f1/f2/f3</Line>
        <Line>username</Line>
        <Line>A:yZZ30 e</Line>
        <Line>END AUTH REQUEST</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>Auth</Name>
          <Type>0</Type>
          <Lines>
            <Line>I LOVE YOU</Line>
          </Lines>
        </Response>
      </Responses>
    </Request>
    <Request>
      <Name>UseUnchanged</Name>
      <Type>51</Type>
      <Lines>
        <Line>UseUnchanged</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>ValidResponses</Name>
      <Type>53</Type>
      <Lines>
        <Line>Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>ValidRequests</Name>
      <Type>52</Type>
      <Lines>
        <Line>valid-requests</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>ValidRequests</Name>
          <Type>4</Type>
          <Lines>
            <Line>Valid-requests Root Valid-responses valid-requests Global_option</Line>
          </Lines>
        </Response>
      </Responses>
    </Request>
  </RequiredRequests>
  <Requests>
    <Request>
      <Name>Root</Name>
      <Type>41</Type>
      <Lines>
        <Line>Root /f1/f2/f3</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>GlobalOption</Name>
      <Type>17</Type>
      <Lines>
        <Line>Global_option -q</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>Argument</Name>
      <Type>3</Type>
      <Lines>
        <Line>Argument </Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>Directory</Name>
      <Type>11</Type>
      <Lines>
        <Line>Directory .</Line>
        <Line>/f1/f2/f3/</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>CheckOut</Name>
      <Type>9</Type>
      <Lines>
        <Line>co</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>ModTime</Name>
          <Type>23</Type>
          <Lines>
            <Line>Mod-time 8 Dec 2009 15:26:27 -0000</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT +updated</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT text U</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT fname mymod/file1.cs</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT newline</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT -updated</Line>
          </Lines>
        </Response>
        <Response>
          <Name>Updated</Name>
          <Type>7</Type>
          <Lines>
            <Line>Updated Updated mymod/</Line>
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
    </Request>
  </Requests>
</Command>";
      public const string XMLWithTargetNamespace = @"<?xml version='1.0' encoding='utf-8'?>
            <psvr:Lines xmlns:psvr='http://www.pserverclient.org' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
               xsi:schemaLocation='http://www.pserverclient.org XMLSchemaTest.xsd'>
                  <psvr:Line>my line 1</psvr:Line>
                  <psvr:Line>line 2</psvr:Line>
            </psvr:Lines>";
      public const string CheckedInWithFileContentsResponse = @"<Response>
          <Name>CheckedIn</Name>
          <Type>5</Type>
          <Lines>
            <Line>Checked-in mod1/</Line>
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
          <Name>CheckedIn</Name>
          <Type>5</Type>
          <Lines>
            <Line>Checked-in mod1/</Line>
            <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
            <Line>/file1.cs/1.2.3.4///</Line>
            <Line>u=rw,g=rw,o=rw</Line>
            <Line>74</Line>
          </Lines>
        </Response>";

      public const string CheckOutRequest = @"<Request>
      <Name>CheckOut</Name>
      <Type>9</Type>
      <Lines>
         <Line>co</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>Auth</Name>
          <Type>0</Type>
          <Lines />
        </Response>
      </Responses>
      </Request>";

      public const string CommandXMLFile = @"<?xml version='1.0' encoding='utf-8'?>
<Command>
   <Name>CheckOut</Name>
   <Type>0</Type>
   <RequiredRequests>
      <Request>
         <Name>Auth</Name>
         <Type>5</Type>
           <Lines>
            <Line>BEGIN AUTH REQUEST</Line>
            <Line>/usr/local/cvsroot/sandbox</Line>
            <Line>abougie</Line>
            <Line>AB4%o=wSobI4w</Line>
            <Line>END AUTH REQUEST</Line>
         </Lines>
           <Responses>
              <Response>
               <Name>Auth</Name>
               <Type>0</Type>
                 <Lines>
                  <Line>I LOVE YOU</Line>
               </Lines>
            </Response>
         </Responses>
      </Request>
      <Request>
         <Name>UseUnchanged</Name>
         <Type>51</Type>
          <Lines>
            <Line>UseUnchanged</Line>
         </Lines>
         <Responses />
      </Request>
   </RequiredRequests>
   <Requests>
      <Request>
         <Name>Root</Name>
         <Type>41</Type>
         <Lines>
            <Line>Root /usr/local/cvsroot/sandbox</Line>
         </Lines>
         <Responses />
      </Request>
      <Request>
         <Name>GlobalOption</Name>
         <Type>17</Type>
         <Lines>
            <Line>Global_option -q</Line>
         </Lines>
         <Responses />
      </Request>
   </Requests>
</Command>";

      public const string CommandXMLFileWithManyItems = @"<?xml version='1.0' encoding='utf-8'?>
<Command>
   <Name>CheckOut</Name>
   <Type>0</Type>
   <RequiredRequests>
      <Request>
         <Name>Auth</Name>
         <Type>5</Type>
           <Lines>
            <Line>BEGIN AUTH REQUEST</Line>
            <Line>/usr/local/cvsroot/sandbox</Line>
            <Line>abougie</Line>
            <Line>AB4%o=wSobI4w</Line>
            <Line>END AUTH REQUEST</Line>
         </Lines>
           <Responses>
              <Response>
               <Name>Auth</Name>
               <Type>0</Type>
                 <Lines>
                  <Line>I LOVE YOU</Line>
               </Lines>
            </Response>
         </Responses>
      </Request>
      <Request>
         <Name>UseUnchanged</Name>
         <Type>51</Type>
          <Lines>
            <Line>UseUnchanged</Line>
         </Lines>
         <Responses />
      </Request>
   </RequiredRequests>
   <Requests>
      <Request>
         <Name>Root</Name>
         <Type>41</Type>
         <Lines>
            <Line>Root /usr/local/cvsroot/sandbox</Line>
         </Lines>
         <Responses />
      </Request>
      <Request>
         <Name>GlobalOption</Name>
         <Type>17</Type>
         <Lines>
            <Line>Global_option -q</Line>
         </Lines>
         <Responses />
      </Request>
     <Request>
       <Name>CheckOut</Name>
       <Type>9</Type>
       <Lines>
         <Line>co</Line>
       </Lines>
       <Responses>
         <Response>
           <Name>ClearSticky</Name>
           <Type>17</Type>
           <Lines>
             <Line>Clear-sticky abougie/</Line>
             <Line>/usr/local/cvsroot/sandbox/abougie/</Line>
           </Lines>
         </Response>
         <Response>
           <Name>SetStaticDirectory</Name>
           <Type>14</Type>
           <Lines>
             <Line>Set-static-directory abougie/</Line>
             <Line>/usr/local/cvsroot/sandbox/abougie/</Line>
           </Lines>
         </Response>
       </Responses>
     </Request>
   </Requests>
</Command>";
   }
}