using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HoukaifaBlog.Infrastructure.Database
{
    public class MyContextSeed
    {
        public static async Task SeedAsync(MyContext myContext, ILoggerFactory loggerFactory, int retry = 1)
        {
            int retryForAvailability = retry;

            try
            {
                // TODO: Only run this if using a real database
                // myContext.Database.Migrate();

                if (!myContext.Posts.Any())
                {
                    myContext.Posts.AddRange(
                        new List<Post>
                        {
                            new Post
                            {
                                Title = "Post Title 1",
                                Body = "Post Body 1",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Post Title 2",
                                Body = "Post Body 2",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Post Title 3",
                                Body = "Post Body 3",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Post Title 4",
                                Body = "Post Body 4",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Post Title 5",
                                Body = "Post Body 5",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Post Title 6",
                                Body = "Post Body 6",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Post Title 7",
                                Body = "Post Body 7",
                                Author = "Sunwish",
                                LastModified = DateTime.Now
                            }
                        }
                    );
                    await myContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<MyContextSeed>();
                    logger.LogError(e.Message);
                    await SeedAsync(myContext, loggerFactory, retryForAvailability);
                }
            }

        }
    }
}
