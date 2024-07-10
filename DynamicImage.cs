using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Diagnostics;

namespace WpfThemer
{
    /// <summary>
    /// Use instead of System.Windows.Controls.Image to automatically update the image source when the theme changes.
    /// </summary>
    public class DynamicImage : Image, INotifyPropertyChanged
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
        

        private string _SymbolName = string.Empty;
        public string SymbolName
        {
            get => _SymbolName;
            set
            {
                _SymbolName = value;
            }
        }

        public DynamicImage() : base()
        {
            ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }

        private void ThemeManager_ThemeChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Theme.eThemeType themeType = ThemeManager.ActiveTheme.ThemeType;

            DynamicSymbol? symbol = ThemeSymbolManager.Symbols.Where(s =>
                (s.ThemeType == themeType) &&
                (s.Name.Equals(SymbolName, System.StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

            if (symbol is not null)
            {
                //Source.SetValue(Image.SourceProperty, symbol.Value);
                Source = new System.Windows.Media.Imaging.BitmapImage(symbol.Value);
            }
            else
            {
                Debug.WriteLine("ThemeManager_ThemeChanged: Symbol not found");                
            }
        }
    }
}
