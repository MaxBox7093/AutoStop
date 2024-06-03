using AutoStop.APIServices;
using AutoStop.Storages;

namespace AutoStop
{
    public partial class Footer : ContentView
    {
        CheckIsDriverAPI _drivercheckAPI;

        public Footer()
        {
            InitializeComponent();
            SetButtonColors(FooterStateStorage.State);
            _drivercheckAPI = new CheckIsDriverAPI();
        }

        private void SetButtonColors(string currentRoute)
        {
            switch(currentRoute)
            {
                case "SearchPage":
                    SearchButton.BackgroundColor = Colors.AntiqueWhite;
                    AddButton.BackgroundColor = Colors.Transparent;
                    HistoryButton.BackgroundColor = Colors.Transparent;
                    ChatsButton.BackgroundColor = Colors.Transparent;
                    ProfileButton.BackgroundColor = Colors.Transparent;
                    break;

                case "AddPage":
                    SearchButton.BackgroundColor = Colors.Transparent;
                    AddButton.BackgroundColor = Colors.AntiqueWhite;
                    HistoryButton.BackgroundColor = Colors.Transparent;
                    ChatsButton.BackgroundColor = Colors.Transparent;
                    ProfileButton.BackgroundColor = Colors.Transparent;
                    break;

                case "HistoryPage":
                    SearchButton.BackgroundColor = Colors.Transparent;
                    AddButton.BackgroundColor = Colors.Transparent;
                    HistoryButton.BackgroundColor = Colors.AntiqueWhite;
                    ChatsButton.BackgroundColor = Colors.Transparent;
                    ProfileButton.BackgroundColor = Colors.Transparent;
                    break;

                case "ChatsPage":
                    SearchButton.BackgroundColor = Colors.Transparent;
                    AddButton.BackgroundColor = Colors.Transparent;
                    HistoryButton.BackgroundColor = Colors.Transparent;
                    ChatsButton.BackgroundColor = Colors.AntiqueWhite;
                    ProfileButton.BackgroundColor = Colors.Transparent;
                    break;

                case "ProfilePage":
                    SearchButton.BackgroundColor = Colors.Transparent;
                    AddButton.BackgroundColor = Colors.Transparent;
                    HistoryButton.BackgroundColor = Colors.Transparent;
                    ChatsButton.BackgroundColor = Colors.Transparent;
                    ProfileButton.BackgroundColor = Colors.AntiqueWhite;
                    break;
            }
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            FooterStateStorage.State = "SearchPage";
            await PushToPageAsync(new SearchPage());
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            bool tmp = await OnDriverChecked(UsersStorage.CurrentUser.Phone);
            var currentPage = Shell.Current.CurrentPage;
            if (tmp)
            {
                FooterStateStorage.State = "AddPage";
                await PushToPageAsync(new AddPage());
            }
            else
            {
                await currentPage.DisplayAlert("Ошибка", "Эта функция доступна только пользователям с учётной записью водителя", "OK");
            }
        }

        private async void OnHistoryButtonClicked(object sender, EventArgs e)
        {
            FooterStateStorage.State = "HistoryPage";
            await PushToPageAsync(new HistoryPage());
        }

        private async void OnChatsButtonClicked(object sender, EventArgs e)
        {
            FooterStateStorage.State = "ChatsPage";
            await PushToPageAsync(new ChatsPage());
        }

        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            FooterStateStorage.State = "ProfilePage";
            await PushToPageAsync(new ProfilePage());
        }

        private async Task PushToPageAsync(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page);
        }

        private async Task<bool> OnDriverChecked(string phone)
        {
            bool success = await _drivercheckAPI.Check(phone);
            return success;
        }
    }
}
