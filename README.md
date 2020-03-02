# AddressBook

To use application change connection string in appsettings.json to match your database connection.

After that run migrations to create database. 

  -From package manager console run command Update-Database on project AddressBook.Data
  
  -From cmd navigate to root folder where solution file is and run command 
  
        dotnet ef database update --project AddressBook.Data
   
