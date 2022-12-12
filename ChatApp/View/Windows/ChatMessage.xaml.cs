using ChatApp.AppData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatApp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChatMessage.xaml
    /// </summary>
    public partial class ChatMessage : Window
    {
        List<ChatMessagePartial> chatMessagePartials = new List<ChatMessagePartial>();
        HttpClient httpClient = new HttpClient(); 
        public ChatMessage()
        {
            InitializeComponent();
            Title = Main.chatRoom.Topic;
            MainWindow.employee.Id.ToString();
            GetMessage();

        }

        private async void GetMessage()
        {
            HttpResponseMessage message = await MainWindow.httpClient.GetAsync("http://localhost:58053/api/ChatMessages");
            var content = await message.Content.ReadAsStringAsync();
            chatMessagePartials = JsonConvert.DeserializeObject<List<ChatMessagePartial>>(content);
            MessageList.ItemsSource = chatMessagePartials.Where(i => i.IdChatRoom == Main.chatRoom.Id);

        }

        private async void SendMessage(object sender, RoutedEventArgs e)
        {
            ChatMessagePartial partial = new ChatMessagePartial(MainWindow.employee.Id, Main.chatRoom.Id, txtSendMessage.Text, System.DateTime.Now);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(partial), Encoding.UTF8, "application/json");
            HttpResponseMessage newmessage = await httpClient.PostAsync("http://localhost:58053/api/ChatMessages", httpContent);

            txtSendMessage.Text = "";
        }

       
    }
}
