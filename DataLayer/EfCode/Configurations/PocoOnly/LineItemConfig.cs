// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.EfClasses.PocoOnly;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EfCode.Configurations.PocoOnly
{
    public class LineItemConfig : IEntityTypeConfiguration<LineItem>
    {
        public void Configure
            (EntityTypeBuilder<LineItem> entity)
        {
            entity.HasOne(p => p.ChosenBook)
                .WithMany()
                .HasForeignKey("ChosenBookId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.LinkedOrder)
                .WithMany()
                .HasForeignKey("LinkedOrderId")
                .IsRequired();
        }
    }
}