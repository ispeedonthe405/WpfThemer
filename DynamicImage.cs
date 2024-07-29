using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;

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



        public string SymbolName
        {
            get { return (string)GetValue(SymbolNameProperty); }
            set 
            { 
                SetValue(SymbolNameProperty, value);
                ApplySourceToSymbol();
            }
        }

        public static readonly DependencyProperty SymbolNameProperty =
            DependencyProperty.Register(
                "SymbolName", 
                typeof(string), 
                typeof(DynamicImage),
                new PropertyMetadata("add", OnSymbolNameChanged),
                SymbolNameValidationCallback);

        private static void OnSymbolNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is DynamicImage dynamicImage)
            {
                dynamicImage.ApplySourceToSymbol();
            }
        }

        private static bool SymbolNameValidationCallback(object value)
        {
            if (value is not string) return false;
            return true;
        }

        private void ApplySourceToSymbol()
        {

            DynamicSymbol? symbol = ThemeSymbolManager.ActiveSymbols.Where(s =>
                (s.Name.Equals(SymbolName, System.StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

            if (symbol is not null)
            {
                try
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(symbol.Value);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine($"ApplySourceToSymbol: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine($"ApplySourceToSymbol: Symbol {SymbolName} not found");
            }
        }

        public DynamicImage() : base()
        {
            ThemeManager.ThemeChanged += (sender, e) => ApplySourceToSymbol();
            ApplySourceToSymbol();
        }
    }
}
