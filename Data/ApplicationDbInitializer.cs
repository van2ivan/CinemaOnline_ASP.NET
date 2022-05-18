using CinemaOnline.Data.Static;
using CinemaOnline.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaOnline.Data.Enums
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                        {
                            new Cinema()
                            {
                                Name = "Movie time",
                                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJdW3nTn5RpvLhDcAqsS8LKbyP65sAyJooyw&usqp=CAU",
                                Description = "Located in the downtown"
                            },
                            new Cinema()
                            {
                                Name = "Royal cinema",
                                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSTxNhERingiZ9fTQhIQZaU59B6XiePzRzkmg&usqp=CAU",
                                Description = "Has the greatest design"
                            },
                        });
                    context.SaveChanges();
                }
                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(new List<Company>()
                        {
                            new Company()
                            {
                                Name = "Paramount",
                                Picture = "https://1757140519.rsc.cdn77.org/blog/wp-content/uploads/sites/2/2020/03/the-1st-logo-2-1024x940.png",
                                Description = "Located in Los Angeles and is a member of Paramount global"
                            },
                            new Company()
                            {
                                Name = "20th century studios",
                                Picture = "https://yt3.ggpht.com/ytc/AKedOLSRgxni4D09Z9qq---RtA1rscQ32Kh9oTXB-GI0sME=s900-c-k-c0x00ffffff-no-rj",
                                Description = "Founded in 1935"
                            }
                        });
                    context.SaveChanges();

                }
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                        {
                            new Actor()
                            {
                                FullName = "Daniel Craig",
                                ProfilePicture = "https://upload.wikimedia.org/wikipedia/commons/8/87/Daniel_Craig_in_2021.jpg",
                                Biography = "He was born in 1968 in Chester, Great Britain..."
                            },
                            new Actor()
                            {
                                FullName = "Merlin Monroe",
                                ProfilePicture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUfZP6kmb1D0wiWMtHjyd9AmPLJPVMr8OM1g&usqp=CAU",
                                Biography = "The famoust actress of 1950s"
                            }
                        });
                    context.SaveChanges();
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                        {
                            new Movie()
                            {
                                Name = "Skyfall",
                                Picture = "https://static.posters.cz/image/750/%D0%9F%D0%BB%D0%B0%D0%BA%D0%B0%D1%82%D0%B8/james-bond-007-skyfall-one-sheet-white-i13182.jpg",
                                Description = "First movie of James Bond series",
                                Price = 9.90,
                                Category = Category.Action,
                                CinemaId = 2,
                                CompanyId = 2,
                                StartDate = DateTime.Now.AddDays(12),
                                EndDate = DateTime.Now.AddDays(19)


                            },
                            new Movie()
                            {
                                Name = "Some like it hot",
                                Picture = "https://c8.alamy.com/comp/A37W2Y/some-like-it-hot-poster-for-1959-ua-film-starring-marilyn-monroe-A37W2Y.jpg",
                                Description = "The famoust movie with Merlin Monroe",
                                Price = 7.90,
                                Category = Category.Comedy,
                                CinemaId = 1,
                                CompanyId = 1,
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now.AddDays(7)
                            }
                        });
                    context.SaveChanges();
                }

            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminEmail = "admin@cinema.com";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdmin = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdmin, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
                }


                string userEmail = "user@cinema.com";

                var appUser = await userManager.FindByEmailAsync(userEmail);
                if (appUser == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = userEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
