using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Model
{
    public class Seed
    {
        public static async Task SeedData(EventManagementContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Concert",
                        Status = true,
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Sport",
                        Status = true,
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Party",
                        Status = true,
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Food",
                        Status = true,
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Education",
                        Status = true,
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Entertainment",
                        Status = true,
                    },
                    new Category
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = "Other",
                        Status = true,
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Venues.Any())
            {
                context.Venues.AddRange(
                    new Venue
                    {
                        VenueId = Guid.NewGuid(),
                        VenueName = "Hall A",
                        Status = true,
                    },
                    new Venue
                    {
                        VenueId = Guid.NewGuid(),
                        VenueName = "Hall B",
                        Status = true,
                    },
                    new Venue
                    {
                        VenueId = Guid.NewGuid(),
                        VenueName = "Hall C",
                        Status = true,
                    },
                    new Venue
                    {
                        VenueId = Guid.NewGuid(),
                        VenueName = "Main Lobby",
                        Status = true,
                    },
                    new Venue
                    {
                        VenueId = Guid.NewGuid(),
                        VenueName = "Side Lobby",
                        Status = true,
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}