using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.App
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			builder.ToTable(DbConstant.ORDERDETAIL_TABLE_NAME);
			builder.HasKey(model => new { model.OrderID, model.ShopID, model.ProductID });
			builder.Property(model => model.Quantity).HasDefaultValue(1);
			builder.Property(model => model.Paid).HasDefaultValue(false);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.Property(model => model.Total).HasDefaultValue(0);

			builder.HasOne(model => model.Status).WithMany(status => status.OrderDetails)
				.HasForeignKey(model => model.StatusID);
			builder.HasOne(model => model.Order).WithMany(order => order.OrderDetails)
				.HasForeignKey(model => model.OrderID);
			builder.HasOne(model => model.Shop).WithMany(model => model.OrderDetails)
				.HasForeignKey(model => model.ShopID);
			builder.HasOne(model => model.Product).WithMany(model => model.OrderDetails)
				.HasForeignKey(model => model.ProductID);
		}
	}
}
