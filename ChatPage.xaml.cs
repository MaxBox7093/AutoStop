using Microsoft.Maui.Controls;
using AutoStop.APIServices;
using AutoStop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers; // Явно указываем использование System.Timers

namespace AutoStop
{
    public partial class ChatPage : ContentPage
    {
        GetMessagesAPI _getMessAPI;
        SendMessageAPI _sendMessAPI;
        Chat chtmp;
        System.Timers.Timer _timer; // Явно указываем использование System.Timers.Timer

        [Obsolete]
        public ChatPage(Chat chat)
        {
            InitializeComponent();
            chtmp = chat;
            _getMessAPI = new GetMessagesAPI();
            _sendMessAPI = new SendMessageAPI();

            // Запуск таймера для обновления сообщений
            _timer = new System.Timers.Timer(20000); // каждые 20 секунд
            _timer.Elapsed += async (sender, e) => await UpdateMessages();
            _timer.Start();

            // Загрузка сообщений при инициализации страницы
            Task.Run(async () => await LoadMessages());
        }

        [Obsolete]
        private async Task LoadMessages()
        {
            var messages = await _getMessAPI.GetMessages(chtmp.idChat.Value);
            Device.BeginInvokeOnMainThread(() =>
            {
                ChatMessagesStackLayout.Children.Clear();
                foreach (var message in messages)
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

        private void AddMessageToUI(Message message)
        {
            var frame = new Frame
            {
                CornerRadius = 10,
                Padding = 10,
                Margin = new Thickness(5),
                BackgroundColor = message.senderPhone == chtmp.phoneUser1 ? Colors.White : Colors.AntiqueWhite,
                HorizontalOptions = message.senderPhone == chtmp.phoneUser1 ? LayoutOptions.Start : LayoutOptions.End,
                Content = new Label
                {
                    Text = message.content,
                    FontSize = 14
                }
            };

            ChatMessagesStackLayout.Children.Add(frame);
        }
    }
}
