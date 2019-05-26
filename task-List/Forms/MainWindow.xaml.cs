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
using System.IO;

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
            public string requester { get; set; }
            public string status { get; set; }
        }
        public class currentTask
        {
            public string first { get; set; }
            public string second { get; set; }
            public string third { get; set; }
        }
        public class oneTask
        {
            public string id { get; set; }
        }

        public int countActiveTasks { get; set; }
        public int countExpectedTasks { get; set; }
        async void GetAllTasks(string status)
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

                if (OneTask.status == status)
                {
                    // TODO :  id, Name, Opys, ToyHtoVykonye, status
                    st.Children.Add(Name);
                    st.Children.Add(Owner);
                    st.Children.Add(Status);

                    //TakeTaskButton
                    Button TakeTaskButton = new Button();
                    TakeTaskButton.FontWeight = FontWeights.UltraBold;
                    TakeTaskButton.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                    TakeTaskButton.Foreground = new SolidColorBrush(Colors.White);
                    TakeTaskButton.BorderBrush = new SolidColorBrush(Colors.White);
                    TakeTaskButton.Content = "Добавити до завдань";
                    TakeTaskButton.Click += new RoutedEventHandler(btnTest_Click);
                    //TakeTaskButton

                    st.Children.Add(TakeTaskButton);

                    myBorder1.Child = st;
                    myBorder1.Tag = OneTask.id;
                }

                wrapPanel1.Children.Add(myBorder1);
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            if (countActiveTasks == 3)
            {
                MessageBox.Show("У вас уже 3 активних завдання, виконайте спочатку їх");
            }
            else
            {
                BinaryWriter writer = new BinaryWriter(App.getStream());
                BinaryReader reader = new BinaryReader(App.getStream());

                try
                {
                    string tag = (string)((Border)((StackPanel)((Button)sender).Parent).Parent).Tag;


                    writer.Write(6);
                    writer.Flush();
                    writer.Write((string)App.Current.Properties["userID"]);
                    writer.Flush();
                    writer.Write(tag);
                    writer.Flush();

                    int resultFromServer = (int)reader.Read();
                    if (resultFromServer == 1)
                    {
                        MessageBox.Show("Завдання додано до активних");
                        GetAllTasks("To Do");
                    }
                    else
                    {
                        MessageBox.Show("помилка на сервері");
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
        }

        private int countTasks { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            hiuser.Content = "Hi, " + App.Current.Properties["userLogin"];
            getCompletedTasks();
            getCountActiveTasks();
            getExpectedTasks();

            GetAllTasks(status);
        }

        private async void getCompletedTasks()
        {
            _client = new FireSharp.FirebaseClient(config);
            int i = 0;
            countTasks = 0;
            FirebaseResponse getResponse = null;
            string userName = (string)App.Current.Properties["userLogin"];

            try
            {
                while (true)
                {
                    getResponse = await _client.GetAsync("Users/" + userName + "/closedTasks/" + i);
                    if (getResponse.Body == "null") break;
                    countTasks++;
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            completet.Content = "Completed: " + (countTasks++).ToString();
        }

        private async void getExpectedTasks()
        {
            _client = new FireSharp.FirebaseClient(config);
            countExpectedTasks = 0;

            FirebaseResponse getAllTasksRequester = null;
            FirebaseResponse getActiveTask = null;
            string userID = (string)App.Current.Properties["userID"];
            List<string> AllUserTasksID = new List<string>();

            try
            {
                getAllTasksRequester = await _client.GetAsync("Users/" + userID + "/userTasks");
                currentTask requesterStatus = getAllTasksRequester.ResultAs<currentTask>();

                if (requesterStatus.first != "")
                    AllUserTasksID.Add(requesterStatus.first);
                if (requesterStatus.second != "")
                    AllUserTasksID.Add(requesterStatus.second);
                if (requesterStatus.third != "")
                    AllUserTasksID.Add(requesterStatus.third);
            }
            catch (Exception e)
            {

            }

            foreach (var TaskID in AllUserTasksID)
            {
                getActiveTask = await _client.GetAsync("Tasks/" + TaskID + "/status");
                string taskStatus = getActiveTask.ResultAs<string>();
                if (taskStatus == "Resolved")
                {
                    countExpectedTasks++;
                }
            }

            expectt.Content = "Expect: " + (countExpectedTasks).ToString();
        }

        private async void getCountActiveTasks()
        {
            _client = new FireSharp.FirebaseClient(config);
            countActiveTasks = 0;

            FirebaseResponse getAllTasksRequester = null;
            FirebaseResponse getActiveTask = null;
            string userID = (string)App.Current.Properties["userID"];
            List<string> AllUserTasksID = new List<string>();

            try
            {
                getAllTasksRequester = await _client.GetAsync("Users/" + userID + "/userTasks");
                currentTask requesterStatus = getAllTasksRequester.ResultAs<currentTask>();

                if (requesterStatus.first != "")
                    AllUserTasksID.Add(requesterStatus.first);
                if (requesterStatus.second != "")
                    AllUserTasksID.Add(requesterStatus.second);
                if (requesterStatus.third != "")
                    AllUserTasksID.Add(requesterStatus.third);
            }
            catch (Exception e)
            {

            }

            foreach(var TaskID in AllUserTasksID)
            {
                getActiveTask = await _client.GetAsync("Tasks/" + TaskID + "/status");
                string taskStatus = getActiveTask.ResultAs<string>();
                if(taskStatus == "Active")
                {
                    countActiveTasks++;
                }
            }

            work.Content = "Active: " + (countActiveTasks).ToString();
        }
        private void tack_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            completed.Foreground = activetack.Foreground = expect.Foreground = main.Foreground = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.BorderBrush = activetack.BorderBrush = expect.BorderBrush = main.BorderBrush = (Brush)bc.ConvertFrom("#FF70B8E8");
            completed.Background = activetack.Background = expect.Background = main.Background = new SolidColorBrush(Colors.White);


            scrollViewer.Visibility = Visibility.Visible;
            tack.Foreground = new SolidColorBrush(Colors.White);
            tack.BorderBrush = new SolidColorBrush(Colors.White);
            tack.Background = (Brush)bc.ConvertFrom("#FF70B8E8");


            hiuser.Visibility = Visibility.Hidden;
            completet.Visibility = Visibility.Hidden;
            work.Visibility = Visibility.Hidden;
            expectt.Visibility = Visibility.Hidden;

            string status = "To Do";
            GetAllTasks(status);
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

            string requester = (string)App.Current.Properties["userID"];

            GetAllActiveUsersTasks(requester);
        }

        async void GetAllActiveUsersTasks(string requester)
        {
            _client = new FireSharp.FirebaseClient(config);

            FirebaseResponse getResponse = null;
            List<Tasks> tasks = new List<Tasks>();

            List<Tasks> AllUserTasks = new List<Tasks>();

            try
            {
                getResponse = await _client.GetAsync("Users/" + requester + "/userTasks");
                var result = getResponse.ResultAs<currentTask>();

                if (result.first != "")
                {
                    getResponse = await _client.GetAsync("Tasks/" + result.first);
                    var firstTask = getResponse.ResultAs<Tasks>();
                    if (firstTask.status == "Active")
                        AllUserTasks.Add(firstTask);
                }
                if (result.second != "")
                {
                    getResponse = await _client.GetAsync("Tasks/" + result.second);
                    var secondTask = getResponse.ResultAs<Tasks>();
                    if (secondTask.status == "Active")
                        AllUserTasks.Add(secondTask);
                }
                if (result.third != "")
                {
                    getResponse = await _client.GetAsync("Tasks/" + result.third);
                    var thirdTask = getResponse.ResultAs<Tasks>();
                    if (thirdTask.status == "Active")
                        AllUserTasks.Add(thirdTask);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            wrapPanel1.Children.Clear();

            foreach (var OneTask in AllUserTasks)
            {
                Border myBorder1 = new Border();
                myBorder1.Background = Brushes.White;
                myBorder1.BorderBrush = Brushes.Black;
                myBorder1.BorderThickness = new Thickness(1);
                myBorder1.Margin = new Thickness(0, 0, 10, 10);

                Label Name = new Label();
                Name.Content = OneTask.name;

                var bc = new BrushConverter();
                Name.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Name.Foreground = new SolidColorBrush(Colors.White);

                Label Owner = new Label();
                Owner.Content = OneTask.owner;
                Owner.Margin = new Thickness(0, 1, 0, 0);
                Owner.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Owner.Foreground = new SolidColorBrush(Colors.White);

                Label Status = new Label();
                Status.Content = OneTask.status;
                Status.Foreground = new SolidColorBrush(Colors.Black);

                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Vertical;

                st.Children.Add(Name);
                st.Children.Add(Owner);
                st.Children.Add(Status);

                if (OneTask.status != "Resolved")
                {
                    //Button Resolved
                    Button resolvedButton = new Button();
                    resolvedButton.FontWeight = FontWeights.UltraBold;
                    resolvedButton.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                    resolvedButton.Foreground = new SolidColorBrush(Colors.White);
                    resolvedButton.BorderBrush = new SolidColorBrush(Colors.White);
                    resolvedButton.Content = "До завершених";
                    resolvedButton.Click += new RoutedEventHandler(ChangeStatusTaskToResolved);
                    //Button Resolved

                    //Button Delete
                    Button deleteButton = new Button();
                    deleteButton.FontWeight = FontWeights.UltraBold;
                    deleteButton.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                    deleteButton.Foreground = new SolidColorBrush(Colors.White);
                    deleteButton.BorderBrush = new SolidColorBrush(Colors.White);
                    deleteButton.Content = "Відмовитися";
                    deleteButton.Click += new RoutedEventHandler(DeleteCurrentTaskFromUser);
                    //Button Delete



                    st.Children.Add(resolvedButton);
                    st.Children.Add(deleteButton);
                }
                if (OneTask.status == "Resolved")
                {
                    AccessText mess = new AccessText();
                    mess.Text = "Waiting for access";
                    mess.TextWrapping = TextWrapping.Wrap;
                    mess.Height = double.NaN;
                    mess.Foreground = new SolidColorBrush(Colors.ForestGreen);

                    st.Children.Add(mess);
                }

                myBorder1.Child = st;
                myBorder1.Tag = OneTask.id;
                wrapPanel1.Children.Add(myBorder1);
            }
        }

        private void DeleteCurrentTaskFromUser(object sender, RoutedEventArgs e)
        {
            BinaryWriter writer = new BinaryWriter(App.getStream());
            BinaryReader reader = new BinaryReader(App.getStream());

            try
            {
                string tag = (string)((Border)((StackPanel)((Button)sender).Parent).Parent).Tag;
                string requester = (string)App.Current.Properties["userID"];

                writer.Write(8);
                writer.Flush();
                writer.Write(requester);
                writer.Flush();
                writer.Write(tag);
                writer.Flush();

                int resultFromServer = (int)reader.Read();
                if (resultFromServer == 1)
                {
                    MessageBox.Show("Завдання видалено");
                    GetAllActiveUsersTasks((string)App.Current.Properties["userID"]);
                }
                else
                {
                    MessageBox.Show("помилка на сервері");
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
        private void ChangeStatusTaskToResolved(object sender, RoutedEventArgs e)
        {
            BinaryWriter writer = new BinaryWriter(App.getStream());
            BinaryReader reader = new BinaryReader(App.getStream());

            try
            {
                string tag = (string)((Border)((StackPanel)((Button)sender).Parent).Parent).Tag;


                writer.Write(7);
                writer.Flush();
                writer.Write(tag);
                writer.Flush();

                int resultFromServer = (int)reader.Read();
                if (resultFromServer == 1)
                {
                    MessageBox.Show("Завдання завершено");
                    GetAllActiveUsersTasks((string)App.Current.Properties["userID"]);
                }
                else
                {
                    MessageBox.Show("помилка на сервері");
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

            string requester = (string)App.Current.Properties["userID"];

            GetResolvedTasks(requester);
        }

        async void GetResolvedTasks(string requester)
        {
            _client = new FireSharp.FirebaseClient(config);

            FirebaseResponse getResponse = null;

            List<Tasks> AllResolvedUserTasks = new List<Tasks>();

            try
            {
                getResponse = await _client.GetAsync("Users/" + requester + "/userTasks");
                var result = getResponse.ResultAs<currentTask>();

                if (result.first != "")
                {
                    getResponse = await _client.GetAsync("Tasks/" + result.first);
                    var firstTask = getResponse.ResultAs<Tasks>();
                    if (firstTask.status == "Resolved")
                    {
                        AllResolvedUserTasks.Add(firstTask);
                    }
                }
                if (result.second != "")
                {
                    getResponse = await _client.GetAsync("Tasks/" + result.second);
                    var secondTask = getResponse.ResultAs<Tasks>();
                    if (secondTask.status == "Resolved")
                    {
                        AllResolvedUserTasks.Add(secondTask);
                    }
                }
                if (result.third != "")
                {
                    getResponse = await _client.GetAsync("Tasks/" + result.third);
                    var thirdTask = getResponse.ResultAs<Tasks>();
                    if (thirdTask.status == "Resolved")
                    {
                        AllResolvedUserTasks.Add(thirdTask);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            wrapPanel1.Children.Clear();

            foreach (var OneTask in AllResolvedUserTasks)
            {
                Border myBorder1 = new Border();
                myBorder1.Background = Brushes.White;
                myBorder1.BorderBrush = Brushes.Black;
                myBorder1.BorderThickness = new Thickness(1);
                myBorder1.Margin = new Thickness(0, 0, 10, 10);

                Label Name = new Label();
                Name.Content = OneTask.name;

                var bc = new BrushConverter();
                Name.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Name.Foreground = new SolidColorBrush(Colors.White);

                Label Owner = new Label();
                Owner.Content = OneTask.owner;
                Owner.Margin = new Thickness(0, 1, 0, 0);
                Owner.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Owner.Foreground = new SolidColorBrush(Colors.White);

                Label Status = new Label();
                Status.Content = OneTask.status;
                Status.Foreground = new SolidColorBrush(Colors.Black);

                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Vertical;

                st.Children.Add(Name);
                st.Children.Add(Owner);
                st.Children.Add(Status);


                AccessText mess = new AccessText();
                mess.Text = "Waiting for access";
                mess.TextWrapping = TextWrapping.Wrap;
                mess.Height = double.NaN;
                mess.Foreground = new SolidColorBrush(Colors.ForestGreen);

                st.Children.Add(mess);

                myBorder1.Child = st;
                myBorder1.Tag = OneTask.id;
                wrapPanel1.Children.Add(myBorder1);
            }
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

            string requester = (string)App.Current.Properties["userID"];

            GetAllClosedTasks(requester);
        }

        async void GetAllClosedTasks(string requester)
        {
            _client = new FireSharp.FirebaseClient(config);

            FirebaseResponse getResponse = null;
            int i = 0;
            List<oneTask> AllResolvedUserTasksID = new List<oneTask>();
            List<Tasks> AllResolvedUserTasks = new List<Tasks>();

            try
            {
                while (true)
                {
                    getResponse = await _client.GetAsync("Users/" + requester + "/closedTasks/" + i);
                    if (getResponse.Body == "null") break;
                    var result = getResponse.ResultAs<oneTask>();
                    AllResolvedUserTasksID.Add(result);
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                foreach (var taskID in AllResolvedUserTasksID)
                {
                    getResponse = await _client.GetAsync("Tasks/" + taskID.id);
                    var result = getResponse.ResultAs<Tasks>();
                    AllResolvedUserTasks.Add(result);
                }
            }
            catch (Exception e)
            {

            }

            wrapPanel1.Children.Clear();

            foreach (var OneTask in AllResolvedUserTasks)
            {
                Border myBorder1 = new Border();
                myBorder1.Background = Brushes.White;
                myBorder1.BorderBrush = Brushes.Black;
                myBorder1.BorderThickness = new Thickness(1);
                myBorder1.Margin = new Thickness(0, 0, 10, 10);

                Label Name = new Label();
                Name.Content = OneTask.name;

                var bc = new BrushConverter();
                Name.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Name.Foreground = new SolidColorBrush(Colors.White);

                Label Owner = new Label();
                Owner.Content = OneTask.owner;
                Owner.Margin = new Thickness(0, 1, 0, 0);
                Owner.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Owner.Foreground = new SolidColorBrush(Colors.White);

                Label Status = new Label();
                Status.Content = OneTask.status;
                Status.Foreground = new SolidColorBrush(Colors.Black);

                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Vertical;

                st.Children.Add(Name);
                st.Children.Add(Owner);
                st.Children.Add(Status);

                myBorder1.Child = st;
                myBorder1.Tag = OneTask.id;
                wrapPanel1.Children.Add(myBorder1);
            }
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

            getCompletedTasks();
            getCountActiveTasks();
            getExpectedTasks();
        }
    }
}
