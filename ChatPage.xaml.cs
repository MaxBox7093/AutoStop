using Microsoft.Maui.Controls;
using AutoStop.APIServices;
using AutoStop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AutoStop.Storages; // ���� ��������� ������������� System.Timers

namespace AutoStop
{
    public partial class ChatPage : ContentPage
    {
        GetMessagesAPI _getMessAPI;
        SendMessageAPI _sendMessAPI;
        GetNameByPhoneNumAPI _getNameAPI;
        Chat chtmp;
        System.Timers.Timer _timer; // ���� ��������� ������������� System.Timers.Timer

        [Obsolete]
        public ChatPage(Chat chat)
        {
            InitializeComponent();
            chtmp = chat;
            _getMessAPI = new GetMessagesAPI();
            _sendMessAPI = new SendMessageAPI();
            _getNameAPI = new GetNameByPhoneNumAPI();

            // ������ ������� ��� ���������� ���������
            _timer = new System.Timers.Timer(2000); // ������ 2 �������
            _timer.Elapsed += async (sender, e) => await UpdateMessages();
            _timer.Start();

            AddUsr2Name(chat);

            // �������� ��������� ��� ������������� ��������
            Task.Run(async () => await LoadMessages());
        }

        [Obsolete]
        private async Task LoadMessages()
        {
            var messages = await _getMessAPI.GetMessages(chtmp.idChat.Value);
            Device.BeginInvokeOnMainThread(() =>
            {
                ChatMessagesStackLayout.Children.Clear();
                foreach (var message in messages.OrderBy(m => m.sendDate))
                {
                    AddMessageToUI(message);
                }
            });
        }

        [Obsolete]
        private async Task UpdateMessages()
        {
            await LoadMessages();
        }

        private async void AddUsr2Name(Chat chat)
        {
            NameUsr2.Text = await _getNameAPI.GetName(chat.phoneUser1);
            PhoneUsr2.Text = chat.phoneUser1;
        }

        private async void AddMessageToUI(Message message)
        {
            string senderName = message.senderPhone == chtmp.phoneUser1 ? await _getNameAPI.GetName(chtmp.phoneUser1) : "��";
            string sendDateString = message.sendDate.HasValue ? message.sendDate.Value.ToString("HH:mm") : "";

            var frame = new Frame
            {
                CornerRadius = 10,
                Padding = 10,
                Margin = new Thickness(5),
                BackgroundColor = message.senderPhone == chtmp.phoneUser1 ? Colors.White : Colors.AntiqueWhite,
                HorizontalOptions = message.senderPhone == chtmp.phoneUser1 ? LayoutOptions.Start : LayoutOptions.End,
                WidthRequest = 100,
                MinimumWidthRequest = 80,
                MaximumWidthRequest = 200,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = $"{senderName}",
                            FontSize = 14,
                            TextColor = Colors.Black,
                            FontAttributes = FontAttributes.Bold
                        },
                        new Label
                        {
                            Text = $"({sendDateString})",
                            FontSize = 10,
                            TextColor = Colors.Black,
                            FontAttributes = FontAttributes.None
                        },
                        new Label
                        {
                            Text = message.content,
                            FontSize = 14
                        }
                    }
                }
            };

            ChatMessagesStackLayout.Children.Add(frame);
        }

        [Obsolete]
        private void Send_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(messUI.Text))
            {
                Message ms = new Message
                {
                    refChat = chtmp.idChat,
                    senderPhone = UsersStorage.CurrentUser.Phone,
                    content = messUI.Text
                };
                SendMess(ms);
            }
        }

        [Obsolete]
        private async void SendMess(Message mess)
        {
            bool success = await _sendMessAPI.Send(mess);
            if (!success)
            {
                await DisplayAlert("������", "�� ������� ��������� ���������", "OK");
            }

            await UpdateMessages();
        }
    }
}
