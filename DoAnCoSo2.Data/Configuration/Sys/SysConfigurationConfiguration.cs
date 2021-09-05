using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.Sys
{
	public class SysConfigurationConfiguration : IEntityTypeConfiguration<SysConfiguration>
	{
		public void Configure(EntityTypeBuilder<SysConfiguration> builder)
		{
			builder.ToTable(DbConstant.SYSCONFIGURATION_TABLE_NAME);
			builder.HasKey(model => model.Name);
			builder.Property(model => model.Name).HasMaxLength(DbConstant.SYSCONFIGURATION_NAME_MAX_LENGTH);
			builder.Property(model => model.Value).HasMaxLength(DbConstant.SYSCONFIGURATION_VALUE_MAX_LENGTH);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
		}
	}
}
