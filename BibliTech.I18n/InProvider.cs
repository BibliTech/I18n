using BibliTech.I18n.Builder;
using BibliTech.I18n.Fallback;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliTech.I18n
{

    public interface IInProvider
    {

        string? FallbackLanguage { get; }
        string Lookup(string languageCode, string key);

    }

    public class InProvider : IInProvider
    {

        Dictionary<string, Dictionary<string, string>> items;

        public FallbackStrategy FallbackStrat { get; private set; }
        public string? FallbackLanguage { get; private set; }

        internal InProvider(InBuilder builder)
        {
            this.items = new Dictionary<string, Dictionary<string, string>>();
            this.FallbackStrat = builder.FallbackStrategy;

            this.LoadItems(builder);
        }

        public string Lookup(string languageCode, string key)
        {
            if (this.items.TryGetValue(languageCode, out var langDict))
            {
                if (langDict.TryGetValue(key, out var result))
                {
                    return result;
                }
                else
                {
                    return this.UseFallback(key);
                }
            }
            else
            {
                return this.UseFallback(key);
            }
        }

        private string UseFallback(string key)
        {
            return this.FallbackStrat.Lookup(key, this);
        }

        private void LoadItems(InBuilder builder)
        {
            foreach (var input in builder.Inputs)
            {
                var code = input.LanguageCode;
                var langDict = this.items.GetOrCreate(code,
                    _ => new Dictionary<string, string>());

                var inputItems = input.Read();
                foreach (var pair in inputItems)
                {
                    langDict[pair.Key] = pair.Value;
                }
            }
        }

    }

}
