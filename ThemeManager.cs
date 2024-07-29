
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
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
        public static event PropertyChangedEventHandler? ThemeChanged;

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
            if(propertyName.Equals("ActiveTheme", StringComparison.CurrentCultureIgnoreCase))
            {
                ThemeChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
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
        private static Application? HostApp;

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
                if (HostApp is null) return;

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

                    HostApp.Resources.MergedDictionaries.Remove(_ActiveTheme.Resource);
                    _ActiveTheme = theme;
                    HostApp.Resources.MergedDictionaries.Add(_ActiveTheme.Resource);

                    ReloadTemplates();

                    ThemeChanged?.Invoke(value, new PropertyChangedEventArgs("ActiveTheme"));
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
            //Theme? theme = Themes.Where(t => t.DisplayName.Equals("system", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //if(theme is null) return;

            Debug.WriteLine("Sampling system colors");

            ActiveTheme.Resource["BackgroundNormal"] = SystemColors.WindowColor;
            ActiveTheme.Resource["ForegroundNormal"] = SystemColors.WindowTextColor;
            ActiveTheme.Resource["BorderNormal"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BackgroundNormal"] = SystemColors.ControlColor;
            ActiveTheme.Resource["BackgroundSelected"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["BackgroundInactive"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["BackgroundDisabled"] = SystemColors.ControlColor;
            ActiveTheme.Resource["BackgroundMouseOver"] = SystemColors.ControlLightColor;
            ActiveTheme.Resource["BackgroundPressed"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundLight"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundMedium"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundDark"] = SystemColors.ControlDarkColor;

            ActiveTheme.Resource["BackgroundNormalBrushKey"] = SystemColors.ControlBrush;
            ActiveTheme.Resource["BackgroundSelectedBrushKey"] = SystemColors.MenuHighlightBrush;
            ActiveTheme.Resource["BackgroundInactiveBrushKey"] = SystemColors.InactiveSelectionHighlightBrush;
            ActiveTheme.Resource["BackgroundDisabledBrushKey"] = SystemColors.ControlBrush;
            ActiveTheme.Resource["BackgroundMouseOverBrushKey"] = SystemColors.ControlLightColor;
            ActiveTheme.Resource["BackgroundPressedBrushKey"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundLightBrushKey"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundMediumBrushKey"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BackgroundDarkBrushKey"] = SystemColors.ControlDarkColor;

            ActiveTheme.Resource["ForegroundNormal"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundSelected"] = SystemColors.HighlightTextColor;
            ActiveTheme.Resource["ForegroundInactive"] = SystemColors.HighlightTextColor;
            ActiveTheme.Resource["ForegroundDisabled"] = SystemColors.GrayTextColor;
            ActiveTheme.Resource["ForegroundMouseOver"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundPressed"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundLight"] = SystemColors.ControlTextColor;
            ActiveTheme.Resource["ForegroundDark"] = SystemColors.ControlTextColor;

            ActiveTheme.Resource["ForegroundNormalBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundSelectedBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundInactiveBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundDisabledBrushKey"] = SystemColors.GrayTextBrush;
            ActiveTheme.Resource["ForegroundMouseOverBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundPressedBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundLightBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["ForegroundDarkBrushKey"] = SystemColors.ControlTextBrush;

            ActiveTheme.Resource["BorderNormal"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderSelected"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderInactive"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderDisabled"] = SystemColors.InactiveBorderColor;
            ActiveTheme.Resource["BorderMouseOver"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderPressed"] = SystemColors.ActiveBorderColor;
            ActiveTheme.Resource["BorderLight"] = SystemColors.ControlLightColor;
            ActiveTheme.Resource["BorderMedium"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["BorderDark"] = SystemColors.ControlDarkDarkColor;

            ActiveTheme.Resource["BorderNormalBrushKey"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderSelectedBrushKey"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderInactiveBrushKey"] = SystemColors.InactiveBorderBrush;
            ActiveTheme.Resource["BorderDisabledBrushKey"] = SystemColors.InactiveBorderBrush;
            ActiveTheme.Resource["BorderMouseOverBrushKey"] = SystemColors.ControlTextBrush;
            ActiveTheme.Resource["BorderPressedBrushKey"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderLightBrushKey"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderMediumBrushKey"] = SystemColors.ActiveBorderBrush;
            ActiveTheme.Resource["BorderDarkBrushKey"] = SystemColors.ActiveBorderBrush;

            ActiveTheme.Resource["ControlNormal"] = SystemColors.ControlColor;
            ActiveTheme.Resource["ControlSelected"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["ControlInactive"] = SystemColors.HighlightColor;
            ActiveTheme.Resource["ControlDisabled"] = SystemColors.ControlColor;
            ActiveTheme.Resource["ControlMouseOver"] = SystemColors.ControlLightLightColor;
            ActiveTheme.Resource["ControlPressed"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["ControlLight"] = SystemColors.ControlDarkColor;
            ActiveTheme.Resource["ControlDark"] = SystemColors.ControlDarkColor;

            ActiveTheme.Resource["ControlNormalBrushKey"] = SystemColors.ControlBrush;
            ActiveTheme.Resource["ControlSelectedBrushKey"] = SystemColors.ControlDarkBrush;
            ActiveTheme.Resource["ControlInactiveBrushKey"] = SystemColors.InactiveSelectionHighlightBrush;
            ActiveTheme.Resource["ControlDisabledBrushKey"] = SystemColors.InactiveSelectionHighlightBrush;
            ActiveTheme.Resource["ControlMouseOverBrushKey"] = SystemColors.ControlLightBrush;
            ActiveTheme.Resource["ControlPressedBrushKey"] = SystemColors.ControlDarkBrush;
            ActiveTheme.Resource["ControlLightBrushKey"] = SystemColors.ControlLightBrush;
            ActiveTheme.Resource["ControlMediumBrushKey"] = SystemColors.ControlLightBrush;
            ActiveTheme.Resource["ControlDarkBrushKey"] = SystemColors.ControlDarkBrush;


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
            //BuildTheme(Theme.eThemeType.Undefined, "System", "System Theme", "Theme_System.xaml");
            BuildTheme(Theme.eThemeType.Light, "Light", "Light Theme", "Theme_Light.xaml");
            BuildTheme(Theme.eThemeType.Dark, "Dark", "Dark Theme", "Theme_Dark.xaml");
            BuildTheme(Theme.eThemeType.Dark, "Green", "Green Theme", "Theme_Green.xaml");
            BuildTheme(Theme.eThemeType.Dark, "Blue Steel", "Blue Steel Theme", "Theme_BlueSteel.xaml");
            BuildTheme(Theme.eThemeType.Dark, "Blue Light", "Blue Light", "Theme_BlueLight.xaml");

            //_ActiveTheme = Themes.Last();

            BuildTemplate("Border.xaml");
            BuildTemplate("Button.xaml");
            BuildTemplate("CheckBox.xaml");
            BuildTemplate("ComboBox.xaml");
            BuildTemplate("DataGrid.xaml");
            BuildTemplate("GridSplitter.xaml");
            BuildTemplate("Label.xaml");
            BuildTemplate("ListBox.xaml");
            BuildTemplate("ListBoxItem.xaml");
            BuildTemplate("ListView.xaml");
            BuildTemplate("Menu.xaml");
            BuildTemplate("RadioButton.xaml");
            BuildTemplate("ScrollBar.xaml");
            BuildTemplate("Separator.xaml");
            BuildTemplate("StatusBar.xaml");
            BuildTemplate("TabControl.xaml");
            BuildTemplate("TextBlock.xaml");
            BuildTemplate("TextBox.xaml");
            //BuildTemplate("Thumb.xaml");
            BuildTemplate("ToggleButton.xaml");
            //BuildTemplate("Toolbar.xaml");
            BuildTemplate("ToolTip.xaml");
            BuildTemplate("TreeView.xaml");
            //BuildTemplate("Window.xaml");
        }


        private static void LoadTemplates()
        {
            foreach (var template in Templates)
            {
                HostApp?.Resources.MergedDictionaries.Add(template);
            }
        }

        private static void ReloadTemplates()
        {
            foreach (var template in Templates)
            {
                HostApp?.Resources.MergedDictionaries.Remove(template);
                //if (!IsSystemTheme(ActiveTheme))
                {
                    HostApp?.Resources.MergedDictionaries.Add(template);
                }
            }
        }

        public static void SetApplication(Application? application)
        {
            HostApp = application;
            if (HostApp is null) return;

            application?.Resources.MergedDictionaries.Add(ActiveTheme.Resource);
            LoadTemplates();
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
