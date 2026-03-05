using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using wpfdemo0403.classes;
using wpfdemo0403.database;

namespace wpfdemo0403.pages
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

        }

        private void authBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTxt.Text;
            string password = passTxt.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || login == "Логин" || password == "Пароль")
            {
                MessageBox.Show("Заполните поля логина и пароля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                    var dbUser = classes.database.Context.Пользователь.FirstOrDefault(u => u.Логин == login && u.Пароль == password);

                    if (dbUser != null)
                    {
                        switch (dbUser.Роль.ToLower())
                        {
                            case "администратор":
                                AppSession.CurrentUser = new AdminUser();
                                break;
                            case "менеджер":
                                AppSession.CurrentUser = new ManagerUser();
                                break;
                            case "клиент":
                                AppSession.CurrentUser = new ClientUser();
                                break;
                            default:
                                AppSession.CurrentUser = new GuestUser();
                                break;
                        }

                        AppSession.CurrentUser.Id = dbUser.Код;
                        AppSession.CurrentUser.Fio = dbUser.ФИО;
                        AppSession.CurrentUser.Login = dbUser.Логин;

                        MessageBox.Show($"Добро пожаловать, {AppSession.CurrentUser.Fio}!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void guestBtn_Click(object sender, RoutedEventArgs e)
        {
            AppSession.CurrentUser = new GuestUser();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void loginTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginTxt.Text == "Логин") loginTxt.Clear();
        }

        private void passTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passTxt.Text == "Пароль") passTxt.Clear();
        }
    }
}
