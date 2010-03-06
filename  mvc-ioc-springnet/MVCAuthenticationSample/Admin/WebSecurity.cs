using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Web;


public class WebSecurity
{
    private const string DefCryptoAlg = "sha1";

    public static void HashWithSalt(
        string plaintext, ref string salt, out string hash)
    {
        const int SALT_BYTE_COUNT = 16;
        if (salt == null || salt == "")
        {
            byte[] saltBuf = new byte[SALT_BYTE_COUNT];
            RNGCryptoServiceProvider rng =
                new RNGCryptoServiceProvider();
            rng.GetBytes(saltBuf);

            StringBuilder sb =
                new StringBuilder(saltBuf.Length);
            for (int i = 0; i < saltBuf.Length; i++)
                sb.Append(string.Format("{0:X2}", saltBuf[i]));
            salt = sb.ToString();
        }

        hash = FormsAuthentication.
            HashPasswordForStoringInConfigFile(
            salt + plaintext, DefCryptoAlg);
    }
    public static string Encrypt(string plaintext)
    {

        FormsAuthenticationTicket ticket;
        ticket = new FormsAuthenticationTicket(1, "", DateTime.Now,
            DateTime.Now, false, plaintext, "");

        return FormsAuthentication.Encrypt(ticket);
    }

    public static string Decrypt(string ciphertext)
    {
        FormsAuthenticationTicket ticket;
        ticket = FormsAuthentication.Decrypt(ciphertext);
        return ticket.UserData;
    }


}