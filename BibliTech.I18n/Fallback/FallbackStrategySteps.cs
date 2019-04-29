using System;
using System.Collections.Generic;
using System.Text;

namespace BibliTech.I18n.Fallback
{
    public class EmptyFallbackStrategyStep : BaseFallbackStrategyStep
    {

        public override string? DoLookup(string key, IInProvider provider) => "";

    }

    public class UseFallbackLanguageFallbackStrategyStep : BaseFallbackStrategyStep
    {

        public override string? DoLookup(string key, IInProvider provider)
        {
            var fallbackLanguageCode = provider.FallbackLanguage;

            return fallbackLanguageCode == null ?
                null :
                provider.Lookup(fallbackLanguageCode, key);
        }

    }

    public class UseLookupKeyFallbackStrategyStep : BaseFallbackStrategyStep
    {

        public override string? DoLookup(string key, IInProvider provider) => key;

    }

    public class ThrowExceptionFallbackStrategyStep : BaseFallbackStrategyStep
    {

        public override string? DoLookup(string key, IInProvider provider) => throw new KeyNotFoundException(key);

    }

}
