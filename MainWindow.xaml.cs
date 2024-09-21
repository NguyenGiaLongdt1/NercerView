using DevExpress.Xpf.Core;
using NercerView.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NercerView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private readonly HttpClient _httpClient;
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient(); 
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string UserName = txtUsername.Text;
            string PassWord = txtPassword.Password;

           
            var LoginRequest = new { UserName = UserName, PassWord = PassWord };
            var JsonRequest = JsonSerializer.Serialize(LoginRequest);
            var Content = new StringContent(JsonRequest, Encoding.UTF8, "application/json");
            string ApiLogin = "http://localhost:5002/api/Auth/login";
            try
            {
                if ( UserName != txtUsername.Text || PassWord != txtPassword.Password)
                {
                    MessageBox.Show("Sai thông tin tài khoải! Yêu cầu nhập lại thông tin");
                    return;
                }
                //TrangChuNew TrangChuNew = new TrangChuNew();
                var response = await _httpClient.PostAsync(ApiLogin, Content);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync();
                    Application.Current.Properties["AuthToken"] = responseData;
                    MessageBox.Show("Login Thành công");
                    this.Close();
                    //TrangChuNew.Show();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }

        private void tbl_Register_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
            this.Close();
           
        }
    }
}
