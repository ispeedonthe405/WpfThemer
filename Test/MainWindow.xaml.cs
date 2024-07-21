using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfThemer;

namespace Test
{
    internal class DgridItem
    {
        public string Value0 { get; set; } = string.Empty;
        public string Value1 { get; set; } = string.Empty;
        public string Value2 { get; set; } = string.Empty;

        public static DgridItem Generate()
        {
            Random rnd = new();
            DgridItem item = new()
            {
                Value0 = rnd.NextDouble().ToString(),
                Value1 = rnd.NextDouble().ToString(),
                Value2 = rnd.NextDouble().ToString()
            };
            return item;
        }
    }

    /// <summary>
    /// The EventLog and Logger are transplanted here from Eudora.Net.
    /// Here it serves as more complex ListView content
    /// </summary>
    internal class LogEvent
    {
        public enum EventCategory
        {
            Information,
            Warning,
            Error,
            Notify,
            Debug
        }
        public EventCategory Category { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public Color BrushColor { get; set; } = Colors.Orange;
    }

    internal class Logger
    {
        public static ObservableCollection<LogEvent> Events = new ObservableCollection<LogEvent>();

        private static string FormatMessage(LogEvent.EventCategory eventCategory, DateTime timestamp, string message)
        {
            string output = string.Empty;
            string header = string.Empty;
            switch (eventCategory)
            {
                case LogEvent.EventCategory.Information:
                    header = "Info";
                    break;

                case LogEvent.EventCategory.Warning:
                    header = "Warning";
                    break;

                case LogEvent.EventCategory.Error:
                    header = "Error";
                    break;

                case LogEvent.EventCategory.Debug:
                    header = "Debug";
                    break;
            }
            //output = String.Format("[{0}] [{1}]:\t{2}", timestamp.ToString(), header, message);
            output = $@"{timestamp} {header}: {message}";
            return output;
        }

        private static Color BrushColorFromCategory(LogEvent.EventCategory category)
        {
            Color color = Colors.Black;
            switch (category)
            {
                case LogEvent.EventCategory.Warning:
                    color = Colors.DarkOrange;
                    break;

                case LogEvent.EventCategory.Error:
                    color = Colors.Crimson;
                    break;

                case LogEvent.EventCategory.Notify:
                    color = Colors.Green;
                    break;
            }
            return color;
        }

        public static void NewEvent(LogEvent.EventCategory category, string message)
        {
            if (!App.Current.Dispatcher.CheckAccess())
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() => NewEvent(category, message)));
                return;
            }
            LogEvent ev = new()
            {
                Category = category,
                Timestamp = DateTime.Now,
                Message = message,
                BrushColor = BrushColorFromCategory(category)
            };
            Events.Add(ev);
        }

        public static void LogException(Exception ex)
        {
            // Show the exception message by default
            string message = ex.Message;

            // If possible, get a more detailed message consisting of
            // some data from the stack frame. I heart C#.
            System.Diagnostics.StackTrace stackTrace = new(ex);
            var frame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
            if (frame is not null)
            {
                MethodBase? mb = frame.GetMethod();
                if (mb is not null)
                {
                    message = $"{mb.ReflectedType}.{mb.Name}{Environment.NewLine}{ex.Message}";
                }
            }
            NewEvent(LogEvent.EventCategory.Warning, message);
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DgridItem> GridItems { get; set; } = [];
        Logger Logger { get; set; } = new();

        private void BuildSymbols()
        {
            ThemeSymbolManager.AddSymbol("add", new("pack://application:,,,/images/dark/add.png"), Theme.eThemeType.Light);
            ThemeSymbolManager.AddSymbol("add", new("pack://application:,,,/images/light/add.png"), Theme.eThemeType.Dark);
            ThemeSymbolManager.AddSymbol("add", new("pack://application:,,,/images/dark/add.png"), Theme.eThemeType.Undefined);

            ThemeSymbolManager.AddSymbol("cancel", new("pack://application:,,,/images/dark/cancel.png"), Theme.eThemeType.Light);
            ThemeSymbolManager.AddSymbol("cancel", new("pack://application:,,,/images/light/cancel.png"), Theme.eThemeType.Dark);
            ThemeSymbolManager.AddSymbol("cancel", new("pack://application:,,,/images/dark/cancel.png"), Theme.eThemeType.Undefined);

            ThemeSymbolManager.AddSymbol("format_align_left", new("pack://application:,,,/images/dark/format_align_left.png"), Theme.eThemeType.Light);
            ThemeSymbolManager.AddSymbol("format_align_left", new("pack://application:,,,/images/light/format_align_left.png"), Theme.eThemeType.Dark);
            ThemeSymbolManager.AddSymbol("format_align_left", new("pack://application:,,,/images/dark/format_align_left.png"), Theme.eThemeType.Undefined);

            ThemeSymbolManager.AddSymbol("format_align_center", new("pack://application:,,,/images/dark/format_align_center.png"), Theme.eThemeType.Light);
            ThemeSymbolManager.AddSymbol("format_align_center", new("pack://application:,,,/images/light/format_align_center.png"), Theme.eThemeType.Dark);
            ThemeSymbolManager.AddSymbol("format_align_center", new("pack://application:,,,/images/dark/format_align_center.png"), Theme.eThemeType.Undefined);

            ThemeSymbolManager.AddSymbol("format_align_right", new("pack://application:,,,/images/dark/format_align_right.png"), Theme.eThemeType.Light);
            ThemeSymbolManager.AddSymbol("format_align_right", new("pack://application:,,,/images/light/format_align_right.png"), Theme.eThemeType.Dark);
            ThemeSymbolManager.AddSymbol("format_align_right", new("pack://application:,,,/images/dark/format_align_right.png"), Theme.eThemeType.Undefined);
        }

        public MainWindow()
        {
            InitializeComponent();

            BuildSymbols();

            cb_Theme.ItemsSource = ThemeManager.Themes;
            cb_Theme.DisplayMemberPath = "DisplayName";

            for (int i = 0; i < 30; i++)
            {
                GridItems.Add(DgridItem.Generate());
                tb_BigText.Text += "This is a test of the emergency broadcast system. This is only a test. ";
            }
            datagrid.ItemsSource = GridItems;

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    throw new Exception("This is a test exception.");
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                }
            }

            ThemeManager.SetTheme("Light");
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

        private void New_UserControl_Click(object sender, RoutedEventArgs e)
        {
            uc_Test uc = new();
            MainGrid.Children.Add(uc);
            Grid.SetRow(uc, 4);
            Grid.SetColumn(uc, 3);
            Grid.SetRowSpan(uc, 2);
            uc.Visibility = Visibility.Visible;
        }
    }
}