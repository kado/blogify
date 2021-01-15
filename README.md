# Blogify
Blogify is a micro blog engine developed in .NET Core 5. This project was made with 
Microsoft Visual Studio Community 2019 Versión 16.8.4.

## Total Time
I took aproximately 18 hrs + 2 Hours of planning and reading some articles.

## Database
This project runs with MS SQL Server 2019 Express. Please run: Script DBBlogify.sql file 
to create the database with some sample data.

## Dependency Injection
Dependency Injection is implemented with defaults .NET Core 5.0 tools. Check Startup.cs for service configuration:
``` C#
services.AddScoped<BlogifyWebApi.Models.Interfaces.IBlogProvider, BlogifyWebApi.Models.Providers.BlogProvider>();
services.AddScoped<BlogifyWebApi.Models.Interfaces.IBlog, BlogifyWebApi.Models.EF.Blog>();
services.AddScoped<BlogifyWebApi.Models.Interfaces.ICategory, BlogifyWebApi.Models.EF.Category>();
services.AddScoped<BlogifyWebApi.Models.Interfaces.IUser, BlogifyWebApi.Models.EF.User>();
services.AddScoped<BlogifyWebApi.Models.Interfaces.IComment, BlogifyWebApi.Models.EF.Comment>();
```

## Testing Credentials
These are the software credentials for testing purposes:

| Username | Password | Role |
| ------ | ------ | ------ |
| blogger1 | Back4g00d | Writer |
| blogger2 | Back4g00d | Writer |
| klacatt | Back4g00d | Editor |

## Unit Testing
A unit testing project was added. Tests class were implemented just to test methods 
for BlogProvider Class in both BlogifyWebApp and BlogifyWebApi

## BlogifyWebApp

Is a ASP.NET Core MVC 5 Web Application for handling Editor's operations.

Nuget Packages Required:
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation Version 5.0.2
Microsoft.EntityFrameworkCore Version 5.0.2
Microsoft.EntityFrameworkCore.SqlServer Version 5.0.2
Microsoft.EntityFrameworkCore.Tools Version 5.0.2

## Connections Strings 
The connection string is in appsettings.json, must be changed in order run the project:
```json
  "ConnectionStrings": {
    "BlogifyCnnStr": "Server=localhost;Database=DBBlogify;Trusted_Connection=True;"
  },
```
[Test the up and running web application](http://blogify.southcentralus.cloudapp.azure.com/webapp)

## BlogifyWebApi
Is ASP.NET Core 5 Web API.

Nuget Packages Required:
Microsoft.EntityFrameworkCore Version 5.0.2
Microsoft.EntityFrameworkCore.SqlServer Version 5.0.2
Microsoft.EntityFrameworkCore.Tools Version 5.0.2
Newtonsoft.Json Version 12.0.3 
Swashbuckle.AspNetCore Version 5.6.3

## Available Endpoints
##  /Category/List
GET Method
Returns a list of blog categories. 
OutPut:
``` C#
	[
		{
			string Name,
			int Id 
		}
		...
	]
```

##  /Blog/List?categoryId=X
GET Method
Return a list of blog entries. An optional categoryId querystring parameters
can bet set to filter results by certain blog category.
OutPut:
``` C#
	[
		{
			int Id
			int Category
			string Title
			DateTime Created
			string Author
			string Data
			string Editor
			DateTime? Edited
			string Revision
			string Status
		}
		...
	]
```

##  /Blog/{id}
GET Method
Returns a blog entry from the given id in route.
OutPut:
``` C#
	{
		int Id
		int Category
		string Title
		DateTime Created
		string Author
		string Data
		string Editor
		DateTime? Edited
		string Revision
		string Status
	}
```
##  /Blog/Approve/{id}
PUT Method
Set the status of blog entry to APPROVED. From route obtains integer with blog id
FromBody obtains a single string (json format) with the editor user name (always "klacatt", quotes must be included).
OutPut: Implements IActionResult returning 200 status code success.
		Returns  Not found if the blog is not found. 
		Returns  Bad Request if the blog id is invalid or if an exception is thrown.
		
##  /Blog/Delete/{id}
DELETE Method
Deletes the blog entry with the given id. Blog id is retrieved from path
OutPut: Implements IActionResult returning 200 status code success.
		Returns  Not found if the blog is not found. 
		Returns  Bad Request if the blog id is invalid or if an exception is thrown.

##  /Blog/Reject/{id}
Set the status of blog entry to REJECTED. From route obtains integer with blog id
FromBody obtains a single string (json format) with the editor user name (always "klacatt", quotes must be included).
OutPut: Implements IActionResult returning 200 status code success.
		Returns  Not found if the blog is not found. 
		Returns  Bad Request if the blog id is invalid or if an exception is thrown.


## Connections Strings 
The connection string is in appsettings.json, must be changed in order run the project:
```json
  "ConnectionStrings": {
    "BlogifyCnnStr": "Server=localhost;Database=DBBlogify;Trusted_Connection=True;"
  },
```

[Test API Usage](http://blogify.southcentralus.cloudapp.azure.com/webapi)

## Deploy on Windows Server
Deployment instrucctions:

- Download and install [SQL Server 2019 Express Edition][https://www.microsoft.com/en-us/download/details.aspx?id=101064]
- Execute Script DBBlogify.sql to create the database and sample data (Users, Categories, etc.).
- Check if Web Server Role is installed on Windows Server, if it's not then follow this [tutorial][https://docs.microsoft.com/en-us/iis/web-hosting/web-server-for-shared-hosting/installing-the-web-server-role] for set it up.
- Download and install [.NET Core 5 Hosting Bundle for Windows][https://dotnet.microsoft.com/download/dotnet/5.0]
- Open IIS Service Manager (Windows Administrative Tools >> Internet Information Services (IIS) Manager).
  go to Application pools and create two new app pools: Name = (NetCoreAppPool1 |NetCoreAppPool2) // .NET CLR version = No Managed Code. Click OK.
- Go to Windows Explorer and create the folder where you want to stored the applications files. For Example: C:\\MyApps\WebApp\. Put your application package in these locations
- In IIS Service Manager, under Sites and then under Default Web Site. Right click and select Add Application option.
- Set the alias for the application, choose one of the recently created NetCoreAppPool and then select the physical path you created for storing app's files. Click OK.
- Check appsettings.json for customizing the connection strings.
- Enjoy


## License
[MIT](https://choosealicense.com/licenses/mit/)