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
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable(DbConstant.PRODUCT_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.ProductName).HasMaxLength(DbConstant.PRODUCT_NAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Material).HasMaxLength(DbConstant.PRODUCT_MATERIAL_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.Property(model => model.Like).HasDefaultValue(0);

			builder.HasOne(model => model.Shop).WithMany(shop => shop.Products)
				.HasForeignKey(model => model.ShopID);
			builder.HasOne(model => model.Brand).WithMany(brand => brand.Products)
				.HasForeignKey(model => model.BrandID);
			builder.HasOne(model => model.Category).WithMany(category => category.Products)
				.HasForeignKey(model => model.CategoryID);
		}
	}
}
