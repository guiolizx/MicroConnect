using System.Security.Cryptography;
using System.Text;

namespace teste.Helper;

public static class Crypt
{
    public static string genHash(this string val)
    {
        var hash = SHA1.Create();
        var encoding = new ASCIIEncoding();
        var array = encoding.GetBytes(val);

        array = hash.ComputeHash(array);

        var strHexa = new StringBuilder();

        foreach(var i in array)
        {
            strHexa.Append(i.ToString("x2"));
        }

        return strHexa.ToString();
    }
}