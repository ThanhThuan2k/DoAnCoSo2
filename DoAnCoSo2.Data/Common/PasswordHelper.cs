using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Common
{
	public class PasswordHelper
	{
        // LUÔN GIỮ ĐỘ DÀI CHUỖI = 256
        // KHÔNG ĐƯỢC XÓA/THÊM GÌ VÀO DÒNG NÀY
        // KHÔNG ĐƯỢC ĐỂ XUẤT HIỆN 2 GIÁ TRỊ GIỐNG NHAU
        const string COLLECTION_CHARS = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()_-+={[}]\|;:""\'<,>.?/abcdefghijklmnopqrstuvwxyzđáàảãạăắằẵẳặâấầẩẫậíìỉĩịúùủũụéèẻẽẹêếềễểệóòỏõọôốồổỗộơớờởỡợưứừửữựýỳỷỹỵĐÁÀẢÃẠĂẮẰẴẲẶÂẤẦẨẪẬÍÌỈĨỊÚÙỦŨỤÉÈẺẼẸÊẾỀỄỂỆÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢƯỨỪỬỮỰÝỲỶỸỴƵƶẐẑĎďĆćĈĉČčḨḩḤḥḪḫṰṱṮṯŦŧȾⱦț";

        public static string CreateSalt(int min, int max)
        {
            //const string mailCOLLECTION_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            char[] salt = new char[rand.Next(min, max)];
            for (int i = 0; i < salt.Length; i++)
            {
                salt[i] = COLLECTION_CHARS[rand.Next(COLLECTION_CHARS.Length)];
            }

            string result = new String(salt);
            return result;
        }

        public static string RandomNumber(int min, int max)
        {
            const string NUM_CHARS = "0123456789";
            Random rand = new Random();
            char[] salt = new char[rand.Next(min, max)];
            for (int i = 0; i < salt.Length; i++)
            {
                salt[i] = NUM_CHARS[rand.Next(NUM_CHARS.Length)];
            }

            string result = new String(salt);
            return result;
        }
    }
}
