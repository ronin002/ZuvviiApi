using System.Text;
using System.Text.RegularExpressions;

namespace ZuvviiAPI.Services
{
    public class Utils
    {
        public static bool ValidMail(string mail)
        {
            if (mail == null) return false;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mail);
            return match.Success;
        }

        public static string HashPassword(string mail, string pwd)
        {
            var firstCharUp = mail.Substring(0, 1).ToUpper();
            var lastCharUp = mail.Substring(mail.Length - 1).ToUpper();
            var firstCharLower = firstCharUp.ToLower();
            var lastCharLower = lastCharUp.ToLower();
            pwd = firstCharUp + lastCharLower + pwd + firstCharLower + lastCharUp;
            return sha256(pwd);
        }

        public static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
