using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Extensions
{
    public static class BrandNameExtensions
    {
        public static string GetBrandNameShortening(this string str)
        {
            if (str.Length < 3)
                return str;
            else
                return str.Substring(0, 3);
        }
    }
}
