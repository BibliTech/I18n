using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.I18n.InInputs
{

    public class JsonInInput : BaseInInput
    {

        Func<Stream> inputStream;

        public JsonInInput(string languageCode, Stream inputStream) : this(languageCode, () => inputStream) { }

        public JsonInInput(string languageCode, Func<Stream> inputStream) : base(languageCode)
        {
            this.inputStream = inputStream;
        }

        public override IDictionary<string, string> Read()
        {
            using (var stream = this.inputStream())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var content = streamReader.ReadToEnd();
                    return content.DeserializeJson<IDictionary<string, string>>();
                }
            }
        }

    }

}
