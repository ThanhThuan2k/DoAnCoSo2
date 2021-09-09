using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.Sys
{
	public class SysErrorConfiguration : IEntityTypeConfiguration<SysError>
	{
		public void Configure(EntityTypeBuilder<SysError> builder)
		{
			builder.ToTable(DbConstant.SYSERROR_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.ErrorName).HasMaxLength(DbConstant.SYSERROR_ERRORNAME_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
			builder.HasIndex(model => model.ErrorCode).IsUnique();
		}
	}
}
