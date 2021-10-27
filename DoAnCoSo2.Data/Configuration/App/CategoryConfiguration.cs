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
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable(DbConstant.CATEGORY_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Name).HasMaxLength(DbConstant.CATEGORY_NAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Avatar).HasMaxLength(DbConstant.CATEGORY_AVATAR_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
		}
	}
}
