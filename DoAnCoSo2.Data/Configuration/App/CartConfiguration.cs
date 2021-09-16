using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.App
{
	public class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable(DbConstant.CART_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.HasOne(model => model.Customer).WithMany(model => model.Carts)
				.HasForeignKey(model => model.CustomerId);
			builder.HasOne(model => model.Product).WithMany(model => model.Carts)
				.HasForeignKey(model => model.ProductId);
			builder.Property(model => model.Color).HasDefaultValueSql("NULL");
			builder.Property(model => model.Classification).HasDefaultValueSql("NULL");
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
		}
	}
}
