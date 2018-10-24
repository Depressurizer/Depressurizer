using System;
using System.Globalization;
using Depressurizer.Enums;

namespace Depressurizer.Helpers
{
    internal static class Language
    {
        #region Public Methods and Operators

        public static CultureInfo GetCultureInfo(InterfaceLanguage language)
        {
            CultureInfo cultureInfo;

            switch (language)
            {
                case InterfaceLanguage.English:
                    cultureInfo = new CultureInfo("en");
                    break;
                case InterfaceLanguage.Spanish:
                    cultureInfo = new CultureInfo("es");
                    break;
                case InterfaceLanguage.Russian:
                    cultureInfo = new CultureInfo("ru");
                    break;
                case InterfaceLanguage.Ukrainian:
                    cultureInfo = new CultureInfo("uk");
                    break;
                case InterfaceLanguage.Dutch:
                    cultureInfo = new CultureInfo("nl");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }

            return cultureInfo;
        }

        #endregion
    }
}
