using System.Text;

namespace NewsWebSite.Common
{
    public static class Hash
    {
        public static string PasswordHash(string Password, string PasswordSalt)
        {
            var bytes = Encoding.UTF8.GetBytes(Password + PasswordSalt);
            var Hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return Hash;
        }

        public static bool VerifyPassword(string PasswordHash, string PasswordSalt, string Password)
        {
            var bytes = Encoding.UTF8.GetBytes(Password + PasswordSalt);
            var Hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            if(Hash == PasswordHash) return true;
            return false;
        }
    }
}
