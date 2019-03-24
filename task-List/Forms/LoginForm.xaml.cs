using System;
using System.Net.Sockets;
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
    }
}
