using System.Security.Cryptography;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace SHA256Now.Views
{
    public sealed partial class TextHashSHA256Page : Page
    {
        MainPage MainPage { get; }

        public TextHashSHA256Page()
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

        private void TextAsInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                StringBuilder stringBuilder = new StringBuilder();
                using (SHA256 Sha256 = SHA256.Create())
                {
                    byte[] bytes = Sha256.ComputeHash(Encoding.UTF8.GetBytes(TextAsInputTextBox.Text));
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        stringBuilder.Append($"{bytes[i]:x2}");
                    }
                }
                TextHashTextBlock.Text = stringBuilder.ToString();
                CompareHashes();
            }
        }

        private void TextHashToCompareTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CompareHashes();
        }

        private void CompareHashes()
        {
            if (TextHashTextBlock.Text.Equals(TextHashToCompareTextBox.Text))
            {
                MainPage.NotifyUser("Hashes are equal.", NotifyType.StatusMessage);
                HashCompareResultTextBlock.Text = "Hashes are equal.";
            }
            else
            {
                HashCompareResultTextBlock.Text = "Hashes are not equal.";
            }
        }

        #region MenuAppBarButton
        private void HomeAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is AppBarButton)
            {
                MainPage.GoToHomePage();
            }
        }
        #endregion MenuAppBarButton
    }
}
