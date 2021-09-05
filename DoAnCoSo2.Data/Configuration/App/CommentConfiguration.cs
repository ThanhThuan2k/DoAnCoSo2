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
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable(DbConstant.COMMENT_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.PostAt).HasDefaultValueSql("GETDATE()");
			builder.Property(model => model.IsHidden).HasDefaultValue(false);
			builder.Property(model => model.Like).HasDefaultValue(0);
			builder.Property(model => model.Unlike).HasDefaultValue(0);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");

			builder.HasOne(model => model.RootComment).WithMany(model => model.ReplyComments)
				.HasForeignKey(model => model.ReplyCommentID);
			builder.HasOne(model => model.Evaluated).WithMany(model => model.Comments)
				.HasForeignKey(model => model.EvaluatedID);

			builder.HasOne(model => model.Product).WithMany(model => model.Comments)
				.HasForeignKey(model => model.ProductID);
		}
	}
}
