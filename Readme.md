# DDDExampleCode

This is some code to go with my article [Three approaches to Domain-Driven Design with Entity Framework Core](https://www.thereformedprogrammer.net/three-approaches-to-domain-driven-design-with-entity-framework-core/)
and my talk "Using Domain-Driven Design in ASP.NET Core/Entity Framework Core applications". 

This contains some examples of Entity Framework Core (EF Core) classes, all mapped to (nearly) the same database.

## The three types of EF Core classes 

### 0. Standard EF Core classes

These are classes with public getters and setters. You can find these in:
- [Entity classes](https://github.com/JonPSmith/DDDExampleCode/tree/master/DataLayer/EfClasses/Standard).
- [EF Core configuration classes](https://github.com/JonPSmith/DDDExampleCode/tree/master/DataLayer/EfCode/Configurations/Standard).
- The [DbContext](https://github.com/JonPSmith/DDDExampleCode/blob/master/DataLayer/EfCode/StandardDbContext.cs) class.

### 1. POCO-only EF Core classes

These are classes with private setters and specific constructors. Also see that the classes don't show the primary keys or foreign keys. 
This requires more work at the configuration level to set up the relationships via shadow properties.  You can find these in:
- [Entity classes](https://github.com/JonPSmith/DDDExampleCode/tree/master/DataLayer/EfClasses/PocoOnly).
- [EF Core configuration classes](https://github.com/JonPSmith/DDDExampleCode/tree/master/DataLayer/EfCode/Configurations/PocoOnly).
- The [DbContext](https://github.com/JonPSmith/DDDExampleCode/blob/master/DataLayer/EfCode/PocoOnlyDbContext.cs) class.

NOTE: I didn't quite get the format of the Review table correct. It has an extra column called "BookId1" which is nullable - that shouldn't be there.

I have also created a very simple [Repository class](https://github.com/JonPSmith/DDDExampleCode/blob/master/PocoRepository/Repository.cs)
to show how it would look.

### 2. CrUD-only EF Core classes

These are classes with private setters specific constructors, but don't need a repository. You can find these in:
- [Entity classes](https://github.com/JonPSmith/DDDExampleCode/tree/master/DataLayer/EfClasses/CrUDOnly).
- [EF Core configuration classes](https://github.com/JonPSmith/DDDExampleCode/tree/master/DataLayer/EfCode/Configurations/CrUDOnly).
- The [DbContext](https://github.com/JonPSmith/DDDExampleCode/blob/master/DataLayer/EfCode/CrUDOnlyDbContext.cs) class.

### 3. CrUD and business logic

I didn't create these versions of the EF Core classes. 


## Other parts of the repo

The RazorPageApp and The ServiceLayer use the CrUD-only EF Core classes 
provides a working example of using the CrUD-only approach to DDD. 
It uses a Sqlite in-memory database, with automatic seeding with data to allow you to run the application anywhere.
It uses the with the [EfCore.GenericServices](https://www.nuget.org/packages/EfCore.GenericServices/) library,
which you can find out more about in the repo's [readme](https://github.com/JonPSmith/EfCore.GenericServices/blob/master/README.md) file.