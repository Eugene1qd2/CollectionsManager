﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using CollectionManager.Models.User;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;

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
    }
}
