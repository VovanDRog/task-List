using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;

namespace task_List.Forms
{
    public partial class LoginForm : Window
    {
        const int PORT = 1556;
        const string ADDRESS = "127.0.0.1";
        TcpClient client = null;
        static NetworkStream stream = null;

        bool isLoginСorrect = false;
        bool isPasswordСorrect = false;

        AuthForm authForm;

        public LoginForm()
        {
            InitializeComponent();

            loginErrorLabel.Visibility = passwordErrorLabel.Visibility = Visibility.Hidden;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginErrorLabel.Visibility = !isLoginСorrect ? Visibility.Visible : Visibility.Hidden;
            passwordErrorLabel.Visibility = !isPasswordСorrect ? Visibility.Visible : Visibility.Hidden;

            client = new TcpClient(ADDRESS, PORT);
            stream = client.GetStream();

            if (loginTextBox.Text != "" && passwordTextBox.Text != "" && isLoginСorrect && isPasswordСorrect)
            {
                try
                {
                    string[] data = new string [] { loginTextBox.Text, passwordTextBox.Text };
                    inputFromAServer(1, data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something is wrong. \n\n\n" + ex.ToString());
                }
            }
        }

        private void Login_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string pattern = @"^[A-Za-z0-9_-]{5,16}$";
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
        private void Password_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // string pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
            string pattern = @"^[A-Za-z0-9]{5,16}$";

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

        private bool inputFromAServer(int a, string [] data)
        {
            try
            {
                byte[] msg = new byte[1024];
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);

                writer.Write(a);
                writer.Flush();

                foreach ( string value in data)
                {
                    writer.Write(value);
                    writer.Flush();
                }

                client.Close();
                writer.Close();
                reader.Close();

                //return ( Convert.ToBoolean( reader.ReadByte()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oh. We have a error \n\n" + ex);
            }
            return false;
        }
    }
}