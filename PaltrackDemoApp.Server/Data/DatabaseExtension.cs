using PaltrackDemoApp.Server.Models;

namespace PaltrackDemoApp.Server.Data
{
    public static class DatabaseExtension
    {
        //Method for seeding initial data into the database
        public static void SeedData(this IServiceProvider services)
        {
            try
            {
                //Create a new scope for database operations
                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //Ensure database exists otherwise one will be created
                db.Database.EnsureCreated();

                //Check if there are no persons in the database before seeding
                if (!db.Persons.Any())
                {
                    //Array of data to be seeded
                    var mockData = new[] {
                        new {
                            Name = "Orla",
                            Surname = "Gates",
                            Email = "praesent.eu@outlook.net",
                            Country = "Germany",
                            Region = "Guanacaste"
                        },
                        new {
                            Name = "Sade",
                            Surname = "Moreno",
                            Email = "tincidunt.orci@protonmail.ca",
                            Country = "Germany",
                            Region = "Guanacaste"
                        },
                        new {
                            Name = "Fulton",
                            Surname = "Lyons",
                            Email = "consectetuer.adipiscing.elit@google.net",
                            Country = "India",
                            Region = "Southwestern Tagalog Region"
                        },
                        new {
                            Name = "Phillip",
                            Surname = "Rhodes",
                            Email = "at.libero@protonmail.org",
                            Country = "India",
                            Region = "Tasmania"
                        },
                        new {
                            Name = "Lee",
                            Surname = "Riddle",
                            Email = "massa.rutrum.magna@hotmail.ca",
                            Country = "Spain",
                            Region = "Ancash"
                        },
                        new {
                            Name = "Austin",
                            Surname = "Miranda",
                            Email = "rutrum@aol.couk",
                            Country = "Germany",
                            Region = "Bavaria"
                        },
                        new {
                            Name = "Lydia",
                            Surname = "Martinez",
                            Email = "eros.nec@google.com",
                            Country = "Australia",
                            Region = "Coquimbo"
                        },
                        new {
                            Name = "Constance",
                            Surname = "Deleon",
                            Email = "non.luctus.sit@yahoo.net",
                            Country = "United States",
                            Region = "North Region"
                        },
                        new {
                            Name = "Hillary",
                            Surname = "Peters",
                            Email = "dictum@protonmail.com",
                            Country = "Australia",
                            Region = "Małopolskie"
                        },
                        new {
                            Name = "Jocelyn",
                            Surname = "Herman",
                            Email = "sagittis@hotmail.org",
                            Country = "Germany",
                            Region = "Bavaria"
                        }
                    };

                    //Add mock data into database
                    foreach (var person in mockData)
                    {
                        db.Persons.Add(new Person
                        {
                            Name = person.Name,
                            Surname = person.Surname,
                            Email = person.Email,
                            Country = person.Country,
                            Region = person.Region
                        });
                    }

                    //Save to database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}