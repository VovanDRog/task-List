using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;

namespace task_List.Forms
{
    public partial class LoginForm : Window
    {

        bool isLoginСorrect = false;
        bool isPasswordСorrect = false;

        AuthForm authForm;

        public LoginForm()
        {
            InitializeComponent();

            App.Current.Properties["id"] = null;
            App.Current.Properties["login"] = string.Empty;
            App.Current.Properties["password"] = string.Empty;

            loginErrorLabel.Visibility = passwordErrorLabel.Visibility = Visibility.Hidden;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginErrorLabel.Visibility = !isLoginСorrect ? Visibility.Visible : Visibility.Hidden;
            passwordErrorLabel.Visibility = !isPasswordСorrect ? Visibility.Visible : Visibility.Hidden;

           
            if (loginTextBox.Text != "" && passwordTextBox.Text != "" && isLoginСorrect && isPasswordСorrect)
            {

                BinaryWriter writer = new BinaryWriter(App.getStream());
                BinaryReader reader = new BinaryReader(App.getStream());

                try
                {
                    string login = loginTextBox.Text;
                    string password = passwordTextBox.Text;


                    writer.Write(1);
                    writer.Flush();
                    writer.Write(login);
                    writer.Flush();
                    writer.Write(password);
                    writer.Flush();


                    var res = reader.Read();
                    if(res == 1)
                    {
                        App.Current.Properties["userID"] = login;
                        App.Current.Properties["userLogin"] = login;
                        App.Current.Properties["userPassword"] = password;

                        var form = new MainWindow();
                        this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неправельний логін або пароль");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something is wrong. \n\n\n" + ex.ToString());
                }
                finally
                {
                    writer.Close();
                    reader.Close();
                    App.closeClient();
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

        private int sendToServer(int a, string [] data)
        {
            int res = -1;
            try
            {

                var stream = (NetworkStream) App.Current.Properties["stream"];
                var client = (TcpClient) App.Current.Properties["client"];

                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);

                writer.Write(a);
                writer.Flush();

                foreach ( string value in data)
                {
                    writer.Write(value);
                    writer.Flush();
                }

                res = reader.Read();

                client.Close();
                writer.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Oh. We have a error \n\n" + ex);
            }

            if (res != -1)
                return res;
            else
                return -1;
        }
    }
}