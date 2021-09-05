using DoAnCoSo2.DTOs.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysNotification
	{
		public SysNotification()
		{
			Notifications = new HashSet<Notification>();
		}
		public int Id { get; set; }
		public string Description { get; set; }
		public string Message { get; set; }
		public DateTime? DeleteAt { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }

		public ICollection<Notification> Notifications { get; set; }
	}
}
