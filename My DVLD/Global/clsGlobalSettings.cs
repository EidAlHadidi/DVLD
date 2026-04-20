using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace My_DVLD
{
    public class clsGlobalSettings
    {
        public static int LoggedInUserID = -1;
        public static string EventSourceName = "DVLD";

        public static string ComputeHash(string input)
        {
			using (SHA256 sha256 = SHA256.Create())
			{
				// Compute the hash value from the UTF-8 encoded input string
				byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));


				// Convert the byte array to a lowercase hexadecimal string
				return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
			}
		}
    }
}
