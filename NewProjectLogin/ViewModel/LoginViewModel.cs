using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using NewProjectLogin.Models;

namespace NewProjectLogin.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        private UserModel newUser;
        private string password;
        private DataBaseLogic _dataBaseLogic;


        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginViewModel()
        {
            _dataBaseLogic = new DataBaseLogic("Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False");
            LoginCommand = new RelayCommand(Login);
            NewUser = new UserModel();
            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser);
            SwitchOn = new RelayCommand(SwitchOnThemes);
        }

        private void SwitchOnThemes()
        {
            if (thisTheme == "Dark")
            {
                thisTheme = "Light";



                ResourceDictionary lightTheme = new ResourceDictionary() { Source = new Uri("View/Themes/Light.xaml", UriKind.Relative) };

                Application.Current.Resources.MergedDictionaries.Add(lightTheme);
            }
            else

            {
                thisTheme = "Dark";

                // Применить темную тему 
                ResourceDictionary darkTheme = new ResourceDictionary() { Source = new Uri("View/Themes/Dark.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(darkTheme);


            }
        }

        private string thisTheme = "Light";


        private void EditUser()
        {
            throw new NotImplementedException();
        }

        private void AddUser()
        {
            throw new NotImplementedException();
        }

        public UserModel NewUser
        {
            get { return newUser; }
            set
            {
                newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }

        public ICommand EditUserCommand { get; private set; }

        public ICommand SwitchOn { get; set; }

        private void Login()
        {
            //HomeMain hm = new HomeMain();
            MainWindow adminMain = new MainWindow();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Close();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Не введены данные!");
                return;
            }

            bool userExists = _dataBaseLogic.IsUserCheck(Username, Password, "Admin") || _dataBaseLogic.IsUserCheck(Username, Password, "User");

            if (userExists)
            {
                // Получаем статус блокировки пользователя
                bool isBlocked = _dataBaseLogic.IsUserBlocked(Username);

                if (isBlocked)
                {
                    MessageBox.Show("Пользователь заблокирован!");
                    return;
                }

                // В зависимости от роли пользователя открываем соответствующее окно
                if (_dataBaseLogic.IsUserCheck(Username, Password, "Admin"))
                {
                    App.Current.Windows[0].Close();
                    adminMain.Show();
                    mainWindow.Close();
                }
                else
                {
                    App.Current.Windows[0].Close();
                    //hm.Show();
                    mainWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Пользователь не обнаружен.");
            }
        }



       

    }
}
