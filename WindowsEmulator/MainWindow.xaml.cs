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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WindowsEmulator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();

            // Добавим некоторых тестовых пользователей
            users.Add(new User { Username = "user1", Password = "pass1" });
            users.Add(new User { Username = "user2", Password = "pass2" });
        }
        private string currentInput = "";
        private string currentOperation = "";
        private double result = 0;

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            if (ValidateUser(login, password))
            {
                // Скрыть экран входа и отобразить программу на рабочем столе
                loginScreen.Visibility = Visibility.Collapsed;
                desktop.Visibility = Visibility.Visible;
                programWindow.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
        private void RunProgram_Click(object sender, RoutedEventArgs e)
        {
            // Показать окно программы при нажатии на иконку
            programWindows.Visibility = Visibility.Visible;
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            var createAccountWindow = new CreateAccountWindow(users);
            createAccountWindow.ShowDialog();
        }

        private bool ValidateUser(string username, string password)
        {
            return users.Any(u => u.Username == username && u.Password == password);
        }
        private void CloseProgram_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть окно программы при нажатии на кнопку
            programWindows.Visibility = Visibility.Collapsed;
        }
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentInput += button.Content.ToString();
            UpdateDisplay();
        }

        // Метод для обработки выбора операции
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (!string.IsNullOrEmpty(currentInput))
            {
                if (string.IsNullOrEmpty(currentOperation))
                {
                    result = double.Parse(currentInput);
                }
                else
                {
                    result = PerformOperation(result, double.Parse(currentInput), currentOperation);
                }

                currentOperation = button.Content.ToString();
                currentInput = "";
                UpdateDisplay();
            }
        }

        // Метод для выполнения операции
        private double PerformOperation(double operand1, double operand2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    if (operand2 != 0)
                    {
                        return operand1 / operand2;
                    }
                    else
                    {
                        MessageBox.Show("Cannot divide by zero!");
                        return operand1;
                    }
                default:
                    return operand1;
            }
        }

        // Метод для обновления отображения
        private void UpdateDisplay()
        {
            // Ваш элемент TextBox для отображения результата
            // Например: calcDisplay.Text = currentInput;
        }

        // Метод для очистки калькулятора
        private void ClearCalculator()
        {
            currentInput = "";
            currentOperation = "";
            result = 0;
            UpdateDisplay();
        }

        // Обработчик кнопки закрытия калькулятора
        private void CloseCalculator_Click(object sender, RoutedEventArgs e)
        {
            calculatorWindow.Visibility = Visibility.Collapsed;
            ClearCalculator();
        }

        // Обработчик кнопки запуска калькулятора
        private void RunCalculator_Click(object sender, RoutedEventArgs e)
        {
            calculatorWindow.Visibility = Visibility.Visible;
        }

    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
