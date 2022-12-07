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
        public static HttpClient httpClient = new HttpClient();
        public static Employee employee;
        public MainWindow()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Login) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.Password))
            {
                Enter();
            }
        }

        private async void Enter()
        {
            LoginTb.Text = Properties.Settings.Default.Login;
            PasswordTb.Text = Properties.Settings.Default.Password;
        }
        private async void SignIn(object sender, RoutedEventArgs e)
        {
            httpClient.DefaultRequestHeaders.Accept.Add
                (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var content = new userData { Password = PasswordTb.Text, Login = LoginTb.Text };
            HttpContent httpContent = 
                new StringContent(JsonConvert.SerializeObject(content), 
                Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:58053/api/Users", httpContent);

            if(message.IsSuccessStatusCode)
            {
                var curContent = await message.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(curContent);

                if ((bool)Remember.IsChecked)
                {
                    Properties.Settings.Default.Login = LoginTb.Text;
                    Properties.Settings.Default.Password = PasswordTb.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Login = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                    Properties.Settings.Default.Save();
                }
                Main w = new Main();
                w.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("не верный логин или пароль");
            }
        }

        public class userData
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

       
    }
}
