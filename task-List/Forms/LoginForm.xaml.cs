using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;

namespace task_List.Forms
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private NetworkStream stream;
        bool isLoginСorrect = false;
        bool isPasswordСorrect = false;

        public LoginForm()
        {
            InitializeComponent();

            loginErrorLabel.Visibility = passwordErrorLabel.Visibility = Visibility.Hidden;
            // tryConnectToServer();
        }

        private void tryConnectToServer()
        {
            try
            {
                TcpClient tcpClient = new TcpClient("127.0.0.1", 8888);
                stream = tcpClient.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text != "" && passwordTextBox.Text != "" && isLoginСorrect && isPasswordСorrect)
            {
                //MessageBox.Show("Submit");
                try
                {
                    string str = "1" + loginTextBox.Text + " " + passwordTextBox.Text;
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", str);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something is wrong. \n" + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Not submit");
                loginErrorLabel.Visibility = passwordErrorLabel.Visibility = Visibility.Visible;
            }
        }

        private void Login_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (loginTextBox.Text == "") login_placeholder.Visibility = Visibility.Visible; else login_placeholder.Visibility = Visibility.Hidden;

            string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
            string log = loginTextBox.Text;

            if (!Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                loginErrorLabel.Content = "bad";
                loginErrorLabel.Visibility = Visibility.Visible;
                isLoginСorrect = false;
            }
            else
            {
                loginErrorLabel.Visibility = Visibility.Hidden;
                isLoginСorrect = true;
            }
        }
        private void Password_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$";
            string pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

            string pass = passwordTextBox.Text;

            if (!Regex.IsMatch(pass, pattern, RegexOptions.IgnoreCase))
            {
                passwordErrorLabel.Content = "password is incorrect";
                passwordErrorLabel.Visibility = Visibility.Visible;
                isPasswordСorrect = false;
            }
            else
            {
                passwordErrorLabel.Visibility = Visibility.Hidden;
                isPasswordСorrect = true;
            }
        }
    }
}
