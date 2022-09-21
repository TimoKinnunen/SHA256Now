using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SHA256Now.Views
{
    public sealed partial class HomePage : Page
    {
        MainPage MainPage { get; }

        //text input
        string rawInput { get; set; }

        public HomePage()
        {
            InitializeComponent();

            MainPage = MainPage.CurrentMainPage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // code here
            // code here
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            // code here
            // code here
        }
    }
}
