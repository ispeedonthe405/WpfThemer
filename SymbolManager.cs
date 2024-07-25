using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfThemer
{
    public class DynamicSymbol
    {
        public string Name { get; set; } = string.Empty;
        public Uri? Value { get; set; } = default;
        public Theme.eThemeType ThemeType { get; set; } = Theme.eThemeType.Undefined;

        public DynamicSymbol(string name, Uri value, Theme.eThemeType themeType)
        {
            Name = name;
            Value = value;
            ThemeType = themeType;
        }
    }

    public static class ThemeSymbolManager
    {
        public static ObservableCollection<DynamicSymbol> Symbols = [];

        public static void ClearSymbols()
        {
            Symbols.Clear();
        }

        public static void AddSymbol(string name, Uri value, Theme.eThemeType themeType)
        {
            Symbols.Add(new DynamicSymbol(name, value, themeType));
        }

        public static DynamicSymbol? GetSymbol(string name)
        {
            return Symbols.FirstOrDefault(s =>
            (s.ThemeType == ThemeManager.ActiveTheme.ThemeType) &&
            (s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)));
        }
        
        
    }
}
