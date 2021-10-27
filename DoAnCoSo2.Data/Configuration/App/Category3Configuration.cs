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
	public class Category3Configuration : IEntityTypeConfiguration<Category3>
	{
		public void Configure(EntityTypeBuilder<Category3> builder)
		{
			builder.ToTable(DbConstant.CATEGORY3_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.HasOne(model => model.Category2).WithMany(model => model.Categories3)
				.HasForeignKey(model => model.ParentCategory2Id);
		}
	}
}
