using NewProjectLogin.Models;
using NewProjectLogin.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewProjectLogin.ViewModel
{
    public class EditUserViewModel : ViewModelBase
    {
        private UserModel newUser;
        private DataBaseLogic _dataBaseLogic;

        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users 
        { get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
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

        public EditUserViewModel()
        {
            _dataBaseLogic = new DataBaseLogic("Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False");
            EditUserCommand = new RelayCommand(EditUser);
            LoadData();
            NewUser = new UserModel();
        }

        private void EditUser()
        {
            EditUser ed = new EditUser();
            ed.ShowDialog();
        }



        private void LoadData()
        {
            var dataTable = _dataBaseLogic.LoadDataBase();
            Users = DataTableLoad(dataTable);
        }

        private ObservableCollection<UserModel> DataTableLoad(System.Data.DataTable dataTable)
        {
            var users = new ObservableCollection<UserModel>();
            foreach(System.Data.DataRow row in dataTable.Rows)
            {
                var user = new UserModel
                {
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Name = row["Name"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Blocked = row["Blocked"].ToString()
                };
                users.Add(user);

            }
            return users;
        }

        public ICommand EditUserCommand { get; private set; }






    }
}
