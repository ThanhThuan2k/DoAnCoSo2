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
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.ToTable(DbConstant.BRAND_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Name).HasMaxLength(DbConstant.BRAND_NAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Avatar).HasMaxLength(DbConstant.BRAND_AVATAR_MAX_LENGTH);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.Property(model => model.Avatar).HasDefaultValueSql("NULL");
		}
	}
}
