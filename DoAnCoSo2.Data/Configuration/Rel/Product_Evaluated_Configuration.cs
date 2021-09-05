using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Rel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.Rel
{
	public class Product_Evaluated_Configuration : IEntityTypeConfiguration<Product_Evaluated>
	{
		public void Configure(EntityTypeBuilder<Product_Evaluated> builder)
		{
			builder.ToTable(DbConstant.PRODUCT_EVALUATED_TABLE_NAME);
			builder.Property(model => model.Quantity).HasDefaultValue(0);
			builder.HasKey(model => new { model.ProductID, model.EvaluatedID });

			builder.HasOne(model => model.Product).WithMany(product => product.Evaluated)
				.HasForeignKey(model => model.EvaluatedID);
			builder.HasOne(model => model.Evaluated).WithMany(sysEvaluated => sysEvaluated.Products)
				.HasForeignKey(model => model.ProductID);
		}
	}
}
