using System;
using System.Net.Sockets;
using System.Windows;

namespace task_List
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const int PORT = 8080;
        const string ADDRESS = "127.0.0.1";
        static private TcpClient client;
        static private NetworkStream stream;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
        
        public static TcpClient getClient()
        {
            if (client == null) {
                try
                {
                    return client = new TcpClient(ADDRESS, PORT);
                }
                catch (Exception ex) {
                    MessageBox.Show("Нажаль, сервер наразі не доступний. Спробуйте ще раз. \n\n\n\n\n" + ex);
                    return client;
                }
            }
            return client;
        }

        public static void closeClient()
        {
            if (client != null)
                client = null;
        }

        public static NetworkStream getStream()
        {
            try
            {
                return stream = client == null ? getClient().GetStream() : client.GetStream();
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Нажаль, сервер наразі не доступний. Спробуйте ще раз. \n\n\n\n\n" + ex);
                return stream;
            }
        }

        public static void closeStream()
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
    }
}
