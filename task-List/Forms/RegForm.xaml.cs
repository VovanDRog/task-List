using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace task_List.Forms
{
    public partial class AuthForm : Window
    {

        public AuthForm()
        {
            InitializeComponent();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

            string pattern = @"^[A-Za-z0-9_-]{5,16}$";
            string log = Name.Text;

            if (!Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                NameErrorLabel.Content = "Incorrect name";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NameErrorLabel.Visibility = Visibility.Hidden;
            }

/*
            if (Name.Text.Length < 4 || Name.Text.Length > 10)
            {
                NameErrorLabel.Content = "Incorrect name length";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NameErrorLabel.Visibility = Visibility.Hidden;
            }*/
        }

        private void Password2_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        } 

        private void RegisterButtonSubmitClick(object sender, RoutedEventArgs e)
        {
            if (Password1.Password.Length > 4 && Password1.Password.Length < 10 && Password1.Password == Password2.Password && Name.Text.Length > 4 && Name.Text.Length < 16)
            {
                BinaryWriter writer = new BinaryWriter(App.getStream());
                BinaryReader reader = new BinaryReader(App.getStream());

                try
                {
                    string login = Name.Text;
                    string password = Password1.Password.ToString();
                    writer.Write(2);
                    writer.Flush();
                    writer.Write(login);
                    writer.Flush();
                    writer.Write(password);
                    writer.Flush();


                    var res = reader.ReadInt32();
                    switch (res)
                    {
                        case 1:
                            MessageBox.Show("Ви успішно зареєструвалися");
                            Name.Clear();
                            Password1.Clear();
                            Password2.Clear();
                            break;
                        case 2:
                            MessageBox.Show("Користувач з таким логіном вже існує");
                            break;
                        case 0:
                            MessageBox.Show("Сталася помилка на сервері");
                            break;
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

            if (Name.Text == "")
            {
                NameErrorLabel.Content = "Name is empty";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
           
            if(Password1.Password.ToString() == "")
            {
                PasswordErrorLabel.Content = "Password is empty";
                PasswordErrorLabel.Visibility = Visibility.Visible;
            }

            if (Password2.Password.ToString() == "")
            {
                Password2ErrorLabel.Content = "Password is empty";
                Password2ErrorLabel.Visibility = Visibility.Visible;
            }
        }

        private void LoginButtonSubmitClick(object sender, RoutedEventArgs e)
        {
            var form = new LoginForm();
            this.Close();
            form.Show();
        }

        private void Password2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password1.Password != Password2.Password)
            {
                Password2ErrorLabel.Content = "Passwords do not match";
                Password2ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                Password2ErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        private void Password1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[A-Za-z0-9]{5,16}$";
            string log = Password1.Password;

            if (!Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                PasswordErrorLabel.Content = "Incorrect password";
                PasswordErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordErrorLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
