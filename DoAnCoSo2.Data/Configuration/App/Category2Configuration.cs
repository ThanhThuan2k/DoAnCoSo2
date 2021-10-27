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
	public class Category2Configuration : IEntityTypeConfiguration<Category2>
	{
		public void Configure(EntityTypeBuilder<Category2> builder)
		{
			builder.ToTable(DbConstant.CATEGORY2_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.HasOne(model => model.Category).WithMany(model => model.Categories2)
				.HasForeignKey(model => model.ParentCategoryId);
		}
	}
}
