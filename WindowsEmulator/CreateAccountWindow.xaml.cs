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

namespace WindowsEmulator
{
    /// <summary>
    /// Логика взаимодействия для CreateAccountWindow.xaml
    /// </summary>

        public partial class CreateAccountWindow : Window
        {
            // Структура данных для хранения пользователей
            private List<User> users;

            public CreateAccountWindow(List<User> existingUsers)
            {
                InitializeComponent();

                // Передаем существующих пользователей из MainWindow
                users = existingUsers;
            }

            private void CreateAccount_Click(object sender, RoutedEventArgs e)
            {
                string newUsername = txtNewUsername.Text;
                string newPassword = txtNewPassword.Password;

                // Проверяем, не существует ли пользователь с таким именем
                if (UserExists(newUsername))
                {
                    MessageBox.Show("User with this username already exists.");
                }
                else
                {
                    // Добавляем нового пользователя
                    CreateUser(newUsername, newPassword);
                    MessageBox.Show("Account created successfully!");
                    Close();
                }
            }

            private void CreateUser(string username, string password)
            {
                User newUser = new User { Username = username, Password = password };
                users.Add(newUser);
            }

            private bool UserExists(string username)
            {
                return users.Exists(u => u.Username == username);
            }
        }
    }

