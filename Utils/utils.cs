using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIpayApplication.Utils
{
    public class utils
    {
        public static string IdGenerated(string value) {
            string returnValue = string.Empty;
            string dateString = DateTime.Now.ToString("yyyymmddhhmmss");
            returnValue = string.Format("{0}{1}{2}", value, dateString, GetRandomString(Constantes.lenghtValueDefautl));
            return returnValue;
        }

        public static string GetRandomString(int length) {
            string dic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] value = dic.ToCharArray();

            var stringConcat = string.Empty;
            Random random;

            for (int i = 0; i < length; i++)
            {
                random = new Random();
                stringConcat += value[random.Next(0, dic.Length - 1)];
            }

            return stringConcat;
        }
    }
}
