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
	public class Customer_Admin_MessageConfiguration : IEntityTypeConfiguration<Customer_Admin_Message>
	{
		public void Configure(EntityTypeBuilder<Customer_Admin_Message> builder)
		{
			builder.ToTable(DbConstant.CUSTOMER_ADMIN_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.IsSeen).HasDefaultValue(false);
			builder.Property(model => model.SendTime).HasDefaultValueSql("GETDATE()");

			builder.HasOne(model => model.Customer).WithMany(model => model.Messages_Admin)
				.HasForeignKey(model => model.CustomerID);
			builder.HasOne(model => model.Admin).WithMany(model => model.Messages_Customer)
				.HasForeignKey(model => model.AdminID);
		}
	}
}
