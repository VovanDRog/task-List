using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace task_List.Forms
{
    public partial class AddForm : Window
    {
        const int PORT = 1556;
        const string ADDRESS = "127.0.0.1";
        TcpClient client = null;
        static NetworkStream stream = null;

        public AddForm()
        {
            InitializeComponent();
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

        private void Task_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Task.Text.Length < 4 || Task.Text.Length > 20)
            {
                TaskErrorLabel.Content = "Incorrect task length";
                TaskErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                TaskErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Description.Text == "")
            {
                DescriptionErrorLabel.Content = "Description is empty";
                DescriptionErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                DescriptionErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        private void AddButtonSubmitClick(object sender, RoutedEventArgs e)
        {

            client = new TcpClient(ADDRESS, PORT);
            stream = client.GetStream();


            if (Name.Text.Length < 4 || Name.Text.Length > 20 && Task.Text.Length < 4 || Task.Text.Length > 20 && Description.Text != "")
            {
                try
                {
                    string[] data = new string[] { Name.Text, Task.Text, Description.Text };
                    inputFromAServer(4, data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something is wrong. \n\n\n" + ex.ToString());
                }
            }


            if (Name.Text.Length < 4 || Name.Text.Length > 20)
            {
                NameErrorLabel.Content = "Incorrect name length";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NameErrorLabel.Visibility = Visibility.Hidden;
            }

            if (Task.Text.Length < 4 || Task.Text.Length > 20)
            {
                TaskErrorLabel.Content = "Incorrect task length";
                TaskErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                TaskErrorLabel.Visibility = Visibility.Hidden;
            }

            if (Description.Text == "")
            {
                DescriptionErrorLabel.Content = "Description is empty";
                DescriptionErrorLabel.Visibility = Visibility.Visible;
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
