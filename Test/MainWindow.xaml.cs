using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfThemer;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            cb_Theme.ItemsSource = ThemeManager.Themes;
            cb_Theme.DisplayMemberPath = "DisplayName";
            cb_Theme.SelectedItem = ThemeManager.ActiveTheme;
        }

        private void cb_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Theme.SelectedItem is Theme theme)
            {
                ThemeManager.ActiveTheme = theme;
                tb_Description.Text = theme.Description;
            }
        }
    }
}