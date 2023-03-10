# FoodOrderAPI

Food ordering APIs: Food CRUD, Order CRUD, Authentication.

## Getting Started

### Dependencies

You have to install :
* [.Net 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

[_Optional_]

* [Visual Studio Code](https://code.visualstudio.com/download)
* [SSMS](https://learn.microsoft.com/id-id/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)



### Installing
* Clone this project
* This program uses [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/), you must first install EF Core by running the following command:
```
dotnet tool install --global dotnet-ef
```
* If all requirements have been installed, run the following command: 
```
dotnet ef database update
```
Note: This command functions to update the database according to migration. The database will be automatically initialized and some records will be initialized.

* Run the following command to restores the dependencies of a project:
```
dotnet restore
```
* Run this command to build the project and all of its dependencies.
```
dotnet build
```


### Executing program:
* To run the program, run the following command
```
dotnet run
```
After the program is running, you can open [Swagger](https://localhost:7073) in a browser by accessing the following url to see the details of the program
```
https://localhost:7073
```

### Initialized users:
* username: admin; password: admin;
* username: cashier; password: cashier;
* username: waiter1; password: waiter1;
* username: waiter2; password: waiter2;

### Author
Ahmad Hadi Jaelani (Aj Hadi)

[@ajhadi](https://github.com/ajhadi)