using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        #region [- Configure -]
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                   .ValueGeneratedNever();

            builder.Property(o => o.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

            builder.Property(o => o.FailureReason)
                   .HasMaxLength(500);

            builder.Ignore(o => o.TotalAmount); 

           
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(i => i.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(o => o.Items)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        } 
        #endregion
    }
}
