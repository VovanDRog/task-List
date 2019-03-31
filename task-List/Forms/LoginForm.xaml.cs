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
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" && Password.Text != "")
            {
                using (TcpClient tcpClient = new TcpClient("127.0.0.1", 8888))
                {
                    NetworkStream stream = tcpClient.GetStream();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(Login.Text);

                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", Login.Text);
                }
            }
        }


        private void Login_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string pattern = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
            string log = Login.Text;

            if (Regex.IsMatch(log, pattern, RegexOptions.IgnoreCase))
            {
                test.Content = "gud";
            }
            else
            {
                test.Content = "bead";
            }


        }
        private void Password_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {


            //string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$";
            string pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";


            string pass = Password.Text;

            if (Regex.IsMatch(pass, pattern, RegexOptions.IgnoreCase))
            {
                test.Content = "+";
            }
            else
            {
                test.Content = "-";
            }

        }

        private void Login_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Login.Text == String.Empty)
            {
                log.Content = "";
            }

        }

        private void Login_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Login.Text == String.Empty)
            {
                log.Content = "Login";
            }

        }

        private void Password_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Password.Text == String.Empty)
            {
                pass.Content = "";
            }

        }

        private void Password_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Password.Text == String.Empty)
            {
                pass.Content = "Password";
            }
        }
    }
}
