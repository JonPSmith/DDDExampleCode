// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.EfClasses;
using DataLayer.EfClasses.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EfCode.Configurations.Standard
{
    public class PriceOfferConfig : IEntityTypeConfiguration<PriceOffer>
    {
        public void Configure
            (EntityTypeBuilder<PriceOffer> entity)
        {
            entity.Property(p => p.NewPrice)
                .HasColumnType("decimal(9,2)");
        }
    }
}