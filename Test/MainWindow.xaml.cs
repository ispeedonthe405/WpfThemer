﻿using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DgridItem> GridItems { get; set; } = [];
        Logger Logger { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();

            cb_Theme.ItemsSource = ThemeManager.Themes;
            cb_Theme.DisplayMemberPath = "DisplayName";

            for (int i = 0; i < 30; i++)
            {
                GridItems.Add(DgridItem.Generate());
                tb_BigText.Text += "This is a test of the emergency theme system. This is only a test. ";
            }
            datagrid.ItemsSource = GridItems;

            LogView.DataContext = Logger.Events;
            LogView.ItemsSource = Logger.Events;
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int cat = rand.Next(0, 5);
                LogEvent.EventCategory category = (LogEvent.EventCategory)cat;
                Logger.NewEvent(category, $"This is test event {i}");
            }

            //ThemeManager.SetTheme("Green");
            //cb_Theme.SelectedItem = ThemeManager.ActiveTheme;
            cb_Theme.SelectedIndex = cb_Theme.Items.Count - 1;
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
            Grid.SetRow(uc, 1);
            Grid.SetColumn(uc, 5);
            uc.Visibility = Visibility.Visible;
        }
    }
}