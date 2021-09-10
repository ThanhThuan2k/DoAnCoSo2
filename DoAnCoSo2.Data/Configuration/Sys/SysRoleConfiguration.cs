using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.Sys
{
	public class SysRoleConfiguration : IEntityTypeConfiguration<SysRoles>
	{
		public void Configure(EntityTypeBuilder<SysRoles> builder)
		{
			// Data constraint
			builder.ToTable(DbConstant.SYSROLE_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Role).HasMaxLength(DbConstant.SYSROLE_ROLENAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.HasIndex(model => model.RoleCode).IsUnique();
		}
	}
}
