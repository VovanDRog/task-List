﻿using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditForm.xaml
    /// </summary>
    public partial class EditForm : Window
    {
        public EditForm()
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

        private void NewName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewName.Text.Length < 4 || NewName.Text.Length > 20)
            {
                NewNameErrorLabel.Content = "Incorrect new name length";
                NewNameErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                NewNameErrorLabel.Visibility = Visibility.Hidden;
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

        private void Change_Click(object sender, RoutedEventArgs e)
        {

            if (NewName.Text.Length < 4 || NewName.Text.Length > 20)
            {
                NewNameErrorLabel.Content = "Incorrect new name length";
                NewNameErrorLabel.Visibility = Visibility.Visible;
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

        private void Search_Click(object sender, RoutedEventArgs e)
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