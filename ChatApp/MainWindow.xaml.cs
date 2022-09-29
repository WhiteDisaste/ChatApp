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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HttpClient httpClient = new HttpClient();
        public static Employee employee;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SignIn(object sender, RoutedEventArgs e)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var content = new userData { password = PasswordTb.Text, username = LoginTb.Text };
            HttpContent httpContent = 
                new StringContent(JsonConvert.SerializeObject(content), 
                Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:50203/api/Login", httpContent);

            if(message.IsSuccessStatusCode)
            {
                MessageBox.Show("yes!!!");
            }
            else
            {
                MessageBox.Show("не верный логин или пароль");
            }
        }

        public class userData
        {
            public string username { get; set; }
            public string password { get; set; }
        }

       
    }
}
