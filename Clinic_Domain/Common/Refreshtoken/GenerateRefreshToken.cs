using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Domain.Common.Refreshtoken
{
    public static class GenerateRefreshToken
    {
public static string Generate()
{
    var bytes = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(bytes);
    return Convert.ToBase64String(bytes);
}
    
    }
}


    //public static string Generate()
        //{
        //    var randomBytes = new byte[32];
        //    using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(randomBytes);
        //    }
        //    return Convert.ToBase64String(randomBytes);
        //}