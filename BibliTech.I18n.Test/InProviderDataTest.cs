using BibliTech.I18n.Builder;
using BibliTech.I18n.Fallback;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BibliTech.I18n.Test
{

    public class InProviderTest
    {
        const string TestInvalidKeyword = "SOMETHING_NONEXISTANCE";

        [Fact]
        public void ShouldLoadJsonFile()
        {
            var provider = this.BuildProvider();

            this.AssertCommonData(provider);
            Assert.Equal(TestUtils.DataJsonEnLanguageCode, provider.FallbackLanguage);
        }

        [Fact]
        public void ShouldUseFallbackLanguage()
        {
            var provider = this.BuildProvider();

            var result = provider.Lookup("na-code", nameof(TestDataEn.Hello_All));
            Assert.Equal(TestDataEn.Hello_All, result);
        }

        [Fact]
        public void ShouldUseFallbackKeyword()
        {
            var provider = this.BuildProvider();

            
            var result = provider.Lookup(TestUtils.DataJsonEnLanguageCode, TestInvalidKeyword);
            Assert.Equal(TestInvalidKeyword, result);
        }

        [Fact]
        public void ShouldThrowIfMissing()
        {
            var provider = this.BuildProvider(FallbackStrategy.FallbackLanguageThenThrowIfMissing);

            Assert.Throws<KeyNotFoundException>(() =>
            {
                var result = provider.Lookup(TestUtils.DataJsonEnLanguageCode, TestInvalidKeyword);
            });
        }

        private InProvider BuildProvider(FallbackStrategy fallbackStrategy = null, string fallbackLanguage = null)
        {
            return new InBuilder()
                .AddJsonFile(TestUtils.DataJsonEnFile1, TestUtils.DataJsonEnLanguageCode, true)
                .AddJsonFile(TestUtils.DataJsonViFile1, TestUtils.DataJsonViLanguageCode)
                .SetFallbackStrategy(fallbackStrategy ?? FallbackStrategy.DefaultFallbackStrategy)
                .SetFallbackLanguage(fallbackLanguage ?? "en")
                .Build();
        }

        private void AssertCommonData(IInProvider provider)
        {
            Assert.NotNull(provider);

            var enCode = TestUtils.DataJsonEnLanguageCode;
            var viCode = TestUtils.DataJsonViLanguageCode;

            Assert.Equal(TestDataEn.Hello_All, provider.Lookup(enCode, nameof(TestDataEn.Hello_All)));
            Assert.Equal(TestDataEn.Hello_EnOnly, provider.Lookup(enCode, nameof(TestDataEn.Hello_EnOnly)));

            Assert.Equal(TestDataVi.Hello_All, provider.Lookup(viCode, nameof(TestDataVi.Hello_All)));
            Assert.Equal(TestDataVi.Hello_ViOnly, provider.Lookup(viCode, nameof(TestDataVi.Hello_ViOnly)));
        }

    }

}
