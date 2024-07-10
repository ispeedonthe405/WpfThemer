
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfThemer
{
    public class ThemeManager : INotifyPropertyChanged
    {
        ///////////////////////////////////////////////////////////
        #region INotifyPropertyChanged
        /////////////////////////////

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetField<TField>(ref TField field, TField value, string propertyName)
        {
            if (EqualityComparer<TField>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        /////////////////////////////
        #endregion INotifyPropertyChanged
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Fields
        /////////////////////////////

        private static ObservableCollection<Theme> _Themes = [];
        private static ObservableCollection<ResourceDictionary> _Templates = [];
        private static Theme _ActiveTheme = new();
        private static Application? _Application;

        /////////////////////////////
        #endregion Fields
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Properties
        /////////////////////////////

        public static ObservableCollection<Theme> Themes
        {
            get => _Themes;
        }

        public static ObservableCollection<ResourceDictionary> Templates
        {
            get => _Templates;
        }

        public static Theme ActiveTheme
        {
            get => _ActiveTheme;
            set
            {
                if (_Application is null) return;

                // Special case for system theme: allow that one through to resample the system colors
                if (_ActiveTheme == value && !IsSystemTheme(value)) return;

                if (value is not Theme theme) return;

                try
                {
                    // Refresh system colors on selection of System theme
                    // (to account for changes the user might have made)
                    if (IsSystemTheme(value))
                    {
                        SampleSystemColors();
                    }
                    
                    _Application.Resources.MergedDictionaries.Add(theme.Resource);
                    _Application.Resources.MergedDictionaries.Remove(ActiveTheme.Resource);
                    _ActiveTheme = theme;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /////////////////////////////
        #endregion Properties
        ///////////////////////////////////////////////////////////
        
        private static bool IsSystemTheme(Theme theme)
        {
            return theme.DisplayName.Equals("system", StringComparison.CurrentCultureIgnoreCase);
        }

        private static void SampleSystemColors()
        {
            Theme? theme = Themes.Where(t => t.DisplayName.Equals("system", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if(theme is null) return;

            theme.Resource["BackgroundNormal"] = SystemColors.ControlColor;
            theme.Resource["BackgroundSelected"] = SystemColors.HighlightColor;
            theme.Resource["BackgroundInactive"] = SystemColors.HighlightColor;
            theme.Resource["BackgroundDisabled"] = SystemColors.ControlColor;
            theme.Resource["BackgroundMouseOver"] = SystemColors.ControlLightColor;
            theme.Resource["BackgroundPressed"] = SystemColors.ControlDarkColor;
            theme.Resource["BackgroundLight"] = SystemColors.ControlDarkColor;
            theme.Resource["BackgroundMedium"] = SystemColors.ControlDarkColor;
            theme.Resource["BackgroundDark"] = SystemColors.ControlDarkColor;

            theme.Resource["BackgroundNormalBrushKey"] = SystemColors.ControlBrush;
            theme.Resource["BackgroundSelectedBrushKey"] = SystemColors.MenuHighlightBrush;
            theme.Resource["BackgroundInactiveBrushKey"] = SystemColors.InactiveSelectionHighlightBrush;
            theme.Resource["BackgroundDisabledBrushKey"] = SystemColors.ControlBrush;
            theme.Resource["BackgroundMouseOverBrushKey"] = SystemColors.ControlLightColor;
            theme.Resource["BackgroundPressedBrushKey"] = SystemColors.ControlDarkColor;
            theme.Resource["BackgroundLightBrushKey"] = SystemColors.ControlDarkColor;
            theme.Resource["BackgroundMediumBrushKey"] = SystemColors.ControlDarkColor;
            theme.Resource["BackgroundDarkBrushKey"] = SystemColors.ControlDarkColor;

            theme.Resource["ForegroundNormal"] = SystemColors.ControlTextColor;
            theme.Resource["ForegroundSelected"] = SystemColors.HighlightTextColor;
            theme.Resource["ForegroundInactive"] = SystemColors.HighlightTextColor;
            theme.Resource["ForegroundDisabled"] = SystemColors.GrayTextColor;
            theme.Resource["ForegroundMouseOver"] = SystemColors.ControlTextColor;
            theme.Resource["ForegroundPressed"] = SystemColors.ControlTextColor;
            theme.Resource["ForegroundLight"] = SystemColors.ControlTextColor;
            theme.Resource["ForegroundDark"] = SystemColors.ControlTextColor;

            theme.Resource["ForegroundNormalBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["ForegroundSelectedBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["ForegroundInactiveBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["ForegroundDisabledBrushKey"] = SystemColors.GrayTextBrush;
            theme.Resource["ForegroundMouseOverBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["ForegroundPressedBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["ForegroundLightBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["ForegroundDarkBrushKey"] = SystemColors.ControlTextBrush;

            theme.Resource["BorderNormal"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderSelected"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderInactive"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderDisabled"] = SystemColors.InactiveBorderColor;
            theme.Resource["BorderMouseOver"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderPressed"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderLight"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderMedium"] = SystemColors.ActiveBorderColor;
            theme.Resource["BorderDark"] = SystemColors.ActiveBorderColor;

            theme.Resource["BorderNormalBrushKey"] = SystemColors.ActiveBorderBrush;
            theme.Resource["BorderSelectedBrushKey"] = SystemColors.ActiveBorderBrush;
            theme.Resource["BorderInactiveBrushKey"] = SystemColors.InactiveBorderBrush;
            theme.Resource["BorderDisabledBrushKey"] = SystemColors.InactiveBorderBrush;
            theme.Resource["BorderMouseOverBrushKey"] = SystemColors.ControlTextBrush;
            theme.Resource["BorderPressedBrushKey"] = SystemColors.ActiveBorderBrush;
            theme.Resource["BorderLightBrushKey"] = SystemColors.ActiveBorderBrush;
            theme.Resource["BorderMediumBrushKey"] = SystemColors.ActiveBorderBrush;
            theme.Resource["BorderDarkBrushKey"] = SystemColors.ActiveBorderBrush;

            theme.Resource["ControlNormal"] = SystemColors.ControlColor;
            theme.Resource["ControlSelected"] = SystemColors.HighlightColor;
            theme.Resource["ControlInactive"] = SystemColors.HighlightColor;
            theme.Resource["ControlDisabled"] = SystemColors.ControlColor;
            theme.Resource["ControlMouseOver"] = SystemColors.ControlLightLightColor;
            theme.Resource["ControlPressed"] = SystemColors.ControlDarkColor;
            theme.Resource["ControlLight"] = SystemColors.ControlDarkColor;
            theme.Resource["ControlDark"] = SystemColors.ControlDarkColor;

            theme.Resource["ControlNormalBrushKey"] = SystemColors.ControlBrush;
            theme.Resource["ControlSelectedBrushKey"] = SystemColors.ControlDarkBrush;
            theme.Resource["ControlInactiveBrushKey"] = SystemColors.InactiveSelectionHighlightBrush;
            theme.Resource["ControlDisabledBrushKey"] = SystemColors.InactiveSelectionHighlightBrush;
            theme.Resource["ControlMouseOverBrushKey"] = SystemColors.ControlLightBrush;
            theme.Resource["ControlPressedBrushKey"] = SystemColors.ControlDarkBrush;
            theme.Resource["ControlLightBrushKey"] = SystemColors.ControlLightBrush;
            theme.Resource["ControlMediumBrushKey"] = SystemColors.ControlLightBrush;
            theme.Resource["ControlDarkBrushKey"] = SystemColors.ControlDarkBrush;
        }

        private static void BuildTheme(Theme.eThemeType themeType, string name, string description, string filename)
        {
            string uri = $"/WpfThemer;component/Themes/{filename}";
            Themes.Add(new Theme(
                themeType,
                name,
                description,
                new ResourceDictionary() { Source = new Uri(uri, UriKind.RelativeOrAbsolute) }));
        }

        private static void BuildTemplate(string filename)
        {
            string uri = $"/WpfThemer;component/Templates/{filename}";
            Templates.Add(new ResourceDictionary() { Source = new Uri(uri, UriKind.RelativeOrAbsolute) });
        }

        static ThemeManager()
        {
            //BuildTheme("Default", "Default Theme", "Theme_Default.xaml");
            BuildTheme(Theme.eThemeType.Undefined, "System", "System Theme", "Theme_System.xaml");
            BuildTheme(Theme.eThemeType.Dark, "Dark", "Dark Theme", "Theme_Dark.xaml");
            BuildTheme(Theme.eThemeType.Light, "Light", "Light Theme", "Theme_Light.xaml");            
            _ActiveTheme = Themes.First();

            BuildTemplate("Button.xaml");
            BuildTemplate("CheckBox.xaml");
            BuildTemplate("ComboBox.xaml");
            BuildTemplate("ComboBoxItem.xaml");
            BuildTemplate("DataGrid.xaml");
            BuildTemplate("ListBox.xaml");
            BuildTemplate("ListBoxItem.xaml");
            BuildTemplate("ListView.xaml");
            BuildTemplate("ListViewItem.xaml");
            BuildTemplate("Menu.xaml");
            BuildTemplate("RadioButton.xaml");
            BuildTemplate("ScrollBar.xaml");
            BuildTemplate("Separator.xaml");
            BuildTemplate("StatusBar.xaml");
            BuildTemplate("TabControl.xaml");
            BuildTemplate("TabItem.xaml");
            ////BuildTemplate("TextBlock.xaml");
            BuildTemplate("TextBox.xaml");
            BuildTemplate("ToggleButton.xaml");
            BuildTemplate("Toolbar.xaml");
            BuildTemplate("Window.xaml");
        }

        private static void ReloadTemplates()
        {
            foreach (var template in Templates)
            {
                _Application?.Resources.MergedDictionaries.Remove(template);
                _Application?.Resources.MergedDictionaries.Add(template);
            }
        }

        public static void SetApplication(Application? application)
        {
            _Application = application;
            if (_Application is null) return;

            application?.Resources.MergedDictionaries.Add(ActiveTheme.Resource);
            ReloadTemplates();
        }

        public static void SetTheme(string themeName)
        {
            foreach (var theme in Themes)
            {
                if (theme.DisplayName.Equals(themeName, StringComparison.CurrentCultureIgnoreCase))
                {
                    ActiveTheme = theme;
                    ReloadTemplates();
                    return;
                }
            }
        }

        /// <summary>
        /// Use this to add a theme from your own assembly
        /// </summary>
        /// <param name="theme"></param>
        public static void AddExternalTheme(Theme theme)
        {
            Themes.Add(theme);
        }
    }
}
