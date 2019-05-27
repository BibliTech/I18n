using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{

    public static class FluintExtensions
    {

        public static string Lookup(this string keyword)
        {
            return Fluint.Fluin.Provider.Lookup(keyword);
        }

        public static string Lookup(this string keyword, string languageCode)
        {
            return Fluint.Fluin.Provider.Lookup(languageCode, keyword);
        }

        public static string Lookup(this string keyword, CultureInfo cultureInfo)
        {
            return Fluint.Fluin.Provider.Lookup(cultureInfo, keyword);
        }

    }

}
