using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BibliTech.I18n.CodeLookup
{

    public interface ICodeLookup
    {

        string Lookup(CultureInfo cultureInfo);

    }

}
