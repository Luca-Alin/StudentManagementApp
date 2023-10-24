**Student Management Application**

**Project Details**

- **Target Framework:** .NET 7.0
- **Programming Language:** C#
- **Project Template:** ASP.NET Core Web API
- **Development Environment:** JetBrains Rider on Ubuntu 23.04
- **Database Compatibility:** Microsoft SQL Server and MySQL (additional databases can be added by including their NuGet
  dependencies, configuring the connection string in the `appsettings.json` file, and updating
  the `builder.Services.AddDbContext` line in `Program.cs`)

**Key Features**

- Add, remove, and update student records.
- Add, remove, and update faculty information.
- Add or remove courses.
- A student can be part of multiple faculties.
- Ability to update grades.
- Students can log in using their email, review their personal details, and access their grade information.
- Stay tuned for more features in future updates!

**How to Run**

1. Set up your database (following the steps outlined above).
   (optional) Remove "Seed.SeedData(app);" from `Program.cs` if you don't want sample records in the database.
2. Run the following command:
   ```
   (terminal) dotnet ef database update
   ```

**Enhancing Features**

- **Models**: Modifying the models won't directly affect the database but might impact the application's functionality.
  To address this, run these terminal commands:
   ```
   dotnet ef database drop
   dotnet ef migrations remove
   dotnet ef migrations add [your_migration_name]
   dotnet ef database update
   ```
  **Keep in mind that these steps will remove all data in the database. Instructions on data preservation during table
  updates are forthcoming.**

- **Controllers**: Changes to controllers won't directly alter the database but could affect frontend functionality.
  Ensure a solid grasp of HTTP protocols and the Axios library before making any controller changes.

- **Repositories**: Feel free to modify repositories as needed, but remember that unless you implement the changes in
  the repository interfaces, they won't be visible in the controllers. The controllers should reference the interfaces,
  not the repositories themselves.

**You're All Set!**
Now you can launch the application and test it using Postman or any other API testing tool. Manage your university's
online catalog with ease!