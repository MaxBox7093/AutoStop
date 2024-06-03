using Microsoft.Maui.Controls;
using AutoStop.APIServices;
using AutoStop.Storages;
using AutoStop.Models;
using System.Collections.ObjectModel;

namespace AutoStop
{
    public partial class ChatsPage : ContentPage
    {
        private readonly GetChatListAPI _getChatAPI;
        public ObservableCollection<Chat> Chats { get; set; }

        public ChatsPage()
        {
            InitializeComponent();
            Chats = new ObservableCollection<Chat>();
            _getChatAPI = new GetChatListAPI();
            BindingContext = this;
            LoadChats();
        }

        private async void LoadChats()
        {
            var chats = await _getChatAPI.GetChatList(UsersStorage.CurrentUser.Phone);
            if (chats != null)
            {
                foreach (var chat in chats)
                {
                    if (chat.phoneUser1 == UsersStorage.CurrentUser.Phone)
                    {
                        chat.phoneUser1 = chat.phoneUser2;
                        chat.phoneUser2 = UsersStorage.CurrentUser.Phone;
                    }
                    Chats.Add(chat);
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось загрузить чаты", "OK");
            }
        }

        [Obsolete]
        private async void OnChatSelected(object sender, SelectionChangedEventArgs e)
        {
            var chat = e.CurrentSelection.FirstOrDefault() as Chat;
            if (chat != null)
            {
                await Navigation.PushAsync(new ChatPage(chat));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
