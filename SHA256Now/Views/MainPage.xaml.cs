using SHA256Now.NavigationModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SHA256Now.Views
{
    public sealed partial class MainPage : Page
    {
        DispatcherTimer HideTextTimer { get; }

        int CounterIntervalSeconds { get; set; }

        public static MainPage CurrentMainPage { get; set; }

        public ListView MenuNavigationListView
        {
            get { return NavigationListView; }
        }

        public MainPage()
        {
            InitializeComponent();

            CurrentMainPage = this;

            HideTextTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            HideTextTimer.Tick += HideTextTimer_Tick;

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            GoToFileHashPage();
        }

        private void HideTextTimer_Tick(object sender, object e)
        {
            CounterIntervalSeconds++;
            if (CounterIntervalSeconds >= 5)
            {
                CounterIntervalSeconds = 0;
                //hide text by setting it to empty
                NotifyUser(string.Empty, NotifyType.StatusMessage);
                //after 5 seconds stop the timer
                HideTextTimer.Stop();
            }
        }

        public void GoToHomePage()
        {
            MainFrame.Navigate(typeof(HomePage));
            MenuNavigationListView.SelectedIndex = 0;
        }

        public void GoToFileHashPage()
        {
            MainFrame.Navigate(typeof(FileHashSHA256Page));
            MenuNavigationListView.SelectedIndex = 1;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // code here
            ObservableCollection<NavigationMenuItem> allNavigationMenuItems = GetNavigationMenuItems();

            NavigationMenuCollectionViewSource.Source = allNavigationMenuItems;

            if (allNavigationMenuItems.Count > 0)
            {
                NavigationListView.SelectedItem = allNavigationMenuItems.FirstOrDefault();
            }

            NotifyUser(string.Empty, NotifyType.StatusMessage);
            // code here
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // code here
            // code here
        }

        private static ObservableCollection<NavigationMenuItem> GetNavigationMenuItems()
        {
            return new ObservableCollection<NavigationMenuItem>(){
                new NavigationMenuItem(){
                    MenuSymbolIcon = Symbol.Home,
                    MenuToolTip = "Home",
                    MenuItemName = "Home"
                },
                new NavigationMenuItem(){
                    MenuSymbolIcon = Symbol.Calculator,
                    MenuToolTip = "File hash SHA256",
                    MenuItemName = "File hash SHA256"
                },
                new NavigationMenuItem(){
                    MenuSymbolIcon = Symbol.Calculator,
                    MenuToolTip = "Text hash SHA256",
                    MenuItemName = "Text hash SHA256"
                },
                  new NavigationMenuItem(){
                    MenuSymbolIcon = Symbol.Document,
                    MenuToolTip = "Instructions",
                    MenuItemName = "Instructions"
                },
                new NavigationMenuItem(){
                    MenuSymbolIcon = Symbol.Document,
                    MenuToolTip = "About",
                    MenuItemName = "About"
                },
                new NavigationMenuItem(){
                    MenuSymbolIcon = Symbol.Document,
                    MenuToolTip = "Privacy statement",
                    MenuItemName = "Privacy statement"
                },
            };
        }

        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is Button)
            {
                MenuSplitView.IsPaneOpen = MenuSplitView.IsPaneOpen ? false : true;

                switch (MenuSplitView.IsPaneOpen)
                {
                    case true:
                        ToolTipService.SetToolTip(HamburgerButton, "Close meny pane");
                        break;
                    case false:
                        ToolTipService.SetToolTip(HamburgerButton, "Open meny pane");
                        break;
                    default:
                        ToolTipService.SetToolTip(HamburgerButton, "Close meny pane");
                        break;
                }
            }
        }

        private void NavigationListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is ListView listView)
            {
                if (listView.SelectedItem is NavigationMenuItem navigationMenuItem)
                {
                    switch (navigationMenuItem.MenuItemName)
                    {
                        case "Home":
                            MainFrame.Navigate(typeof(HomePage));
                            break;
                        case "File hash SHA256":
                            MainFrame.Navigate(typeof(FileHashSHA256Page));
                            break;
                        case "Text hash SHA256":
                            MainFrame.Navigate(typeof(TextHashSHA256Page));
                            break;
                        case "Instructions":
                            MainFrame.Navigate(typeof(InstructionsPage));
                            break;
                        case "About":
                            MainFrame.Navigate(typeof(AboutPage));
                            break;
                        case "Privacy statement":
                            MainFrame.Navigate(typeof(PrivacyStatementPage));
                            break;
                        default:
                            MainFrame.Navigate(typeof(HomePage));
                            break;
                    }
                    MenuSplitView.IsPaneOpen = false;
                    ToolTipService.SetToolTip(HamburgerButton, "Open meny pane");
                }
            }
        }

        /// <summary>
        /// Used to display messages to the user
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="type"></param>
        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
                case NotifyType.ErrorMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
            }
            StatusTextBlock.Text = strMessage;

            // Collapse the StatusBlock if it has no text to conserve real estate.
            StatusBorder.Visibility = (!string.IsNullOrEmpty(StatusTextBlock.Text)) ? Visibility.Visible : Visibility.Collapsed;
            if (!string.IsNullOrEmpty(StatusTextBlock.Text))
            {
                StatusBorder.Visibility = Visibility.Visible;
                StatusPanel.Visibility = Visibility.Visible;
            }
            else
            {
                StatusBorder.Visibility = Visibility.Collapsed;
                StatusPanel.Visibility = Visibility.Collapsed;
            }

            // Collapse the StatusTextBlock if it has no text to conserve real estate.
            StatusBorder.Visibility = (!string.IsNullOrEmpty(StatusTextBlock.Text)) ? Visibility.Visible : Visibility.Collapsed;
            if (!string.IsNullOrEmpty(StatusTextBlock.Text))
            {
                StatusBorder.Visibility = Visibility.Visible;
                StatusBorder.Visibility = Visibility.Visible;

                CounterIntervalSeconds = 0;
                if (HideTextTimer != null && !HideTextTimer.IsEnabled)
                {
                    HideTextTimer.Start();
                }
            }
            else
            {
                StatusBorder.Visibility = Visibility.Collapsed;
                StatusBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void StatusBorder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //hide
            NotifyUser(string.Empty, NotifyType.StatusMessage);
        }
    }

    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
}
