using System;
using System.Collections.Generic;
using System.Linq;
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
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditForm editForm = new EditForm();
            editForm.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteForm deleteForm = new DeleteForm();
            deleteForm.Show();


        }
    }
}
