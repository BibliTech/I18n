using System;
using System.Collections.Generic;
using System.Text;

namespace BibliTech.I18n.Fallback
{

    public interface IFallbackStrategyStep
    {

        string Lookup(string key, Func<IFallbackStrategyStep?> next, IInProvider provider);

    }

    public abstract class BaseFallbackStrategyStep : IFallbackStrategyStep
    {

        public virtual string Lookup(string key, Func<IFallbackStrategyStep?> next, IInProvider provider)
        {
            var result = this.DoLookup(key, provider);

            if (result == null)
            {
                var nextStep = next();

                if (nextStep == null)
                {
                    throw new InvalidOperationException("Something is wrong with Strategy Setup: no proper end step.");
                }
                else
                {
                    return nextStep.Lookup(key, next, provider);
                }
            }
            else
            {
                return result;
            }
        }

        public abstract string? DoLookup(string key, IInProvider provider);

    }

}
