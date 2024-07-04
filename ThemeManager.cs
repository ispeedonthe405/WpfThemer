
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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
                if (_ActiveTheme == value) return;
                if (_Application is null) return;

                _Application.Resources.MergedDictionaries.Remove(ActiveTheme.Resource);
                _ActiveTheme = value;
                _Application.Resources.MergedDictionaries.Add(ActiveTheme.Resource);
            }
        }

        /////////////////////////////
        #endregion Properties
        ///////////////////////////////////////////////////////////
        
        private static void BuildTheme(string name, string description, string filename)
        {
            string uri = string.Format("/WpfThemer;component/Themes/{0}", filename);
            Themes.Add(new Theme(
                name,
                description,
                new ResourceDictionary() { Source = new Uri(uri, UriKind.RelativeOrAbsolute) }));
        }

        private static void BuildTemplate(string filename)
        {
            string uri = string.Format("/WpfThemer;component/Templates/{0}", filename);
            Templates.Add(new ResourceDictionary() { Source = new Uri(uri, UriKind.RelativeOrAbsolute) });
        }

        static ThemeManager()
        {
            BuildTheme("Default", "Default Theme", "Theme_Default.xaml");
            BuildTheme("Dark", "Dark Theme", "Theme_Dark.xaml");
            BuildTheme("Light", "Light Theme", "Theme_Light.xaml");
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
            BuildTemplate("Separator.xaml");
            BuildTemplate("TabControl.xaml");
            BuildTemplate("TabItem.xaml");
            //BuildTemplate("TextBlock.xaml");
            BuildTemplate("TextBox.xaml");
            BuildTemplate("ToggleButton.xaml");
            BuildTemplate("Toolbar.xaml");
            BuildTemplate("Window.xaml");
        }

        public static void SetApplication(Application? application)
        {
            _Application = application;
            if (_Application is null) return;

            application?.Resources.MergedDictionaries.Add(ActiveTheme.Resource);
            foreach (var template in Templates)
            {
                _Application.Resources.MergedDictionaries.Add(template);
            }
        }

        public static void SetTheme(string themeName)
        {
            foreach (var theme in Themes)
            {
                if (theme.DisplayName.Equals(themeName, StringComparison.CurrentCultureIgnoreCase))
                {
                    ActiveTheme = theme;
                    return;
                }
            }
        }
    }
}
