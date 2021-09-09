using DoAnCoSo2.Data.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.App
{
	public class Customer_Shop_MessageConfiguration : IEntityTypeConfiguration<Customer_Shop_Message>
	{
		public void Configure(EntityTypeBuilder<Customer_Shop_Message> builder)
		{
			builder.ToTable(DbConstant.CUSTOMER_SHOP_MESSAGE_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.IsSeen).HasDefaultValue(false);
			builder.Property(model => model.SendTime).HasDefaultValueSql("GETDATE()");

			builder.HasOne(model => model.Customer).WithMany(model => model.Messages_Shop)
				.HasForeignKey(model => model.CustomerID);
			builder.HasOne(model => model.Shop).WithMany(model => model.Messages_Shop)
				.HasForeignKey(model => model.ShopID);
		}
	}
}
