using System;
using System.Globalization;
using Depressurizer.Core.Enums;

namespace Depressurizer.Core.Helpers
{
    public static class Language
    {
        #region Public Methods and Operators

        public static CultureInfo GetCultureInfo(StoreLanguage language)
        {
            switch (language)
            {
                case StoreLanguage.Arabic:
                    return new CultureInfo("ar");
                case StoreLanguage.Bulgarian:
                    return new CultureInfo("bg");
                case StoreLanguage.ChineseSimplified:
                    return new CultureInfo("zh-CN");
                case StoreLanguage.ChineseTraditional:
                    return new CultureInfo("zh-TW");
                case StoreLanguage.Czech:
                    return new CultureInfo("cs");
                case StoreLanguage.Danish:
                    return new CultureInfo("da");
                case StoreLanguage.Dutch:
                    return new CultureInfo("nl");
                case StoreLanguage.English:
                    return new CultureInfo("en");
                case StoreLanguage.Finnish:
                    return new CultureInfo("fi");
                case StoreLanguage.French:
                    return new CultureInfo("fr");
                case StoreLanguage.German:
                    return new CultureInfo("de");
                case StoreLanguage.Greek:
                    return new CultureInfo("el");
                case StoreLanguage.Hungarian:
                    return new CultureInfo("hu");
                case StoreLanguage.Italian:
                    return new CultureInfo("it");
                case StoreLanguage.Japanese:
                    return new CultureInfo("ja");
                case StoreLanguage.Korean:
                    return new CultureInfo("ko");
                case StoreLanguage.Norwegian:
                    return new CultureInfo("no");
                case StoreLanguage.Polish:
                    return new CultureInfo("pl");
                case StoreLanguage.Portuguese:
                    return new CultureInfo("pt");
                case StoreLanguage.PortugueseBrasil:
                    return new CultureInfo("pt-BR");
                case StoreLanguage.Romanian:
                    return new CultureInfo("ro");
                case StoreLanguage.Russian:
                    return new CultureInfo("ru");
                case StoreLanguage.Spanish:
                    return new CultureInfo("es");
                case StoreLanguage.SpanishLatin:
                    // TODO: Confirm this is the proper language code.
                    return new CultureInfo("es-MX");
                case StoreLanguage.Swedish:
                    return new CultureInfo("sv");
                case StoreLanguage.Thai:
                    return new CultureInfo("th");
                case StoreLanguage.Turkish:
                    return new CultureInfo("tr");
                case StoreLanguage.Ukrainian:
                    return new CultureInfo("uk");
                case StoreLanguage.Vietnamese:
                    return new CultureInfo("vi");
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }

        public static CultureInfo GetCultureInfo(InterfaceLanguage language)
        {
            switch (language)
            {
                case InterfaceLanguage.English:
                    return new CultureInfo("en");
                case InterfaceLanguage.Spanish:
                    return new CultureInfo("es");
                case InterfaceLanguage.Russian:
                    return new CultureInfo("ru");
                case InterfaceLanguage.Ukrainian:
                    return new CultureInfo("uk");
                case InterfaceLanguage.Dutch:
                    return new CultureInfo("nl");
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }

        public static string LanguageCode(StoreLanguage language)
        {
            switch (language)
            {
                case StoreLanguage.Arabic:
                    return "arabic";
                case StoreLanguage.Bulgarian:
                    return "bulgarian";
                case StoreLanguage.ChineseSimplified:
                    // ReSharper disable once StringLiteralTypo
                    return "schinese";
                case StoreLanguage.ChineseTraditional:
                    // ReSharper disable once StringLiteralTypo
                    return "tchinese";
                case StoreLanguage.Czech:
                    return "czech";
                case StoreLanguage.Danish:
                    return "danish";
                case StoreLanguage.Dutch:
                    return "dutch";
                case StoreLanguage.English:
                    return "english";
                case StoreLanguage.Finnish:
                    return "finnish";
                case StoreLanguage.French:
                    return "french";
                case StoreLanguage.German:
                    return "german";
                case StoreLanguage.Greek:
                    return "greek";
                case StoreLanguage.Hungarian:
                    return "hungarian";
                case StoreLanguage.Italian:
                    return "italian";
                case StoreLanguage.Japanese:
                    return "japanese";
                case StoreLanguage.Korean:
                    // ReSharper disable once StringLiteralTypo
                    return "koreana";
                case StoreLanguage.Norwegian:
                    return "norwegian";
                case StoreLanguage.Polish:
                    return "polish";
                case StoreLanguage.Portuguese:
                    return "portuguese";
                case StoreLanguage.PortugueseBrasil:
                    return "brazilian";
                case StoreLanguage.Romanian:
                    return "romanian";
                case StoreLanguage.Russian:
                    return "russian";
                case StoreLanguage.Spanish:
                    return "spanish";
                case StoreLanguage.SpanishLatin:
                    // ReSharper disable once StringLiteralTypo
                    return "latam";
                case StoreLanguage.Swedish:
                    return "swedish";
                case StoreLanguage.Thai:
                    return "thai";
                case StoreLanguage.Turkish:
                    return "turkish";
                case StoreLanguage.Ukrainian:
                    return "ukrainian";
                case StoreLanguage.Vietnamese:
                    return "vietnamese";
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }

        #endregion
    }
}
