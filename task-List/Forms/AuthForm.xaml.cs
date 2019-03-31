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
    /// Interaction logic for AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Login_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Login.Text == String.Empty)
            {
                Log.Content = "";
            }
        }

        private void Login_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Login.Text == String.Empty)
            {
                Log.Content = "Login";
            }
        }

        private void Password1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Password1.Text == String.Empty)
            {
                pass1.Content = "";
            }
        }

        private void Password1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Password1.Text == String.Empty)
            {

                pass1.Content = "Login";
                //перевірка
            }

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Name.Text == String.Empty)
            {
                name_l.Content = "Login";
            }
        }

        private void Name_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Name.Text == String.Empty)
            {
                name_l.Content = "";
            }
        }

        private void Password2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Password2.Text == String.Empty)
            {
                pass2.Content = "";
            }
        }

        private void Password2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Password2.Text == String.Empty)
            {
                pass2.Content = "Login";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
