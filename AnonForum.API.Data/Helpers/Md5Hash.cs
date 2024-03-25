namespace AnonForum.API.Data.Helpers
{
    public class Md5Hash
    {
        public static string GetHash(string input)
        {
            using (var sha = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(inputBytes);

                var sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                //convert to base64

                return sb.ToString();
            }
        }

        //hash with base64 output
        public static string GetHashBase64(string input)
        {
            using (var sha = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(inputBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
    public class Sha256Hash
    {
        public static string GetHash(string input)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(inputBytes);

                var sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        // Hash with base64 output
        public static string GetHashBase64(string input)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(inputBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }

}
