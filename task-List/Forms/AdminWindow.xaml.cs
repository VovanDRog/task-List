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
            public string owner { get; set;  }
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
                myBorder1.Background = Brushes.Gray;
                myBorder1.BorderBrush = Brushes.Black;
                //розмір рамки
                myBorder1.BorderThickness = new Thickness(2);
                //Відступи знизу, справа
                myBorder1.Margin = new Thickness(0, 0, 10, 10);

                Label Name = new Label();
                Name.Content = OneTask.name;
                //Name.FontFamily 
                Name.Foreground = new SolidColorBrush(Colors.Red);

                Label Owner = new Label();
                Owner.Content = OneTask.owner;
                Owner.Foreground = new SolidColorBrush(Colors.Blue);

                Label Status = new Label();
                Status.Content = OneTask.status;
                Status.Foreground = new SolidColorBrush(Colors.Orange);

                //Дозволяє додавати лейбли один під одним
                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Vertical;

                if(OneTask.status != "Zakincheno")
                {
                    // TODO :  id, Name, Opys, ToyHtoVykonye, status
                    st.Children.Add(Name);
                    st.Children.Add(Owner);
                    st.Children.Add(Status);

                    if (OneTask.status == "To Do")
                    {
                        Button bt = new Button();
                        //bt.FontFamily
                        bt.Content = "Добавити до завдань";
                        st.Children.Add(bt);
                    }
                }

                myBorder1.Child = st;
                wrapPanel1.Children.Add(myBorder1);

            }
        }
        public AdminWindow()
        {
            InitializeComponent();

            GetAllTasks();
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

            Name.Text = "";
            Task.Text = "";
            Description.Text = "";
            errordescription.Visibility = Visibility.Hidden;
            errorname.Visibility = Visibility.Hidden;
            errortask.Visibility = Visibility.Hidden;
            this.Width = 822;
            this.Height = 697;

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
