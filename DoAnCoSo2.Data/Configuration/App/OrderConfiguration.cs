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
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable(DbConstant.ORDER_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.OrderTime).HasDefaultValueSql("GETDATE()");
			builder.HasOne(model => model.Customer).WithMany(cus => cus.Orders)
				.HasForeignKey(model => model.CustomerID);
			builder.Property(model => model.Total).HasDefaultValue(0);
		}
	}
}
