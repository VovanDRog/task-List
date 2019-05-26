using System;
using System.Collections.Generic;
using System.Linq;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
using Newtonsoft.Json;

namespace task_List.Forms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "gUXdi6d0iv4C0HBTO66XbMUQdmQ36idwTjlolpqf",
            BasePath = "https://tasksystem-228.firebaseio.com/"
        };

        IFirebaseClient _client;
        string status;
        string owner;
        public class Tasks
        {
            public string id { get; set; }
            public string name { get; set; }
            public string owner { get; set; }
            public string status { get; set; }
        }
        public class currentTask
        {
            public string first { get; set; }
            public string second { get; set; }
            public string third { get; set; }
        }

        async void GetAllTasks(string status, string owner)
        {
            //status;
            _client = new FireSharp.FirebaseClient(config);
            int i = 0;
            bool s = true;
            FirebaseResponse getResponse = null;
            List<Tasks> tasks = new List<Tasks>();

            try
            {
                while (s)
                {
                    try
                    {
                        getResponse = await _client.GetAsync("Tasks/" + i);
                        Tasks result = getResponse.ResultAs<Tasks>();
                        if (result == null) break;
                        tasks.Add(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        s = false;
                    }
                    i++;
                }
            }
            catch (Exception e)
            {

            }
            wrapPanel1.Children.Clear();

            foreach (var OneTask in tasks)
            {


                Border myBorder1 = new Border();
                // Zadniy fon colir
                myBorder1.Background = Brushes.White;
                myBorder1.BorderBrush = Brushes.Black;
                //розмір рамки
                myBorder1.BorderThickness = new Thickness(1);
                //Відступи знизу, справа
                myBorder1.Margin = new Thickness(0, 0, 10, 10);

                Label Name = new Label();
                Name.Content = OneTask.name;
                //Name.FontFamily 

                var bc = new BrushConverter();
                Name.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Name.Foreground = new SolidColorBrush(Colors.White);

                //Name.Foreground = new SolidColorBrush(Colors.);

                Label Owner = new Label();
                Owner.Content = OneTask.owner;
                //var bc = new BrushConverter();
                Owner.Margin = new Thickness(0, 1, 0, 0);
                Owner.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Owner.Foreground = new SolidColorBrush(Colors.White);

                Label Status = new Label();
                Status.Content = OneTask.status;
                Status.Foreground = new SolidColorBrush(Colors.Black);

                //Дозволяє додавати лейбли один під одним
                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Vertical;

                //if (OneTask.status != "Zakincheno")
                if (OneTask.status == status)
                {
                    // TODO :  id, Name, Opys, ToyHtoVykonye, status
                    st.Children.Add(Name);
                    st.Children.Add(Owner);
                    st.Children.Add(Status);

                    if (OneTask.status == status && status == "fuck")
                    {
                        Button bt = new Button();
                        // bt.FontWeight =

                        bt.FontWeight = FontWeights.UltraBold;

                        //bt.FontWeight  =
                        bt.Background = (Brush)bc.ConvertFrom("#FF70B8E8");

                        bt.Foreground = new SolidColorBrush(Colors.White);
                        bt.BorderBrush = new SolidColorBrush(Colors.White);


                        bt.Content = "Добавити до завдань";
                        st.Children.Add(bt);
                    }
                    myBorder1.Child = st;
                    wrapPanel1.Children.Add(myBorder1);
                }

                //myBorder1.Child = st;
                //wrapPanel1.Children.Add(myBorder1);

            }
        }

        private int countTasks { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            hiuser.Content = "Hi, " + App.Current.Properties["userLogin"];
            getCompletedTasks();
            getCountActiveTasks();

            GetAllTasks(status, owner);
        }

        private async void getCompletedTasks()
        {
            _client = new FireSharp.FirebaseClient(config);
            int i = 0;
            countTasks = 0;
            bool s = true;
            FirebaseResponse getResponseRequester = null;
            FirebaseResponse getResponseStatus = null;
            string userName = (string)App.Current.Properties["userLogin"];

            try
            {
                while (s)
                {
                    try
                    {
                        getResponseRequester = await _client.GetAsync("Tasks/" + i + "/requester");
                        if (getResponseRequester == null) break;

                        string requesterName = getResponseRequester.ResultAs<string>();
                        if (requesterName == userName)
                        {
                            getResponseStatus = await _client.GetAsync("Tasks/" + i + "/status");
                            string requesterStatus = getResponseStatus.ResultAs<string>();
                            if (requesterStatus == "To Do")
                                completet.Content = "Completed tasks: " + (countTasks + 1).ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Сталась помилка");
                        Console.WriteLine(e.Message);
                        s = false;
                    }
                    i++;
                }
            }
            catch
            {
                MessageBox.Show(" Сталась помилка");
            }
        }

        private async void getCountActiveTasks()
        {
            _client = new FireSharp.FirebaseClient(config);
            int countActiveTasks = 0;

            FirebaseResponse getAllTasksRequester = null;
            string userID = (string)App.Current.Properties["userID"];

            try
            {
                try
                {
                    getAllTasksRequester = await _client.GetAsync("Users/" + userID + "/Tasks");
                    currentTask requesterStatus = getAllTasksRequester.ResultAs<currentTask>();
                    if (requesterStatus.first != "")
                        countActiveTasks += 1;
                    if (requesterStatus.second != "")
                        countActiveTasks += 1;
                    if (requesterStatus.third != "")
                        countActiveTasks += 1;
                }
                catch (Exception e)
                {
                    MessageBox.Show(" У вас немає активних завдань!!!");
                }
            }
            catch
            {
                MessageBox.Show(" Сталась помилка");
            }
            work.Content = "WorkingTask: " + (countActiveTasks).ToString();
        }
        private void tack_Click(object sender, RoutedEventArgs e)
        {
            //scrollViewer.Content = "";
            var bc = new BrushConverter();
            completed.Foreground = activetack.Foreground = expect.Foreground = main.Foreground = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.BorderBrush = activetack.BorderBrush = expect.BorderBrush = main.BorderBrush = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.Background = activetack.Background = expect.Background = main.Background = new SolidColorBrush(Colors.White);

            scrollViewer.Visibility = Visibility.Visible;
            tack.Foreground = new SolidColorBrush(Colors.White);
            tack.BorderBrush = new SolidColorBrush(Colors.White);
            tack.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
            //scrollViewer.Content = "gfhgnhj,bn";

            hiuser.Visibility = Visibility.Hidden;
            completet.Visibility = Visibility.Hidden;
            work.Visibility = Visibility.Hidden;
            expectt.Visibility = Visibility.Hidden;

            string status = "fuck";
            GetAllTasks(status, owner);
        }

        private void activetack_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            completed.Foreground = tack.Foreground = expect.Foreground = main.Foreground = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.BorderBrush = tack.BorderBrush = expect.BorderBrush = main.BorderBrush = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.Background = tack.Background = expect.Background = main.Background = new SolidColorBrush(Colors.White);

            activetack.Foreground = new SolidColorBrush(Colors.White);
            activetack.BorderBrush = new SolidColorBrush(Colors.White);
            activetack.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
            scrollViewer.Visibility = Visibility.Visible;
            hiuser.Visibility = Visibility.Hidden;
            completet.Visibility = Visibility.Hidden;
            work.Visibility = Visibility.Hidden;
            expectt.Visibility = Visibility.Hidden;
            string status = "To Do";
            string owner = "dick";

            GetAllTasks(status, owner);
        }

        private void expect_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            completed.Foreground = activetack.Foreground = tack.Foreground = main.Foreground = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.BorderBrush = activetack.BorderBrush = tack.BorderBrush = main.BorderBrush = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.Background = activetack.Background = tack.Background = main.Background = new SolidColorBrush(Colors.White);

            expect.Foreground = new SolidColorBrush(Colors.White);
            expect.BorderBrush = new SolidColorBrush(Colors.White);
            expect.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
            scrollViewer.Visibility = Visibility.Visible;
            hiuser.Visibility = Visibility.Hidden;
            completet.Visibility = Visibility.Hidden;
            work.Visibility = Visibility.Hidden;
            expectt.Visibility = Visibility.Hidden;
            string status = "expect";
            string owner = "dick";

            GetAllTasks(status, owner);
        }

        private void completed_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            tack.Foreground = activetack.Foreground = expect.Foreground = main.Foreground = (Brush)bc.ConvertFrom("#FF70B8E8");
            tack.BorderBrush = activetack.BorderBrush = expect.BorderBrush = main.BorderBrush = (Brush)bc.ConvertFrom("#FF70B8E8");
            tack.Background = activetack.Background = expect.Background = main.Background = new SolidColorBrush(Colors.White);

            completed.Foreground = new SolidColorBrush(Colors.White);
            completed.BorderBrush = new SolidColorBrush(Colors.White);
            completed.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
            scrollViewer.Visibility = Visibility.Visible;
            hiuser.Visibility = Visibility.Hidden;
            completet.Visibility = Visibility.Hidden;
            work.Visibility = Visibility.Hidden;
            expectt.Visibility = Visibility.Hidden;
            string status = "closed";
            string owner = "dick";

            GetAllTasks(status, owner);
        }

        private void main_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            completed.Foreground = activetack.Foreground = expect.Foreground = tack.Foreground = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.BorderBrush = activetack.BorderBrush = expect.BorderBrush = tack.BorderBrush = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.Background = activetack.Background = expect.Background = tack.Background = new SolidColorBrush(Colors.White);

            hiuser.Visibility = Visibility.Visible;
            completet.Visibility = Visibility.Visible;
            work.Visibility = Visibility.Visible;
            expectt.Visibility = Visibility.Visible;

            main.Foreground = new SolidColorBrush(Colors.White);
            main.BorderBrush = new SolidColorBrush(Colors.White);
            main.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
            scrollViewer.Visibility = Visibility.Hidden;
        }
    }
}
