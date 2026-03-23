using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace DVLD.Business
{
    public class clsSecuritycs
    {
        public static string ComputeHash(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                return string.Empty;
            }
            using (SHA256 sh = SHA256.Create())
            {
                byte[] TransBytes = Encoding.UTF8.GetBytes(Input);
                byte[] hashBytes = sh.ComputeHash(TransBytes);

                StringBuilder sbHashInput = new StringBuilder();
                foreach (byte B in hashBytes)
                {
                    sbHashInput.Append(B.ToString("x2"));
                }
                return sbHashInput.ToString();
            }
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = ComputeHash(enteredPassword);
            return enteredHash == storedHash;
        }
    }
}
