using DoAnCoSo2.Data.Configuration.App;
using DoAnCoSo2.Data.Configuration.Auth;
using DoAnCoSo2.Data.Configuration.Rel;
using DoAnCoSo2.Data.Configuration.Sys;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using DoAnCoSo2.DTOs.Rel;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DoAnCoSo2.Data
{
	public class DoAnCoSo2DbContext : DbContext
	{
		public DbSet<SysRoles> SysRoles { get; set; }
		public DbSet<SysConfiguration> SysConfigurations { get; set; }
		public DbSet<SysOrderStatus> SysOrderStatus { get; set; }
		public DbSet<SysNotification> SysNotifications { get; set; }
		public DbSet<SysEvaluated> SysEvaluated { get; set; }
		public DbSet<SysError> SysErrors { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Shop> Shops { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product_Evaluated> Product_Evaluated { get; set; }
		public DbSet<Customer_Shop_Message> CustomerShopMessage { get; set; }
		public DbSet<Customer_Admin_Message> CustomerAdminMessage { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category2> Category2 { get; set; }
		public DbSet<Category3> Category3 { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build();
			optionsBuilder.UseSqlServer(builder.GetConnectionString("DoAnCoSo2"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new SysRoleConfiguration());
			modelBuilder.ApplyConfiguration(new SysOrderStatusConfiguration());
			modelBuilder.ApplyConfiguration(new SysNotificationConfiguration());
			modelBuilder.ApplyConfiguration(new SysEvaluatedConfiguration());
			modelBuilder.ApplyConfiguration(new SysErrorConfiguration());
			modelBuilder.ApplyConfiguration(new SysConfigurationConfiguration());

			modelBuilder.ApplyConfiguration(new AdminConfiguration());
			modelBuilder.ApplyConfiguration(new CustomerConfiguration());

			modelBuilder.ApplyConfiguration(new ShopConfiguration());
			modelBuilder.ApplyConfiguration(new BrandConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new CommentConfiguration());
			modelBuilder.ApplyConfiguration(new ImageConfiguration());
			modelBuilder.ApplyConfiguration(new NotificationConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new AddressConfiguration());
			modelBuilder.ApplyConfiguration(new Customer_Shop_MessageConfiguration());
			modelBuilder.ApplyConfiguration(new Customer_Admin_MessageConfiguration());

			modelBuilder.ApplyConfiguration(new Product_Evaluated_Configuration());
			modelBuilder.ApplyConfiguration(new CartConfiguration());
			modelBuilder.ApplyConfiguration(new Category2Configuration());
			modelBuilder.ApplyConfiguration(new Category3Configuration());
		}
	}
}
