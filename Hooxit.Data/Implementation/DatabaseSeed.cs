using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hooxit.Data.Implementation
{
    public class DatabaseSeed
    {
        public async void SeedDB(IApplicationBuilder applicationBuilder)
        {
            using (var context = applicationBuilder.ApplicationServices.GetRequiredService<HooxitDbContext>())
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (await roleStore.FindByNameAsync("Candidate") == null)
                {
                    await roleStore.CreateAsync(new IdentityRole() { Name = "Candidate", NormalizedName = "Candidate" });
                }

                if (await roleStore.FindByNameAsync("Company") == null)
                {
                    await roleStore.CreateAsync(new IdentityRole() { Name = "Company", NormalizedName = "Company" });
                }

                roleStore.Context.SaveChanges();
            }
        }
    }
}