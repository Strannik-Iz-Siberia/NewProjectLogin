using NewProjectLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewProjectLogin.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private UserModel newUser;
        private DataBaseLogic _dataBaseLogic;


        public UserModel NewUser
        {
            get { return newUser; }
            set
            {
                newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }
        public RegisterViewModel()
        {
            _dataBaseLogic = new DataBaseLogic("Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False");
            AddUserCommand = new RelayCommand(AddUser);
            NewUser = new UserModel();

        }

        private void AddUser()
        {
            if (string.IsNullOrEmpty(NewUser.Username) || string.IsNullOrEmpty(NewUser.Name) || string.IsNullOrEmpty(NewUser.Email) || string.IsNullOrEmpty(NewUser.LastName) || string.IsNullOrEmpty(NewUser.Password))
            {
                MessageBox.Show("Не введены данные!");
                return;
            }
            _dataBaseLogic.AddUser(newUser);
        }

        public ICommand AddUserCommand { get; private set; }

    }
}
