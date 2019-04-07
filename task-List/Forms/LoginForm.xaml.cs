using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;

namespace task_List.Forms
{
    public partial class LoginForm : Window
    {
        private NetworkStream stream;
        TcpClient client;

        bool isLoginСorrect = false;
        bool isPasswordСorrect = false;

        AuthForm authForm ;

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
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginErrorLabel.Visibility = !isLoginСorrect ? Visibility.Visible : Visibility.Hidden;
            passwordErrorLabel.Visibility = !isPasswordСorrect ? Visibility.Visible : Visibility.Hidden;

            if (loginTextBox.Text != "" && passwordTextBox.Text != "" && isLoginСorrect && isPasswordСorrect)
            {
                try
                {
                    string str = "1" + loginTextBox.Text + " " + passwordTextBox.Text;
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", str);

                    data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    if (responseData == "true")
                    {
                        stream.Close();
                        client.Close();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something is wrong. \n\n\n" + ex.ToString());
                }
            }
        }

        private void Login_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (loginTextBox.Text == "")
                login_placeholder.Visibility = Visibility.Visible;
            else login_placeholder.Visibility = Visibility.Hidden;

            CheckLogin();

        }
        private void Password_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
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


        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            authForm = new AuthForm();
            authForm.ShowDialog();
        }

        public void CheckLogin()
        {
            string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
            string log = loginTextBox.Text;

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
    }
}