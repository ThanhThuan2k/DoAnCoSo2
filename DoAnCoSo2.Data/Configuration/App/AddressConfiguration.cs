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
	public class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable(DbConstant.ADDRESS_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.FullName).HasMaxLength(DbConstant.ADDRESS_FULLNAME_MAX_LENGTH)
				.IsRequired();
			builder.Property(model => model.PhoneNumber).HasMaxLength(DbConstant.ADDRESS_PHONENUMBER_MAX_LENGTH);
			builder.Property(model => model.ReceiverAddress).HasMaxLength(DbConstant.ADDRESS_ADDRESS_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");

			builder.HasOne(model => model.Customer).WithMany(model => model.Addresses)
				.HasForeignKey(model => model.CustomerID);
		}
	}
}
