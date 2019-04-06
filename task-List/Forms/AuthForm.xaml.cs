using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
      
        private void Password1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password2_TextChanged(object sender, TextChangedEventArgs e)
        {

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
