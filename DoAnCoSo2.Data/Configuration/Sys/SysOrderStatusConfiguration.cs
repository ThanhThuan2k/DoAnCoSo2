using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.Sys
{
	public class SysOrderStatusConfiguration : IEntityTypeConfiguration<SysOrderStatus>
	{
		public void Configure(EntityTypeBuilder<SysOrderStatus> builder)
		{
			builder.ToTable(DbConstant.SYSORDERSTATUS_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Status).HasMaxLength(DbConstant.SYSORDERSTATUS_STATUS_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
		}
	}
}
