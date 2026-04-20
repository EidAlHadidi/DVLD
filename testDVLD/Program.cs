using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Business;
using Data;
using System.Data;
using System.Security.Cryptography;

namespace testDVLD
{
    internal class Program
    {

        static void Find(int ID)
        {
            clsDetainedLicense detain = clsDetainedLicense.Find_ByLicenseID(ID);
            if (detain != null)
            {
                Console.WriteLine($"{detain.DetainID} | {detain.LicenseID} | {detain.DetainDate}" +
                    $" | {detain.FineFees} | {detain.CreatedByUserID} | {detain.IsReleased} ");
            }
        }

        static void Main(string[] args)
        {
            Find(27);
        }
		
	}
}
 