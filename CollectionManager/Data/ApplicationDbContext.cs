using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using CollectionManager.Models.User;
using CollectionManager.Models.Collection;

namespace CollectionManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EntireUserViewModel>? EntireUserViewModel { get; set; }
        public DbSet<UserCollectionModel>? UserCollections { get; set; }
    }
}
