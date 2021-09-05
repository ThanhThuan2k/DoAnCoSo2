using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.App
{
	public class ShopConfiguration : IEntityTypeConfiguration<Shop>
	{
		public void Configure(EntityTypeBuilder<Shop> builder)
		{
			builder.ToTable(DbConstant.SHOP_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.CreateDate).HasDefaultValueSql("GETDATE()");
			builder.Property(model => model.Address).HasMaxLength(DbConstant.SHOP_ADDRESS_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Avatar).HasMaxLength(DbConstant.SHOP_AVATAR_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.ShopUri).HasMaxLength(DbConstant.SHOP_URI_MAX_LENGTH);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.HasOne(model => model.Customer).WithOne(customer => customer.Shop)
				.HasForeignKey<Shop>(model => model.OwnerID);
		}
	}
}
