namespace DoAnCoSo2.Data.Constant
{
	public class DbConstant
	{
		// global constant
		private const ushort EMAIL_MAX_LENGTH = 100;
		private const ushort FULLNAME_MAX_LENGTH = 500;
		private const ushort SEX_MAX_LENGTH = 20;
		private const ushort PHONE_NUMBER_MAX_LENGTH = 20;
		private const ushort USERNAME_MAX_LENGTH = 200;
		private const ushort PASSWORD_MAX_LENGTH = 200;
		private const ushort AVATAR_MAX_LENGTH = 1000;
		private const ushort ADDRESS_MAX_LENGTH = 5000;

		// Admin table
		public const string ADMIN_TABLE_NAME = "Admin";
		public const ushort ADMIN_USERNAME_MAX_LENGTH = USERNAME_MAX_LENGTH;
		public const ushort ADMIN_PASSWORD_MAX_LENGTH = PASSWORD_MAX_LENGTH;
		public const ushort ADMIN_FULLNAME_MAX_LENGTH = FULLNAME_MAX_LENGTH;
		public const ushort ADMIN_SEX_MAX_LENGTH = SEX_MAX_LENGTH;
		public const ushort ADMIN_PHONENUMBER_MAX_LENGTH = PHONE_NUMBER_MAX_LENGTH;
		public const ushort ADMIN_EMAIL_MAX_LENGTH = EMAIL_MAX_LENGTH;
		public const ushort ADMIN_AVATAR_MAX_LENGTH = AVATAR_MAX_LENGTH;

		// SysRole table
		public const string SYSROLE_TABLE_NAME = "SysRole";
		public const ushort SYSROLE_ROLENAME_MAX_LENGTH = 100;

		//  Error table
		public const string SYSERROR_TABLE_NAME = "SysError";
		public const ushort SYSERROR_ERRORNAME_MAX_LENGTH = 500;

		// Customer table
		public const string CUSTOMER_TABLE_NAME = "Customer";
		public const ushort CUSTOMER_EMAIL_MAX_LENGTH = EMAIL_MAX_LENGTH;
		public const ushort CUSTOMER_FULLNAME_MAX_LENGTH = FULLNAME_MAX_LENGTH;
		public const ushort CUSTOMER_SEX_MAX_LENGTH = SEX_MAX_LENGTH;
		public const ushort CUSTOMER_PHONENUMBER_MAX_LENGTH = PHONE_NUMBER_MAX_LENGTH;
		public const ushort CUSTOMER_USERNAME_MAX_LENGTH = USERNAME_MAX_LENGTH;
		public const ushort CUSTOMER_PASSWORD_MAX_LENGTH = PASSWORD_MAX_LENGTH;
		public const ushort CUSTOMER_AVATAR_MAX_LENGTH = AVATAR_MAX_LENGTH;

		// Address table
		public const string ADDRESS_TABLE_NAME = "Address";
		public const ushort ADDRESS_FULLNAME_MAX_LENGTH = FULLNAME_MAX_LENGTH;
		public const ushort ADDRESS_PHONENUMBER_MAX_LENGTH = PHONE_NUMBER_MAX_LENGTH;
		public const ushort ADDRESS_ADDRESS_MAX_LENGTH = ADDRESS_MAX_LENGTH;

		// Shop table
		public const string SHOP_TABLE_NAME = "Shop";
		public const ushort SHOP_ADDRESS_MAX_LENGTH = ADDRESS_MAX_LENGTH;
		public const ushort SHOP_AVATAR_MAX_LENGTH = AVATAR_MAX_LENGTH;
		public const ushort SHOP_URI_MAX_LENGTH = 1000;
		public const ushort SHOP_NAME_MAX_LENGTH = 1000;
		public const ushort SHOP_NICKNAME_MAX_LENGTH = 1000;

		// SysNotification table
		public const string SYSNOTIFICATION_TABLE_NAME = "SysNotification";
		public const ushort SYSNOTIFICATION_DESCRIPTION_MAX_LENGTH = 3000;
		public const ushort SYSNOTIFICATION_MESSAGE_MAX_LENGTH = 5000;

		// Notification table
		public const string NOTIFICATION_TABLE_NAME = "Notification";
		public const ushort NOTIFICATION_NOTIFICATIONHEADER_MAX_LENGTH = 500;
		public const ushort NOTIFICATION_NOTIFICATIONBODY_MAX_LENGTH = 5000;
		public const ushort NOTIFICATION_URL_MAX_LENGTH = 1000;

		// Product table
		public const string PRODUCT_TABLE_NAME = "Product";
		public const ushort PRODUCT_NAME_MAX_LENGTH = 10000;
		public const ushort PRODUCT_MATERIAL_MAX_LENGTH = 100;

		// Category table
		public const string CATEGORY_TABLE_NAME = "Category";
		public const ushort CATEGORY_NAME_MAX_LENGTH = 100;
		public const ushort CATEGORY_AVATAR_MAX_LENGTH = AVATAR_MAX_LENGTH;
		
		// Brand table
		public const string BRAND_TABLE_NAME = "Brand";
		public const ushort BRAND_NAME_MAX_LENGTH = 100;
		public const ushort BRAND_AVATAR_MAX_LENGTH = AVATAR_MAX_LENGTH;

		// Image table
		public const string IMAGE_TABLE_NAME = "Image";
		public const ushort IMAGE_URL_MAX_LENGTH = 1000;

		// Configuration table
		public const string SYSCONFIGURATION_TABLE_NAME = "Configuration";
		public const ushort SYSCONFIGURATION_NAME_MAX_LENGTH = 100;
		public const ushort SYSCONFIGURATION_VALUE_MAX_LENGTH = 500;

		// Order table
		public const string ORDER_TABLE_NAME = "Order";
		public const ushort ORDER_ADDRESS_MAX_LENGTH = ADDRESS_MAX_LENGTH;

		// SysOrderStatus table
		public const string SYSORDERSTATUS_TABLE_NAME = "SysOrderStatus";
		public const ushort SYSORDERSTATUS_STATUS_MAX_LENGTH = 100;

		// SysEvaluated table
		public const string SYSEVALUATED_TABLE_NAME = "SysEvaluated";

		// Product_Evaluated table
		public const string PRODUCT_EVALUATED_TABLE_NAME = "Product_Evaluated";

		// Comment table
		public const string COMMENT_TABLE_NAME = "Comment";

		// order detail table
		public const string ORDERDETAIL_TABLE_NAME = "OrderDetail";

		// Customer_Shop_Message table
		public const string CUSTOMER_SHOP_MESSAGE_TABLE_NAME = "CustomerShopMessage";

		// Customer_Admin_Message table
		public const string CUSTOMER_ADMIN_TABLE_NAME = "CustomerAdminMessage";

		// Cart table
		public const string CART_TABLE_NAME = "Cart";
		public const ushort CART_COLOR_MAX_LENGTH = 50;
		public const ushort CART_CLASSIFICATION_MAX_LENGTH = 1000;
	}
}
