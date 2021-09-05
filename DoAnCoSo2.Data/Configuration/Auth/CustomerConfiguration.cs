using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.Auth
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable(DbConstant.CUSTOMER_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Email).HasMaxLength(DbConstant.CUSTOMER_EMAIL_MAX_LENGTH);
			builder.Property(model => model.FullName).HasMaxLength(DbConstant.CUSTOMER_FULLNAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Sex).HasMaxLength(DbConstant.CUSTOMER_SEX_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.PhoneNumber).HasMaxLength(DbConstant.CUSTOMER_PHONENUMBER_MAX_LENGTH);
			builder.Property(model => model.Username).HasMaxLength(DbConstant.CUSTOMER_USERNAME_MAX_LENGTH);
			builder.Property(model => model.Password).HasMaxLength(DbConstant.CUSTOMER_PASSWORD_MAX_LENGTH);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
		}
	}
}
