using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BibliTech.I18n.CodeLookup
{
    public class TwoLetterCodeLookup : ICodeLookup
    {
        public string Lookup(CultureInfo cultureInfo) => cultureInfo.TwoLetterISOLanguageName;
    }

    public class ThreeLetterCodeLookup : ICodeLookup
    {
        public string Lookup(CultureInfo cultureInfo) => cultureInfo.ThreeLetterISOLanguageName;
    }

    public class NameCodeLookup : ICodeLookup
    {
        public string Lookup(CultureInfo cultureInfo) => cultureInfo.Name;
    }

    public class UnsupportedCodeLookup : ICodeLookup
    {
        public string Lookup(CultureInfo cultureInfo) => throw new NotSupportedException();
    }

}
