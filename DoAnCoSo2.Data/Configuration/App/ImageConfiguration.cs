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
	public class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder.ToTable(DbConstant.IMAGE_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Url).HasMaxLength(DbConstant.IMAGE_URL_MAX_LENGTH);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");

			builder.HasOne(model => model.Product).WithMany(product => product.Images)
				.HasForeignKey(model => model.ProductID);

			builder.HasOne(model => model.Comment).WithMany(comment => comment.Images)
				.HasForeignKey(model => model.CommentID);
		}
	}
}
