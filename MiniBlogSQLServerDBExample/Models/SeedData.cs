using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MiniBlogSQLServerDBExample.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcPostContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcPostContext>>()))
            {
                // Look for any movies.
                if (context.Post.Any())
                {
                    return;   // DB has been seeded
                }

                context.Post.AddRange(
                    new Post
                    {
                        Title = "Post 1",
                        PostDate = DateTime.Parse("2019-02-03"),
                        AuthName = "Imran Khan",
                        PostContent = "First Post Content"
                    },
                    new Post
                    {
                        Title = "Post 2",
                        PostDate = DateTime.Parse("2019-2-02"),
                        AuthName = "Imran Khan",
                        PostContent = "Second Post Content"
                    },
                    new Post
                    {
                        Title = "Post 3",
                        PostDate = DateTime.Parse("2019-2-03"),
                        AuthName = "Imran Khan",
                        PostContent = "Third Post Content"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
