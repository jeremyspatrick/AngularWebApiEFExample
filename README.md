# Example Angular 1.3 EntityFramework 6 app
## 1. Setting up the Database

Check the DB connection string in the App.config and change the DataSource to your PC name (if local)
 
In the Package Manager Console select the Models project and run these command

	Enable-Migrations -ContextTypeName SingleApp.Models.SingleAppContextCustom -Force

	Add-Migration init

	Update-Database

## 2. Set up WebAPI

The project is configured for local IIS. To run on local IIS create a website that points to App. Under that website create an Application called api that points to the Api folder (WebAPI). Example api calls can be found in postman.json 


## 3. Example Api calls

	http://localhost/SingleApp/Api/User/Registration
	{
	  "Username" : "dude2", 
	  "Password" : "notmypassword", 
	  "Email" : "notmybag@baby.com",
	  "UserDetail": 
	  {
		"Firstname" : "Jeremy", 
		"Lastname" : "Patrick",
		"Street" : "Jeremy", 
		"City" : "Patrick",
		"State" : "Jeremy", 
		"Zip" : "Patrick"
	  }
	}
	
	http://localhost/SingleApp/Api/Authentication/Login
	{
	  "Username" : "dude2", 
	  "Password" : "notmypassword", 
	}
	
## 4. License

[The MIT License (MIT)](http://www.opensource.org/licenses/mit-license.html)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.