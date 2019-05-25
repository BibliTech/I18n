using System;
using System.Collections.Generic;
using System.Text;

namespace BibliTech.I18n.Test
{

    internal static class TestUtils
    {

        public const string DataFolder = @"Data\";
        public const string DataJsonFolder = DataFolder + @"Json\";

        public const string DataJsonEnFile1 = DataJsonFolder + @"text-en.json";
        public const string DataJsonEnLanguageCode = "en";

        public const string DataJsonViFile1 = DataJsonFolder + @"text-vi.json";
        public const string DataJsonViLanguageCode = "vi";

    }
    
    public static class TestDataEn
    {
        public static readonly string Hello_All = "Hello World";
        public static readonly string Hello_EnOnly = "Hello World En Only";
    }

    public static class TestDataVi
    {
        public static readonly string Hello_All = "Xin chào";
        public static readonly string Hello_ViOnly = "Xin chào - chỉ có trong tiếng Việt";
    }

}
