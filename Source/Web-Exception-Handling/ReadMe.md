# Exception-handling in an IIS-hosted ASP.NET web-application

## 1 The important parts in Web.config

	<configuration>
		<location path="Exception">
			<system.webServer>
				<httpErrors errorMode="DetailedLocalOnly" existingResponse="Auto">
					<clear />
				</httpErrors>
			</system.webServer>
		</location>
		<system.webServer>
			<httpErrors errorMode="Custom" existingResponse="Replace">
				<clear />
				<error path="/Exception/?StatusCode=401" responseMode="ExecuteURL" statusCode="401" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=1" responseMode="ExecuteURL" statusCode="401" subStatusCode="1" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=2" responseMode="ExecuteURL" statusCode="401" subStatusCode="2" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=3" responseMode="ExecuteURL" statusCode="401" subStatusCode="3" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=4" responseMode="ExecuteURL" statusCode="401" subStatusCode="4" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=5" responseMode="ExecuteURL" statusCode="401" subStatusCode="5" />
				<error path="/Exception/?StatusCode=403" responseMode="ExecuteURL" statusCode="403" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=1" responseMode="ExecuteURL" statusCode="403" subStatusCode="1" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=2" responseMode="ExecuteURL" statusCode="403" subStatusCode="2" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=3" responseMode="ExecuteURL" statusCode="403" subStatusCode="3" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=4" responseMode="ExecuteURL" statusCode="403" subStatusCode="4" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=5" responseMode="ExecuteURL" statusCode="403" subStatusCode="5" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=6" responseMode="ExecuteURL" statusCode="403" subStatusCode="6" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=7" responseMode="ExecuteURL" statusCode="403" subStatusCode="7" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=8" responseMode="ExecuteURL" statusCode="403" subStatusCode="8" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=9" responseMode="ExecuteURL" statusCode="403" subStatusCode="9" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=10" responseMode="ExecuteURL" statusCode="403" subStatusCode="10" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=11" responseMode="ExecuteURL" statusCode="403" subStatusCode="11" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=12" responseMode="ExecuteURL" statusCode="403" subStatusCode="12" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=13" responseMode="ExecuteURL" statusCode="403" subStatusCode="13" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=14" responseMode="ExecuteURL" statusCode="403" subStatusCode="14" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=15" responseMode="ExecuteURL" statusCode="403" subStatusCode="15" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=16" responseMode="ExecuteURL" statusCode="403" subStatusCode="16" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=17" responseMode="ExecuteURL" statusCode="403" subStatusCode="17" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=18" responseMode="ExecuteURL" statusCode="403" subStatusCode="18" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=19" responseMode="ExecuteURL" statusCode="403" subStatusCode="19" />
				<error path="/Exception/?StatusCode=404" responseMode="ExecuteURL" statusCode="404" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=1" responseMode="ExecuteURL" statusCode="404" subStatusCode="1" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=2" responseMode="ExecuteURL" statusCode="404" subStatusCode="2" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=3" responseMode="ExecuteURL" statusCode="404" subStatusCode="3" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=4" responseMode="ExecuteURL" statusCode="404" subStatusCode="4" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=5" responseMode="ExecuteURL" statusCode="404" subStatusCode="5" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=6" responseMode="ExecuteURL" statusCode="404" subStatusCode="6" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=7" responseMode="ExecuteURL" statusCode="404" subStatusCode="7" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=8" responseMode="ExecuteURL" statusCode="404" subStatusCode="8" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=9" responseMode="ExecuteURL" statusCode="404" subStatusCode="9" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=10" responseMode="ExecuteURL" statusCode="404" subStatusCode="10" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=11" responseMode="ExecuteURL" statusCode="404" subStatusCode="11" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=12" responseMode="ExecuteURL" statusCode="404" subStatusCode="12" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=13" responseMode="ExecuteURL" statusCode="404" subStatusCode="13" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=14" responseMode="ExecuteURL" statusCode="404" subStatusCode="14" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=15" responseMode="ExecuteURL" statusCode="404" subStatusCode="15" />
				<error path="/Exception/?StatusCode=405" responseMode="ExecuteURL" statusCode="405" />
				<error path="/Exception/?StatusCode=406" responseMode="ExecuteURL" statusCode="406" />
				<error path="/Exception/?StatusCode=412" responseMode="ExecuteURL" statusCode="412" />
				<error path="/Exception/?StatusCode=500" responseMode="ExecuteURL" statusCode="500" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=13" responseMode="ExecuteURL" statusCode="500" subStatusCode="13" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=14" responseMode="ExecuteURL" statusCode="500" subStatusCode="14" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=15" responseMode="ExecuteURL" statusCode="500" subStatusCode="15" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=16" responseMode="ExecuteURL" statusCode="500" subStatusCode="16" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=17" responseMode="ExecuteURL" statusCode="500" subStatusCode="17" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=18" responseMode="ExecuteURL" statusCode="500" subStatusCode="18" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=19" responseMode="ExecuteURL" statusCode="500" subStatusCode="19" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=100" responseMode="ExecuteURL" statusCode="500" subStatusCode="100" />
				<error path="/Exception/?StatusCode=501" responseMode="ExecuteURL" statusCode="501" />
				<error path="/Exception/?StatusCode=502" responseMode="ExecuteURL" statusCode="502" />
			</httpErrors>
		</system.webServer>
	</configuration>

- [NET-Core-MVC - Web.config](/Source/Web-Exception-Handling/NET-Core-MVC/Web.config)
- [NET-Framework-MVC - Web.config](/Source/Web-Exception-Handling/NET-Framework-MVC/Web.config)

### 1.1 Custom handling of http-errors

	<configuration>
		<system.webServer>
			<httpErrors errorMode="Custom" existingResponse="Replace">
				<clear />
				<error path="/Exception/?StatusCode=401" responseMode="ExecuteURL" statusCode="401" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=1" responseMode="ExecuteURL" statusCode="401" subStatusCode="1" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=2" responseMode="ExecuteURL" statusCode="401" subStatusCode="2" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=3" responseMode="ExecuteURL" statusCode="401" subStatusCode="3" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=4" responseMode="ExecuteURL" statusCode="401" subStatusCode="4" />
				<error path="/Exception/?StatusCode=401&amp;SubStatusCode=5" responseMode="ExecuteURL" statusCode="401" subStatusCode="5" />
				<error path="/Exception/?StatusCode=403" responseMode="ExecuteURL" statusCode="403" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=1" responseMode="ExecuteURL" statusCode="403" subStatusCode="1" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=2" responseMode="ExecuteURL" statusCode="403" subStatusCode="2" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=3" responseMode="ExecuteURL" statusCode="403" subStatusCode="3" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=4" responseMode="ExecuteURL" statusCode="403" subStatusCode="4" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=5" responseMode="ExecuteURL" statusCode="403" subStatusCode="5" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=6" responseMode="ExecuteURL" statusCode="403" subStatusCode="6" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=7" responseMode="ExecuteURL" statusCode="403" subStatusCode="7" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=8" responseMode="ExecuteURL" statusCode="403" subStatusCode="8" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=9" responseMode="ExecuteURL" statusCode="403" subStatusCode="9" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=10" responseMode="ExecuteURL" statusCode="403" subStatusCode="10" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=11" responseMode="ExecuteURL" statusCode="403" subStatusCode="11" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=12" responseMode="ExecuteURL" statusCode="403" subStatusCode="12" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=13" responseMode="ExecuteURL" statusCode="403" subStatusCode="13" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=14" responseMode="ExecuteURL" statusCode="403" subStatusCode="14" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=15" responseMode="ExecuteURL" statusCode="403" subStatusCode="15" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=16" responseMode="ExecuteURL" statusCode="403" subStatusCode="16" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=17" responseMode="ExecuteURL" statusCode="403" subStatusCode="17" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=18" responseMode="ExecuteURL" statusCode="403" subStatusCode="18" />
				<error path="/Exception/?StatusCode=403&amp;SubStatusCode=19" responseMode="ExecuteURL" statusCode="403" subStatusCode="19" />
				<error path="/Exception/?StatusCode=404" responseMode="ExecuteURL" statusCode="404" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=1" responseMode="ExecuteURL" statusCode="404" subStatusCode="1" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=2" responseMode="ExecuteURL" statusCode="404" subStatusCode="2" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=3" responseMode="ExecuteURL" statusCode="404" subStatusCode="3" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=4" responseMode="ExecuteURL" statusCode="404" subStatusCode="4" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=5" responseMode="ExecuteURL" statusCode="404" subStatusCode="5" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=6" responseMode="ExecuteURL" statusCode="404" subStatusCode="6" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=7" responseMode="ExecuteURL" statusCode="404" subStatusCode="7" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=8" responseMode="ExecuteURL" statusCode="404" subStatusCode="8" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=9" responseMode="ExecuteURL" statusCode="404" subStatusCode="9" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=10" responseMode="ExecuteURL" statusCode="404" subStatusCode="10" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=11" responseMode="ExecuteURL" statusCode="404" subStatusCode="11" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=12" responseMode="ExecuteURL" statusCode="404" subStatusCode="12" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=13" responseMode="ExecuteURL" statusCode="404" subStatusCode="13" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=14" responseMode="ExecuteURL" statusCode="404" subStatusCode="14" />
				<error path="/Exception/?StatusCode=404&amp;SubStatusCode=15" responseMode="ExecuteURL" statusCode="404" subStatusCode="15" />
				<error path="/Exception/?StatusCode=405" responseMode="ExecuteURL" statusCode="405" />
				<error path="/Exception/?StatusCode=406" responseMode="ExecuteURL" statusCode="406" />
				<error path="/Exception/?StatusCode=412" responseMode="ExecuteURL" statusCode="412" />
				<error path="/Exception/?StatusCode=500" responseMode="ExecuteURL" statusCode="500" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=13" responseMode="ExecuteURL" statusCode="500" subStatusCode="13" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=14" responseMode="ExecuteURL" statusCode="500" subStatusCode="14" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=15" responseMode="ExecuteURL" statusCode="500" subStatusCode="15" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=16" responseMode="ExecuteURL" statusCode="500" subStatusCode="16" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=17" responseMode="ExecuteURL" statusCode="500" subStatusCode="17" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=18" responseMode="ExecuteURL" statusCode="500" subStatusCode="18" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=19" responseMode="ExecuteURL" statusCode="500" subStatusCode="19" />
				<error path="/Exception/?StatusCode=500&amp;SubStatusCode=100" responseMode="ExecuteURL" statusCode="500" subStatusCode="100" />
				<error path="/Exception/?StatusCode=501" responseMode="ExecuteURL" statusCode="501" />
				<error path="/Exception/?StatusCode=502" responseMode="ExecuteURL" statusCode="502" />
			</httpErrors>
		</system.webServer>
	</configuration>
	
In this case we declare "all" possible codes explicitly.

We could also do:
	
	<configuration>
		<system.webServer>
			<httpErrors defaultPath="/Exception/?StatusCode=500" defaultResponseMode="ExecuteURL" errorMode="Custom" existingResponse="Replace">
				<clear />
				<error path="/Exception/?StatusCode=404" responseMode="ExecuteURL" statusCode="404" />
			</httpErrors>
		</system.webServer>
	</configuration>

or:

	<configuration>
		<system.webServer>
			<httpErrors defaultPath="/Exception/?StatusCode=500" defaultResponseMode="ExecuteURL" errorMode="Custom" existingResponse="Replace">
				<remove statusCode="404" subStatusCode="-1" />
				<error path="/Exception/?StatusCode=404" responseMode="ExecuteURL" statusCode="404" />
			</httpErrors>
		</system.webServer>
	</configuration>
	
Then status-code 404 would be handled separately. All other status-codes would would be replaced with "/Exception/?StatusCode=500".
	
To be allowed to set the default-path (/configuration/system.webServer/httpErrors@defaultPath) we have to change:

	<configuration>
		...
		<system.webServer>
			...
			<httpErrors lockAttributes="allowAbsolutePathsWhenDelegated,defaultPath">
			...
		</system.webServer>
		...
	</configuration>

in ApplicationHost.config (eg. C:\Windows\System32\inetsrv\config\applicationhost.config) to:

	<configuration>
		...
		<system.webServer>
			...
			<httpErrors lockAttributes="allowAbsolutePathsWhenDelegated">
			...
		</system.webServer>
		...
	</configuration>

### 1.2 Reset custom handling of http-errors on the "Exception"-path

	<location path="Exception">
		<system.webServer>
			<httpErrors errorMode="DetailedLocalOnly" existingResponse="Auto">
				<clear />
			</httpErrors>
		</system.webServer>
	</location>

In our ExceptionController we set the status-code of the http-response.

	this.Response.StatusCode = XX;
	
If we would not reset custom handling on this path, custom handling would be triggered again, under certain circumstances.