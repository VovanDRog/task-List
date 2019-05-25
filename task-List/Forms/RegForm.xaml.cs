using System;
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

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

            /*string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
            string log = Login.Text;

            if (!Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                loginErrorLabel.Content = "Login is incorrect";
                loginErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                loginErrorLabel.Visibility = Visibility.Hidden;
            }*/
        }     
      
        private void Password1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pattern = @"^[A-Za-z0-9]{5,16}$";
            string log = Password1.Text;

            if (!Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                PasswordErrorLabel.Content = "Incorrect password";
                PasswordErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordErrorLabel.Visibility = Visibility.Hidden;
            }



            /*
            if (Password1.Text.Length < 4 || Password1.Text.Length > 10)
            {
                PasswordErrorLabel.Content = "Incorrect password length";
                PasswordErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordErrorLabel.Visibility = Visibility.Hidden;
            }*/

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
            if (Password1.Text.Length < 4 || Password1.Text.Length > 10 && Password1.Text == Password2.Text && Name.Text.Length < 4 || Name.Text.Length > 16)
            {
                try
                {
                    TcpClient tcpClient = new TcpClient("127.0.0.1", 8888);
                    NetworkStream stream = tcpClient.GetStream();
                    string str = "1" + Name.Text + " " + Password1.Text;
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", str);

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    if (responseData == "true")
                    {
                        stream.Close();
                        
                        this.Close();
                    } 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (Name.Text == "")
            {
                NameErrorLabel.Content = "Name is empty";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
           
            if(Password1.Text == "")
            {
                PasswordErrorLabel.Content = "Password is empty";
                PasswordErrorLabel.Visibility = Visibility.Visible;
            }

            if (Password2.Text == "")
            {
                Password2ErrorLabel.Content = "Password is empty";
                Password2ErrorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
