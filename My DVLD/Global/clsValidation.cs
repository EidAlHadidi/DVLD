using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace My_DVLD
{
    public class clsValidation
    {
        static string EmailValidationPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        static Regex regex = new Regex(EmailValidationPattern);
        public static bool isValidEmail(string Email)
        {
            return regex.IsMatch(Email);
        }
    }
}
