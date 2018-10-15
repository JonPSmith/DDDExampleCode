// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.EfClasses.CrUDOnly;
using DataLayer.EfCode.Configurations.CrUDOnly;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfCode
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(
            DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } //#A
        public DbSet<Order> Orders { get; set; }

        protected override void
            OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new LineItemConfig());

            modelBuilder.Ignore<Review>(); 
            modelBuilder.Ignore<Author>();
            modelBuilder.Ignore<BookAuthor>(); 
        }
    }
}