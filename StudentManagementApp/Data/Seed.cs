using StudentManagementApp.Models;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Students.Any())
                {
                    context.Students.AddRange(new List<StudentModel>()
                    {
                        new()
                        {
                            FirstName = "Teddy",
                            LastName = "Smith",
                            Email = "teddysmith@fun-university.com",
                            PhoneNumber = "704-555-5555",
                            Address = new Address()
                            {
                                Country = "USA",
                                City = "Charlotte",
                                Street = "Main St",
                                Number = 123
                            },
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
                        },
                        new()
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "johndoe@-fun-university.com",
                            PhoneNumber = "704-555-5555",
                            Address = new Address()
                            {
                                Country = "USA",
                                City = "Charlotte",
                                Street = "Main St",
                                Number = 124
                            },
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
                        },
                        new()
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            Email = "janedoe@fun-iniversity.com",
                            PhoneNumber = "704-555-5555",
                            Address = new Address()
                            {
                                Country = "USA",
                                City = "Charlotte",
                                Street = "Main St",
                                Number = 125
                            },
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
                        },
                        new()
                        {
                            FirstName = "Bob",
                            LastName = "Smith",
                            Email = "bobsmith@fun-univsity.com",
                            PhoneNumber = "704-555-5555",
                            Address = new Address()
                            {
                                Country = "USA",
                                City = "Charlotte",
                                Street = "Main St",
                                Number = 126
                            },
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
                        }
                    });
                    context.SaveChanges();
                }

            }
        }

    }
}