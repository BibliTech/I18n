using BibliTech.I18n.Builder;
using BibliTech.I18n.CodeLookup;
using BibliTech.I18n.Fallback;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BibliTech.I18n
{

    public interface IInProvider
    {

        string? FallbackLanguage { get; }
        string Lookup(string languageCode, string key, bool shouldFallback = true);
        string Lookup(string key, bool shouldFallback = true);

    }

    public class InProvider : IInProvider
    {

        Dictionary<string, Dictionary<string, string>> items;

        public FallbackStrategy FallbackStrat { get; private set; }
        public string? FallbackLanguage { get; private set; }
        public ICodeLookup CodeLookup { get; private set; }

        internal InProvider(InBuilder builder)
        {
            this.items = new Dictionary<string, Dictionary<string, string>>();
            this.FallbackStrat = builder.FallbackStrategy;
            this.FallbackLanguage = builder.FallbackLanguage;

            this.LoadItems(builder);
        }

        public string Lookup(string key, bool shouldFallback = true)
        {
            var culture = CultureInfo.CurrentUICulture;
            return this.Lookup(this.CodeLookup.Lookup(culture), key, shouldFallback);
        }

        public string Lookup(string languageCode, string key, bool shouldFallback = true)
        {
            Func<string> useFallbackOrThrow = () =>
            {
                if (shouldFallback)
                {
                    return this.FallbackStrat.Lookup(key, this);
                }
                else
                {
                    return null;
                }
            };

            if (this.items.TryGetValue(languageCode, out var langDict))
            {
                if (langDict.TryGetValue(key, out var result))
                {
                    return result;
                }
                else
                {
                    return useFallbackOrThrow();
                }
            }
            else
            {
                return useFallbackOrThrow();
            }
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
