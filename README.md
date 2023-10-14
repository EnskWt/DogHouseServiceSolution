# DogHouseService
## Features
* ASP.NET Core 7.0 Web API
* Entity Framework Core (Code-first)
* Local MS SQL Database
* Swagger
* Clean Architecture
## Installation
1. Clone the repository:
```
git clone https://github.com/EnskWt/DogHouseServiceSolution.git
```
2. Create database using NuGet Package Manager Console:
```
Update-Database
```
3. Specify the environment (use Development for Swagger)
4. Run project and wait for Swagger page.
## Some additional info
* The project contains ExceptionHandlingMiddleware, which is responsible for catching all Exceptions when the application is running. It works in such a way that if an error occurs in the application, this middleware turns it into an object of type ErrorDetails and returns it as a JSON file with a code and error message.
* As for validation errors, when such errors occur, the application returns a BadRequest with error messages.
* According to the purpose, the field “Name” in the entity “Dog” is unique. Therefore, the Name column of the Dogs table is a unique index, and if you try to create a dog with an already taken name, a DbUpdateException will be thrown. However, the check whether the name is taken happens at the service level, so this is only a double protection (at the service level and at the database level).
