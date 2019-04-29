using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.I18n.InInputs
{

    public interface IInInput
    {
        string LanguageCode { get; }
        IDictionary<string, string> Read();
    }

    public abstract class BaseInInput : IInInput
    {

        public string LanguageCode { get; protected set; }

        public BaseInInput(string languageCode)
        {
            this.LanguageCode = languageCode;
        }

        public abstract IDictionary<string, string> Read();

    }

}
