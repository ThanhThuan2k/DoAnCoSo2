using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.Auth
{
	public class AdminConfiguration : IEntityTypeConfiguration<Admin>
	{
		public void Configure(EntityTypeBuilder<Admin> builder)
		{
			builder.ToTable(DbConstant.ADMIN_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Username).HasMaxLength(DbConstant.ADMIN_USERNAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Password).HasMaxLength(DbConstant.ADMIN_PASSWORD_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.FullName).HasMaxLength(DbConstant.ADMIN_FULLNAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Sex).HasMaxLength(DbConstant.ADMIN_SEX_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.PhoneNumber).HasMaxLength(DbConstant.ADMIN_PHONENUMBER_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Email).HasMaxLength(DbConstant.ADMIN_EMAIL_MAX_LENGTH)
				.IsUnicode();
			builder.HasIndex(model => model.Email).IsUnique();
			builder.HasIndex(model => model.PhoneNumber).IsUnique();
			builder.Property(model => model.IsBlock).HasDefaultValue(false);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.Property(model => model.IsTwoFactorEnabled).HasDefaultValue(false);
			builder.Property(model => model.AccessFailCount).HasDefaultValue(0);
			builder.Property(model => model.IsValidEmail).HasDefaultValue(false);
			builder.Property(model => model.IsValidPhoneNumber).HasDefaultValue(false);
			builder.Property(model => model.Avatar).HasMaxLength(DbConstant.ADMIN_AVATAR_MAX_LENGTH);
			builder.HasIndex(model => model.Salt).IsUnique();

			builder.HasOne(model => model.Role).WithMany(role => role.Admins)
				.HasForeignKey(model => model.RoleID);
		}
	}
}
