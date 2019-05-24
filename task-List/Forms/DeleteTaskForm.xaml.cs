using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace task_List.Forms
{
    public partial class DeleteForm : Window
    {
        const int PORT = 1556;
        const string ADDRESS = "127.0.0.1";
        TcpClient client = null;
        static NetworkStream stream = null;


        public DeleteForm()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            client = new TcpClient(ADDRESS, PORT);
            stream = client.GetStream();

            try {
                if (Name.Text.Length < 4 || Name.Text.Length > 20)
                {
                    NameErrorLabel.Content = "Incorrect name length";
                    NameErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    string[] data = new string[] { Name.Text };
                    inputFromAServer(4, data);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Something is wrong. \n\n\n" + ex.ToString());
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name.Text.Length < 4 || Name.Text.Length > 20)
            {
                NameErrorLabel.Content = "Incorrect name length";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NameErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        private bool inputFromAServer(int a, string[] data)
        {
            try
            {
                byte[] msg = new byte[1024];
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);

                writer.Write(a);
                writer.Flush();

                foreach (string value in data)
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
