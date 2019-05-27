using BibliTech.I18n;
using Microsoft.AspNetCore.Html;
using System;
using System.Globalization;

namespace System
{

    public static class FluintAspNetCoreExtensions
    {

        public static HtmlString LookupHtml(this string keyword)
        {
            return Fluint.Fluin.Provider.LookupHtml(keyword);
        }

        public static HtmlString LookupHtml(this string keyword, string languageCode)
        {
            return Fluint.Fluin.Provider.LookupHtml(languageCode, keyword);
        }

        public static HtmlString LookupHtml(this string keyword, CultureInfo cultureInfo)
        {
            return Fluint.Fluin.Provider.LookupHtml(cultureInfo, keyword);
        }

    }

}

