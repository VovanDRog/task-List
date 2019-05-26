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
using System.IO;

namespace task_List.Forms
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "gUXdi6d0iv4C0HBTO66XbMUQdmQ36idwTjlolpqf",
            BasePath = "https://tasksystem-228.firebaseio.com/"
        };

        IFirebaseClient _client;

        public class Tasks
        {
            public string id { get; set; }
            public string name { get; set; }
            public string owner { get; set; }
            public string requester { get; set; }
            public string status { get; set; }
        }

        async void GetAllTasks()
        {
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

                Label Owner = new Label();
                Owner.Content = OneTask.requester;
                Owner.Margin = new Thickness(0, 1, 0, 0);
                Owner.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                Owner.Foreground = new SolidColorBrush(Colors.White);

                Label Status = new Label();
                Status.Content = OneTask.status;
                Status.Foreground = new SolidColorBrush(Colors.Black);

                //Дозволяє додавати лейбли один під одним
                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Vertical;

                if (OneTask.status != "Zakincheno")
                {
                    // TODO :  id, Name, Opys, ToyHtoVykonye, status
                    st.Children.Add(Name);
                    st.Children.Add(Owner);
                    st.Children.Add(Status);

                    if (OneTask.status == "work")
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
                }

                myBorder1.Child = st;
                wrapPanel1.Children.Add(myBorder1);

            }
        }
        async void GetAllTasksForAccept()
        {
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
                        if (result.status == "Resolved")
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
                Owner.Content = OneTask.requester;
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

                //AcceptTask
                Button AcceptTask = new Button();
                AcceptTask.FontWeight = FontWeights.UltraBold;
                AcceptTask.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                AcceptTask.Foreground = new SolidColorBrush(Colors.White);
                AcceptTask.BorderBrush = new SolidColorBrush(Colors.White);
                AcceptTask.Tag = OneTask.id + " " + OneTask.requester;
                AcceptTask.Click += new RoutedEventHandler(AcceptTask_Click);
                AcceptTask.Content = "Підтвердити";
                //AcceptTask

                //CanceledTask
                Button CanceledTask = new Button();
                CanceledTask.FontWeight = FontWeights.UltraBold;
                CanceledTask.Background = (Brush)bc.ConvertFrom("#FF70B8E8");
                CanceledTask.Foreground = new SolidColorBrush(Colors.White);
                CanceledTask.BorderBrush = new SolidColorBrush(Colors.White);
                CanceledTask.Content = "Скасувати";
                //CanceledTask


                st.Children.Add(AcceptTask);
                st.Children.Add(CanceledTask);

                myBorder1.Child = st;
                wrapPanel1.Children.Add(myBorder1);

            }
        }

        private void AcceptTask_Click(object sender, RoutedEventArgs e)
        {
            BinaryWriter writer = new BinaryWriter(App.getStream());
            BinaryReader reader = new BinaryReader(App.getStream());

            try
            {
                string tag = (string)((Button)sender).Tag;

                string[] subStrings = tag.Split(' ');

                string userId = subStrings[1];
                string taskId = subStrings[0];

                writer.Write(9);
                writer.Flush();
                writer.Write(userId);
                writer.Flush();
                writer.Write(taskId);
                writer.Flush();

                int resultFromServer = (int)reader.Read();
                if (resultFromServer == 1)
                {
                    MessageBox.Show("Завдання закрито");
                    GetAllTasksForAccept();
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

            GetAllTasksForAccept();
        }

        public AdminWindow()
        {
            InitializeComponent();

            GetAllTasksForAccept();
        }


        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length > 0 && errorname.Visibility == Visibility.Hidden && Task.Text.Length > 0 && errortask.Visibility == Visibility.Hidden && Description.Text.Length > 0 && errordescription.Visibility == Visibility.Hidden)
            { //Name.Text = "+++"; 
            }
            //else
            //Name.Text = "---";

        }

        private void add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Name.Visibility = Visibility.Visible;
            Task.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            Description.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Visible;
            Delete.Visibility = Visibility.Hidden;
            Edit.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            scrollViewer.Visibility = Visibility.Hidden;

            Name.Text = "";
            Task.Text = "";
            Description.Text = "";
            errordescription.Visibility = Visibility.Hidden;
            errorname.Visibility = Visibility.Hidden;
            errortask.Visibility = Visibility.Hidden;
            this.Width = 455.818;
            this.Height = 405.682;

        }

        private void edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Name.Visibility = Visibility.Visible;
            Task.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            Description.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;
            Edit.Visibility = Visibility.Visible;
            Search.Visibility = Visibility.Visible;
            Search.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            scrollViewer.Visibility = Visibility.Hidden;

            Name.Text = "";
            Task.Text = "";
            Description.Text = "";
            errordescription.Visibility = Visibility.Hidden;
            errorname.Visibility = Visibility.Hidden;
            errortask.Visibility = Visibility.Hidden;
            this.Width = 455.818;
            this.Height = 405.682;

        }

        private void delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Name.Visibility = Visibility.Visible;
            Task.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            Description.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            scrollViewer.Visibility = Visibility.Hidden;

            Name.Text = "";
            Task.Text = "";
            Description.Text = "";
            errordescription.Visibility = Visibility.Hidden;
            errorname.Visibility = Visibility.Hidden;
            errortask.Visibility = Visibility.Hidden;
            this.Width = 455.818;
            this.Height = 405.682;
        }
        private void viwe_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Task.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            Description.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;
            Edit.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            Name.Visibility = Visibility.Hidden;
            scrollViewer.Visibility = Visibility.Visible;

            Name.Text = "";
            Task.Text = "";
            Description.Text = "";
            errordescription.Visibility = Visibility.Hidden;
            errorname.Visibility = Visibility.Hidden;
            errortask.Visibility = Visibility.Hidden;
            this.Width = 720.818;
            this.Height = 405.682;

        }
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name.Text.Length < 4 || Name.Text.Length > 10)
            {
                errorname.Content = "Incorrect name length";
                errorname.Visibility = Visibility.Visible;
            }
            else
            {
                errorname.Visibility = Visibility.Hidden;
            }

        }

        private void Task_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Task.Text.Length < 4 || Name.Text.Length > 10)
            {
                errortask.Content = "Incorrect task length";
                errortask.Visibility = Visibility.Visible;
            }
            else
            {
                errortask.Visibility = Visibility.Hidden;
            }
        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Description.Text.Length < 4)
            {
                errordescription.Content = "Description is empty";
                errordescription.Visibility = Visibility.Visible;
            }
            else
            {
                errordescription.Visibility = Visibility.Hidden;
            }
        }



        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length < 4 || Name.Text.Length > 10)
            {
                errorname.Content = "Incorrect name length";
                errorname.Visibility = Visibility.Visible;
            }
            else
            {
                errorname.Visibility = Visibility.Hidden;
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length > 0 && errorname.Visibility == Visibility.Hidden)
            {
                Name.Text = "+++";
            }
            else
                Name.Text = "---";

        }

        private void Edit_Click_1(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length > 0 && errorname.Visibility == Visibility.Hidden && Task.Text.Length > 0 && errortask.Visibility == Visibility.Hidden && Description.Text.Length > 0 && errordescription.Visibility == Visibility.Hidden)
            {
                Name.Text = "+++";
            }
            else
                Name.Text = "---";
        }
    }
}
