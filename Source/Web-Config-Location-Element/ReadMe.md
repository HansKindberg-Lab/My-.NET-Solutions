# ASP.NET Core application with location element in Web.config

If you want to use location-elements in [Web.config](/Source/Web-Config-Location-Element/NET-Core-MVC/Web.config#L6) in an ASP.NET Core application, don´t forget to add an AspNetCore-handler, if you want it to work in IIS Express. Otherwhise you will get a 404.0.

	<configuration>
		<location path="Your/Desired/Path">
			<system.webServer>
				<handlers>
					<add name="aspNetCore" modules="AspNetCoreModuleV2" path="*" resourceType="Unspecified" verb="*" />
				</handlers>
			</system.webServer>
		</location>
	</configuration>