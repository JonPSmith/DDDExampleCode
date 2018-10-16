// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.EfClasses.PocoOnly;
using DataLayer.EfCode.Configurations.PocoOnly;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfCode
{
    public class PocoOnlyDbContext : DbContext
    {
        public PocoOnlyDbContext(DbContextOptions<PocoOnlyDbContext> options)      
            : base(options) {}

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void
            OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());       
            modelBuilder.ApplyConfiguration(new BookAuthorConfig());
            modelBuilder.ApplyConfiguration(new LineItemConfig());   
        }
    }
}

