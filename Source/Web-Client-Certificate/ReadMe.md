# IIS-hosted ASP.NET web-application asking for client-certificate

## 1 The important parts in Web.config

	<configuration>
		<location path="AskForClientCertificate">
			<system.webServer>
				<httpErrors errorMode="Custom" existingResponse="Replace">
					<remove statusCode="403" subStatusCode="16" />
					<error path="/Exception/Catch/?StatusCode=403&amp;SubStatusCode=16" responseMode="ExecuteURL" statusCode="403" subStatusCode="16" />
				</httpErrors>
				<security>
					<access sslFlags="SslNegotiateCert" />
				</security>
			</system.webServer>
		</location>
		<system.webServer>
			<security>
				<authentication>
					<anonymousAuthentication enabled="true" />
					<basicAuthentication enabled="false" />
					<clientCertificateMappingAuthentication enabled="false" />
					<digestAuthentication enabled="false" />
					<iisClientCertificateMappingAuthentication enabled="false" />
					<windowsAuthentication enabled="false" />
				</authentication>
			</security>
		</system.webServer>
	</configuration>

- [NET-Core-MVC - Web.config](/Source/Web-Client-Certificate/NET-Core-MVC/Web.config)
- [NET-Framework-MVC - Web.config](/Source/Web-Client-Certificate/NET-Framework-MVC/Web.config)
	
### 1.1 Ask for the client-certificate

	<location path="AskForClientCertificate">
		<system.webServer>
			<security>
				<access sslFlags="SslNegotiateCert" />
			</security>
		</system.webServer>
	</location>
	
A request on this path will ask for a client-certificate.
	
### 1.2 Bypass 403.16

	<location path="AskForClientCertificate">
		<system.webServer>
			<httpErrors errorMode="Custom" existingResponse="Replace">
				<remove statusCode="403" subStatusCode="16" />
				<error path="/Exception/Catch/?StatusCode=403&amp;SubStatusCode=16" responseMode="ExecuteURL" statusCode="403" subStatusCode="16" />
			</httpErrors>
		</system.webServer>
	</location>
	
If the selected client-certificate is not trusted (for example), the IIS will throw a 403.16. We want to allow all certificates so we have to handle 403.16 customly.

### 1.3 Allow anonymous access

	<system.webServer>
		<security>
			<authentication>
				<anonymousAuthentication enabled="true" />
				<basicAuthentication enabled="false" />
				<clientCertificateMappingAuthentication enabled="false" />
				<digestAuthentication enabled="false" />
				<iisClientCertificateMappingAuthentication enabled="false" />
				<windowsAuthentication enabled="false" />
			</authentication>
		</security>
	</system.webServer>
	
We only want to ask for a client-certificate, we dont want to authenticate with it. So we allow anonymous access.