using Microsoft.Maui.Controls;
using AutoStop.APIServices;
using AutoStop.Storages;
using AutoStop.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoStop
{
    public partial class ChatsPage : ContentPage
    {
        private readonly GetChatListAPI _getChatAPI;
        public List<Chat> Chats { get; set; }

        public ChatsPage()
        {
            InitializeComponent();
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
                await DisplayAlert("������", "�� ������� ��������� ����������", "OK");
            }
        }

        private async void OnChatTapped(object sender, ItemTappedEventArgs e)
        {
            var chat = e.Item as Chat;
            if (chat != null)
            {
                //await Navigation.PushAsync(new ChatPage(chat));
            }
        }
    }
}
