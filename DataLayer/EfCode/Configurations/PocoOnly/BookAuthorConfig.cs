// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.EfClasses.PocoOnly;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EfCode.Configurations.PocoOnly
{
    internal class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure
            (EntityTypeBuilder<BookAuthor> entity)
        {
            entity.Property<int>("BookId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .HasAnnotation("Key", 0);
            entity.Property<int>("AuthorId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .HasAnnotation("Key", 0);


            //-----------------------------
            //Relationships

            entity.HasOne(pt => pt.Book)        
                .WithMany(p => p.AuthorsLink);

            entity.HasOne(pt => pt.Author)        
                .WithMany(t => t.BooksLink);
        }
    }
}