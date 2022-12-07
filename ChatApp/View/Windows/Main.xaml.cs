using ChatApp.View.Windows;
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

namespace ChatApp
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public List<ChatRooms> chatRooms = new List<ChatRooms>();
        public List<ChatRoomEmployee> chatRoomEmployees = new List<ChatRoomEmployee>();
        public static ChatRooms chatRoom;
        public Main()
        {
            InitializeComponent();
            HelloGrid.DataContext = MainWindow.employee;
            GridLoad();
        }

        public async void GridLoad()
        {
            HttpResponseMessage chatRooms = await MainWindow.httpClient.GetAsync("http://localhost:58053/api/ChatRooms");
            var roomscontent = await chatRooms.Content.ReadAsStringAsync();
            HttpResponseMessage employee = await MainWindow.httpClient.GetAsync("http://localhost:58053/api/ChatroomEmployees");
            var empcontent = await employee.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ChatRoomEmployee>>(empcontent)
                .Where(i => i.IdUser == MainWindow.employee.Id).ToList();

            if(result == null)
            {
                MessageBox.Show("У вас нет ни одного чата");
            }
            else
            {
                var rooms = JsonConvert.DeserializeObject<List<ChatRooms>>(roomscontent);
                ChatList.ItemsSource = from r in rooms
                                       join res in result on r.Id equals res.IdChatroom
                                       select r;
                //ChatList.ItemsSource = ;
            }
        }

        //private void BackBt_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow w = new MainWindow();
        //    w.Show();
        //    this.Close();
        //}

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            chatRoom = ChatList.SelectedItem as ChatRooms;
            ChatMessage message = new ChatMessage();
            message.Show();
            Close();
        }
    }
}
