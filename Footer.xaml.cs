using AutoStop.Storages;

namespace AutoStop
{
    public partial class Footer : ContentView
    {
        public Footer()
        {
            InitializeComponent();
            SetButtonColors();
            Shell.Current.Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            SetButtonColors();
        }

        private void SetButtonColors()
        {
            var currentRoute = Shell.Current.CurrentState.Location.ToString();

            SearchButton.BackgroundColor = currentRoute.Contains("SearchPage") ? Colors.AntiqueWhite : Colors.Transparent;
            AddButton.BackgroundColor = currentRoute.Contains("AddPage") ? Colors.AntiqueWhite : Colors.Transparent;
            HistoryButton.BackgroundColor = currentRoute.Contains("HistoryPage") ? Colors.AntiqueWhite : Colors.Transparent;
            ChatsButton.BackgroundColor = currentRoute.Contains("ChatsPage") ? Colors.AntiqueWhite : Colors.Transparent;
            ProfileButton.BackgroundColor = currentRoute.Contains("ProfilePage") ? Colors.AntiqueWhite : Colors.Transparent;
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SearchPage");
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddPage");
        }

        private async void OnHistoryButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HistoryPage");
        }

        private async void OnChatsButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ChatsPage");
        }

        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }
    }
}