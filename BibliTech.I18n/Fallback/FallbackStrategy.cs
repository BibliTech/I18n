using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliTech.I18n.Fallback
{

    public class FallbackStrategy
    {

        public static FallbackStrategy DefaultFallbackStrategy;
        public static FallbackStrategy FallbackLanguageThenUseKeyStrategy;
        public static FallbackStrategy FallbackLanguageThenEmptyStrategy;
        public static FallbackStrategy FallbackLanguageThenThrowIfMissing;
        public static FallbackStrategy ThrowIfMissing;

        static FallbackStrategy()
        {
            DefaultFallbackStrategy = FallbackLanguageThenUseKeyStrategy = new FallbackStrategy(new IFallbackStrategyStep[]
            {
                new UseFallbackLanguageFallbackStrategyStep(),
                new UseLookupKeyFallbackStrategyStep(),
            });

            FallbackLanguageThenUseKeyStrategy = new FallbackStrategy(new IFallbackStrategyStep[]
            {
                new UseFallbackLanguageFallbackStrategyStep(),
                new EmptyFallbackStrategyStep(),
            });

            FallbackLanguageThenThrowIfMissing = new FallbackStrategy(new IFallbackStrategyStep[]
            {
                new UseFallbackLanguageFallbackStrategyStep(),
                new ThrowExceptionFallbackStrategyStep(),
            });

            ThrowIfMissing = new FallbackStrategy(new IFallbackStrategyStep[]
            {
                new ThrowExceptionFallbackStrategyStep(),
            });
        }

        IReadOnlyList<IFallbackStrategyStep> fallbackStrategySteps;

        public FallbackStrategy(IEnumerable<IFallbackStrategyStep> steps)
        {
            this.fallbackStrategySteps = steps.ToList();
        }

        public string Lookup(string key, IInProvider provider)
        {
            var i = 1;

            return this.fallbackStrategySteps[0].Lookup(
                key,
                () => i == this.fallbackStrategySteps.Count ? null : this.fallbackStrategySteps[i++],
                provider);
        }

    }

}
