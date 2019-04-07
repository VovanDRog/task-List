using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace task_List.Forms
{
    /// <summary>
    /// Interaction logic for AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        bool isLoginСorrect = false;
        bool isPasswordСorrect = false;

        public AuthForm()
        {
            InitializeComponent();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (Login.Text == "") login_placeholder.Visibility = Visibility.Visible; else login_placeholder.Visibility = Visibility.Hidden;

            string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
            string log = Login.Text;

            if (!Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                loginErrorLabel.Content = "Login is incorrect";
                loginErrorLabel.Visibility = Visibility.Visible;
                isLoginСorrect = false;
            }
            else
            {
                loginErrorLabel.Visibility = Visibility.Hidden;
                isLoginСorrect = true;
            }
        }
      
        private void Password1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Password1.Text.Length < 4 || Password1.Text.Length > 10)
            {
                PasswordErrorLabel.Content = "Incorrect password length";
                PasswordErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordErrorLabel.Visibility = Visibility.Hidden;
            }

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if(Name.Text.Length < 4 || Name.Text.Length > 10)
            {
                NameErrorLabel.Content = "Incorrect name length";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NameErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        private void Password2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Password1.Text != Password2.Text)
            {
                Password2ErrorLabel.Content = "Passwords do not match";
                Password2ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                Password2ErrorLabel.Visibility = Visibility.Hidden;
            }

        } 

        private void RegisterButtonSubmitClick(object sender, RoutedEventArgs e)
        {
            if (Password2.Text != "" && Password1.Text != "" && Name.Text != "" && Login.Text != "")
            {
                try
                {
                    TcpClient tcpClient = new TcpClient("127.0.0.1", 8888);
                    NetworkStream stream = tcpClient.GetStream();
                    string str = "1" + Login.Text + " " + Name.Text + " " + Password1.Text;
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", str);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
    }
}
