using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace SHA256Now.Views
{
    public sealed partial class FileHashSHA256Page : Page
    {
        MainPage MainPage { get; }

        public FileHashSHA256Page()
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

        private async void PickSingleFileButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is Button)
            {
                FileOpenPicker fileOpenPicker = new FileOpenPicker();
                fileOpenPicker.ViewMode = PickerViewMode.List;
                fileOpenPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                fileOpenPicker.FileTypeFilter.Add("*");

                StorageFile storageFile = await fileOpenPicker.PickSingleFileAsync();
                if (storageFile != null)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using (IRandomAccessStream iRandomAccessStream = await storageFile.OpenAsync(FileAccessMode.Read))
                    {
                        FileNameTextBlock.Text = storageFile.Name;

                        using (SHA256 Sha256 = SHA256.Create())
                        {
                            byte[] bytes = Sha256.ComputeHash(iRandomAccessStream.AsStreamForRead());
                            for (int i = 0; i < bytes.Length; i++)
                            {
                                stringBuilder.Append($"{bytes[i]:x2}");
                            }
                        }
                    }
                    FileHashTextBlock.Text = stringBuilder.ToString();
                }
                else
                {
                    MainPage.NotifyUser("Operation canceled.", NotifyType.StatusMessage);
                }
            }
        }

        private void FileHashToCompareTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FileHashTextBlock.Text.Equals(FileHashToCompareTextBox.Text))
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
