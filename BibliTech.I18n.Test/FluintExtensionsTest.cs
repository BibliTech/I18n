using BibliTech.I18n.Fallback;
using Fluint;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace BibliTech.I18n.Test
{

    public class FluintExtensionsTest
    {

        static FluintExtensionsTest()
        {
            Fluin.Initialize(builder =>
            {
                builder
                    .AddJsonFile(TestUtils.DataJsonEnFile1, TestUtils.DataJsonEnLanguageCode, true)
                    .AddJsonFile(TestUtils.DataJsonViFile1, TestUtils.DataJsonViLanguageCode)
                    .SetFallbackStrategy(FallbackStrategy.DefaultFallbackStrategy)
                    .SetFallbackLanguage("en");
            });
        }

        [Fact]
        public void ShouldReturnLookup()
        {
            Assert.Equal(TestDataEn.Hello_All, nameof(TestDataEn.Hello_All).Lookup());
            Assert.Equal(TestDataEn.Hello_EnOnly, nameof(TestDataEn.Hello_EnOnly).Lookup());
        }

        [Fact]
        public void ShouldReturnLookupHtml()
        {
            Assert.Equal(TestDataEn.Hello_All, nameof(TestDataEn.Hello_All).LookupHtml().Value);
            Assert.Equal(TestDataEn.Hello_EnOnly, nameof(TestDataEn.Hello_EnOnly).LookupHtml().Value);
        }

        [Fact]
        public void ShouldReturnLookupWithLanguage()
        {
            Assert.Equal(TestDataVi.Hello_All, nameof(TestDataVi.Hello_All).Lookup(TestUtils.DataJsonViLanguageCode));
            Assert.Equal(TestDataVi.Hello_ViOnly, nameof(TestDataVi.Hello_ViOnly).Lookup(TestUtils.DataJsonViLanguageCode));
        }

        [Fact]
        public void ShouldReturnLookupWithCulture()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("en");
            Assert.Equal(TestDataEn.Hello_All, nameof(TestDataEn.Hello_All).Lookup());
            Assert.Equal(TestDataEn.Hello_EnOnly, nameof(TestDataEn.Hello_EnOnly).Lookup());

            CultureInfo.CurrentUICulture = new CultureInfo("vi");
            Assert.Equal(TestDataVi.Hello_All, nameof(TestDataVi.Hello_All).Lookup());
            Assert.Equal(TestDataVi.Hello_ViOnly, nameof(TestDataVi.Hello_ViOnly).Lookup());
        }

    }

}
