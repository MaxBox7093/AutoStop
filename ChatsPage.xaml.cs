using Microsoft.Maui.Controls;

namespace AutoStop
{
    public partial class ChatsPage : ContentPage
    {
        public ChatsPage()
        {
            InitializeComponent();
        }

        private async void OnChatTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatPage());
        }
    }
}
