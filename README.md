# Blogify
Blogify is a micro blog engine developed in .NET Core 5. This project was made with 
Microsoft Visual Studio Community 2019 Versión 16.8.4.

## Database
This project runs with MS SQL Server 2019 Express. Please run: Script DBBlogify.sql file 
to create the database with some sample data.

## Testing Credentials
These are the software credentials for testing purposes:

| Username | Password | Role |
| ------ | ------ | ------ |
| blogger1 | Back4g00d | Writer |
| blogger2 | Back4g00d | Writer |
| klacatt | Back4g00d | Editor |

## BlogifyWebApp

Is a ASP.NET Core MVC 5 Web Application for handling Editor's operations.

Nuget Packages Required:
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation Version 5.0.2
Microsoft.EntityFrameworkCore Version 5.0.2
Microsoft.EntityFrameworkCore.SqlServer Version 5.0.2
Microsoft.EntityFrameworkCore.Tools Version 5.0.2

Connections Strings must in appsettings.json
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

##  Available Endpoints
[/Category/List]
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

[/Blog/List[?categoryId=X]]
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

[/Blog/{id}]
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
[/Blog/Approve/{id}]
PUT Method
Set the status of blog entry to APPROVED. From route obtains integer with blog id
FromBody obtains a single string (json format) with the editor user name (always "klacatt", quotes must be included).
OutPut: Implements IActionResult returning 200 status code success.
		Returns  Not found if the blog is not found. 
		Returns  Bad Request if the blog id is invalid or if an exception is thrown.
		
[/Blog/Delete/{id}]
DELETE Method
Deletes the blog entry with the given id. Blog id is retrieved from path
OutPut: Implements IActionResult returning 200 status code success.
		Returns  Not found if the blog is not found. 
		Returns  Bad Request if the blog id is invalid or if an exception is thrown.

[/Blog/Reject/{id}]
Set the status of blog entry to REJECTED. From route obtains integer with blog id
FromBody obtains a single string (json format) with the editor user name (always "klacatt", quotes must be included).
OutPut: Implements IActionResult returning 200 status code success.
		Returns  Not found if the blog is not found. 
		Returns  Bad Request if the blog id is invalid or if an exception is thrown.


Connections Strings is in appsettings.json, must be changed in order run project:
```json
  "ConnectionStrings": {
    "BlogifyCnnStr": "Server=localhost;Database=DBBlogify;Trusted_Connection=True;"
  },
```

[Test API Usage](http://blogify.southcentralus.cloudapp.azure.com/webapi)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)