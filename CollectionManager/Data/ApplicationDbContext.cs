using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using CollectionManager.Models.User;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;
using CollectionManager.Models.Socials;
using CollectionManager.Models.Fulltext;

namespace CollectionManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EntireUserViewModel>? AppUsers { get; set; }
        public DbSet<EntireCollectionViewModel>? UserCollections { get; set; }
        public DbSet<EntireItemViewModel>? CollectionItems { get; set; }
        public DbSet<TagModel>? Tags { get; set; }
        public DbSet<ItemTagModel>? ItemTags { get; set; }
        public DbSet<CommentModel>? ItemComments { get; set; }
        public DbSet<LikeModel>? ItemLikes { get; set; }
        public DbSet<FulltextItem>? FulltextStrings { get; set; }

        [DbFunction(name:"SOUNDEX",IsBuiltIn =true)]
        public string FuzzySearch(string query)
        {
            throw new NotImplementedException();
        }
    }
}
