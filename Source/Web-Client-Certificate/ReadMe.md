# IIS-hosted ASP.NET web-application asking for client-certificate

## 1 Web.config

### [1.1 NET-Core-MVC](/Source/Web-Client-Certificate/NET-Core-MVC/Web.config)

	<configuration>
		<location path="Home/NegotiateClientCertificate">
			<system.webServer>
				<handlers>
					<add name="aspNetCore" modules="AspNetCoreModuleV2" path="*" resourceType="Unspecified" verb="*" />
				</handlers>
				<security>
					<access sslFlags="SslNegotiateCert" />
				</security>
			</system.webServer>
		</location>
		<location path="Home/RequireClientCertificate">
			<system.webServer>
				<handlers>
					<add name="aspNetCore" modules="AspNetCoreModuleV2" path="*" resourceType="Unspecified" verb="*" />
				</handlers>
				<security>
					<access sslFlags="SslNegotiateCert, SslRequireCert" />
				</security>
			</system.webServer>
		</location>
		<location path="Home/SslNegotiateClientCertificate">
			<system.webServer>
				<handlers>
					<add name="aspNetCore" modules="AspNetCoreModuleV2" path="*" resourceType="Unspecified" verb="*" />
				</handlers>
				<security>
					<access sslFlags="Ssl, SslNegotiateCert" />
				</security>
			</system.webServer>
		</location>
		<location path="Home/SslRequireClientCertificate">
			<system.webServer>
				<handlers>
					<add name="aspNetCore" modules="AspNetCoreModuleV2" path="*" resourceType="Unspecified" verb="*" />
				</handlers>
				<security>
					<access sslFlags="Ssl, SslNegotiateCert, SslRequireCert" />
				</security>
			</system.webServer>
		</location>
	</configuration>

### [1.2 NET-Framework-MVC](/Source/Web-Client-Certificate/NET-Framework-MVC/Web.config)

	<configuration>
		<location path="Home/NegotiateClientCertificate">
			<system.webServer>
				<security>
					<access sslFlags="SslNegotiateCert" />
				</security>
			</system.webServer>
		</location>
		<location path="Home/RequireClientCertificate">
			<system.webServer>
				<security>
					<access sslFlags="SslNegotiateCert, SslRequireCert" />
				</security>
			</system.webServer>
		</location>
		<location path="Home/SslNegotiateClientCertificate">
			<system.webServer>
				<security>
					<access sslFlags="Ssl, SslNegotiateCert" />
				</security>
			</system.webServer>
		</location>
		<location path="Home/SslRequireClientCertificate">
			<system.webServer>
				<security>
					<access sslFlags="Ssl, SslNegotiateCert, SslRequireCert" />
				</security>
			</system.webServer>
		</location>
	</configuration>

### 1.3 Allow anonymous access

	<configuration>
		...
		<system.webServer>
			<security>
				<authentication>
					<anonymousAuthentication enabled="true"/>
					<basicAuthentication enabled="false"/>
					<clientCertificateMappingAuthentication enabled="false"/>
					<digestAuthentication enabled="false"/>
					<iisClientCertificateMappingAuthentication enabled="false"/>
					<windowsAuthentication enabled="false"/>
				</authentication>
			</security>
		</system.webServer>
		...
	</configuration>
	
We only want to ask for a client-certificate, we dont want to authenticate with it. So we allow anonymous access.