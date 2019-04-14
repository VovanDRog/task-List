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
    /// Логика взаимодействия для DeleteForm.xaml
    /// </summary>
    public partial class DeleteForm : Window
    {
        public DeleteForm()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length < 4 || Name.Text.Length > 20 )
            {
                NameErrorLabel.Content = "Incorrect name length";
                NameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NameErrorLabel.Visibility = Visibility.Hidden;
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
    }
}
