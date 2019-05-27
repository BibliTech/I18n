using BibliTech.I18n;
using BibliTech.I18n.Builder;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace BibliTech.I18n
{

    public static class I18nAspNetCoreExtensions
    {

        public static HtmlString LookupHtml(this IInProvider provider, string key, bool shouldFallback = false)
        {
            return new HtmlString(provider.Lookup(key, shouldFallback));
        }

        public static HtmlString LookupHtml(this IInProvider provider, CultureInfo cultureInfo, string key, bool shouldFallback = false)
        {
            return new HtmlString(provider.Lookup(cultureInfo, key, shouldFallback));
        }

        public static HtmlString LookupHtml(this IInProvider provider, string languageCode, string key, bool shouldFallback = false)
        {
            return new HtmlString(provider.Lookup(languageCode, key, shouldFallback));
        }

    }

}

namespace Microsoft.Extensions.DependencyInjection
{

    public static class I18nAspNetCoreExtensions
    {

        public static IServiceCollection AddI18n(this IServiceCollection services, Action<InBuilder> builderAction)
        {
            var inBuilder = new InBuilder();
            builderAction(inBuilder);

            var provider = inBuilder.Build();
            services.AddSingleton<IInProvider>(provider);

            return services;
        }

    }

}