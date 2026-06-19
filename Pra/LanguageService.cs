using System;
using System.Collections.Generic;
using System.Text;

namespace Pra
{
    internal class LanguageService
    {
        public static event Action<string>? LanguageChanged;

        public static string CurrentLanguage
        {
            get => Preferences.Get("Language", "Английский");
            set
            {
                Preferences.Set("Language", value);
                LanguageChanged?.Invoke(value);
            }
        }
    }
}
