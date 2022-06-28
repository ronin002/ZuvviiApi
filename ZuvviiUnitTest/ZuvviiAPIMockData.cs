using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZuvviiAPI.Data;
using ZuvviiAPI.Dtos;
using ZuvviiAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ZuvviiUnitTest
{
    public class ZuvviiAPIMockData
    {
        public static async Task CreateUsers(ZuvviiApiApplication application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var zuvviiDbContext = provider.GetRequiredService<DataContext>())
                {
                    await zuvviiDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await zuvviiDbContext.Users.AddAsync(new User
                        {   Email = "edson132@gmail.com", 
                            UserName = "Ed", 
                            Password = "9d8ef720c62181a3a8cea3256cbf244fa42357c112358e71a2b6aea8853df928", 
                             });


                        await zuvviiDbContext.Users.AddAsync(new User
                        {
                            Email = "felipe132@gmail.com",
                            UserName = "Lipas",
                            Password = "9d8ef720c62181a3a8cea3256cbf244fa42357c112358e71a2b6aea8853df928",
                        });

                        await zuvviiDbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
