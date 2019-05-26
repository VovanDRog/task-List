using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;
using System.Net.Sockets;

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
            return client = new TcpClient(ADDRESS, PORT); ;
        }

        public static void closeClient()
        {
            if (client != null)
                client = null;
        }

        public static NetworkStream getStream()
        {
            return stream = client == null ? getClient().GetStream() : client.GetStream();
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
