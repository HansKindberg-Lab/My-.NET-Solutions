# Constructing stylesheets and scripts in an ASP.NET web-application

## 1 Projects

### 1.1 NET-Core-MVC

Toggle the "ASPNETCORE_ENVIRONMENT" setting between "Development" and "Production" in [LaunchSettings.json](/Source/Web-Client-Files/NET-Core-MVC/Properties/LaunchSettings.json#L14) and see how the the source for the page changes regarding html/head/link and html/body/script.

### 1.2 NET-Framework-MVC

Toggle the "EnableBundleOptimizations" setting between "true" and "false" in [Web.config](/Source/Web-Client-Files/NET-Framework-MVC/Web.config#L4) and see how the the source for the page changes regarding html/head/link and html/body/script.

## 2 Included sources
### 2.1 CSS
- **bootstrap.css**: https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.css

### 2.2 JS
- **bootstrap.js**: https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.js
- **jquery.js**: https://code.jquery.com/jquery-3.3.1.js or https://code.jquery.com/jquery-3.3.1.slim.js
- **popper.js**: https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.js

## 3 Explanations
[Company.WebApplication.Global.Application_Start(object sender, EventArgs e):](/Source/Web-Client-Files/NET-Framework-MVC/Global.asax.cs#L20)

    BundleTable.EnableOptimizations = bool.Parse(ConfigurationManager.AppSettings["EnableBundleOptimizations"]);

The value of BundleTable.EnableOptimizations is by default handled by the following setting:

    <configuration>
	    <system.web>
		    <compilation debug="true" />
	    </system.web>
    </configuration>

but in this solution we want to control it without changing the debug-value.

From System.Web.Optimization.BundleTable:

    public static bool EnableOptimizations
    {
        get
        {
            if (!BundleTable._enableOptimizationsSet && HttpContext.Current != null)
                return !HttpContext.Current.IsDebuggingEnabled;

            return BundleTable._enableOptimizations;
        }
        set
        {
            BundleTable._enableOptimizations = value;
            BundleTable._enableOptimizationsSet = true;
        }
    }

## 3.1 Fix
[Company.WebApplication.Global.Application_Start(object sender, EventArgs e):](/Source/Web-Client-Files/NET-Framework-MVC/Global.asax.cs#L23)

	var scriptBundle = new ScriptBundle("~/Site-scripts").Include(BundleTable.EnableOptimizations ? new[] {"~/Scripts/Site.min.js"} : new[] {"~/Scripts/jquery.js", "~/Scripts/popper.js", "~/Scripts/bootstrap.js", "~/Scripts/Main.js"});
	scriptBundle.Transforms.Clear();
	BundleTable.Bundles.Add(scriptBundle);

	var styleBundle = new StyleBundle("~/Site-style").Include(BundleTable.EnableOptimizations ? new[] {"~/Style/Site.min.css"} : new[] {"~/Style/bootstrap.css", "~/Style/Main.css"});
	styleBundle.Transforms.Clear();
	BundleTable.Bundles.Add(styleBundle);

We can not just do like the following:

	BundleTable.Bundles.Add(new ScriptBundle("~/Site-scripts").Include(
		"~/Scripts/jquery.js",
		"~/Scripts/popper.js",
		"~/Scripts/bootstrap.js",
		"~/Scripts/Main.js"
	));

	BundleTable.Bundles.Add(new StyleBundle("~/Site-style").Include(
		"~/Style/bootstrap.css",
		"~/Style/Main.css"
	));

Because if BundleTable.EnableOptimizations = true then we will get minify-errors like:
- "run-time error CSS1062: Expected semicolon or closing curly-brace, found '-'"

because of the ":root"-declaration at the beginning of bootstrap.css.