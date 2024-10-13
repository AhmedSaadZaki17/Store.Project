using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Repository.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(200).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired(true);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(P => P.Type)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
