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
	public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.ToTable(DbConstant.NOTIFICATION_TABLE_NAME);
			builder.HasKey(model => model.Id);
			builder.Property(model => model.NotificationBody).HasMaxLength(DbConstant.NOTIFICATION_NOTIFICATIONBODY_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.NotificationHeader).HasMaxLength(DbConstant.NOTIFICATION_NOTIFICATIONHEADER_MAX_LENGTH)
				.IsUnicode();
			builder.Property(model => model.IsRead).HasDefaultValue(false);
			builder.Property(model => model.Url).HasMaxLength(DbConstant.NOTIFICATION_URL_MAX_LENGTH);
			builder.Property(model => model.CreateAt).HasDefaultValueSql("GETDATE()");

			builder.HasOne(model => model.FromShop).WithMany(shop => shop.NotificationsSent)
				.HasForeignKey(model => model.FromShopID);
			builder.HasOne(model => model.ToShop).WithMany(shop => shop.NotificationsReceived)
				.HasForeignKey(model => model.ToShopID);
			builder.HasOne(model => model.FromAdmin).WithMany(admin => admin.NotificationsSent)
				.HasForeignKey(model => model.FromAdminID);
			builder.HasOne(model => model.ToAdmin).WithMany(admin => admin.NotificationsReceived)
				.HasForeignKey(model => model.ToAdminID);
			builder.HasOne(model => model.FromCustomer).WithMany(cus => cus.NotificationsSent)
				.HasForeignKey(model => model.FromCustomerID);
			builder.HasOne(model => model.ToCustomer).WithMany(cus => cus.NotificationsReceived)
				.HasForeignKey(model => model.ToCustomerID);

			builder.HasOne(model => model.SysNotification).WithMany(sysNotification => sysNotification.Notifications)
				.HasForeignKey(model => model.NotificationID);
		}
	}
}
