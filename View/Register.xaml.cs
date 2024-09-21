using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;


namespace NercerView.View
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : ThemedWindow
    {
        private readonly HttpClient _httpClient;
        public Register()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var RegisterData = new
            {
                Name = txtName.Text,
                Username = txtUserName.Text,
                Email = txtEmail.Text,
                Phone = txtSDT.Text,
                Password = txtPassword.Password,
                ConfirmPassword = txtConfirmPassword.Password,
            };
            if (RegisterData.Password.Length < 5)
            {
                MessageBox.Show("PassWord phải lớn hơn 5 ký tự");
                return;
            }
            if ((int)RegisterData.Email.Count(e => e == '@') != 1)
            {
                MessageBox.Show("Email không hợp lệ, phải chứa một ký tự @");
                return;
            }
            if (RegisterData.Password != RegisterData.ConfirmPassword)
            {
                MessageBox.Show("Mật khẩu không trùng khớp! Yêu cầu ConfirmPassword trùng khớp");
                return;
            }

            string ApiUri = "http://localhost:5002/api/Auth/register";

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonSerializer.Serialize(RegisterData);
            Console.WriteLine(json);
            var Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(ApiUri, Content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show(" Đăng ký thành công");
                this.Close();
                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
