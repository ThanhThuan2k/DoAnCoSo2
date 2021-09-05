using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoAnCoSo2.Data.Configuration.Sys
{
	public class SysEvaluatedConfiguration : IEntityTypeConfiguration<SysEvaluated>
	{
		public void Configure(EntityTypeBuilder<SysEvaluated> builder)
		{
			builder.ToTable(DbConstant.SYSEVALUATED_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");

		}
	}
}
