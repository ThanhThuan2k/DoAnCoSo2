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
	public class SysNotificationConfiguration : IEntityTypeConfiguration<SysNotification>
	{
		public void Configure(EntityTypeBuilder<SysNotification> builder)
		{
			builder.ToTable(DbConstant.SYSNOTIFICATION_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.Description).HasMaxLength(DbConstant.SYSNOTIFICATION_DESCRIPTION_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.Message).HasMaxLength(DbConstant.SYSNOTIFICATION_MESSAGE_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");
		}
	}
}
