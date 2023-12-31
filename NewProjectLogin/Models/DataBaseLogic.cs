﻿using NewProjectLogin.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewProjectLogin.Models
{
    public class DataBaseLogic
    {
        private string connectionString = "Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False";

        public DataBaseLogic(string connectString)
        {
            this.connectionString = connectString;
        }

        public bool IsUserBlocked(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Blocked FROM [User] WHERE Username = @Username", connection);
                    command.Parameters.AddWithValue("@Username", username);

                    string blockedStatus = (string)command.ExecuteScalar();
                    return blockedStatus == "YES";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
                    return false;
                }
            }
        }
        public bool IsUserCheck(string username, string password, string role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE (Username = @Username AND Password = @Password AND Role = @Role)", connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", role);
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("Пользователь найден!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Пользователь не найден.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
                    return false;
                }
            }
        }

        public void AddUser(UserModel NewUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [User] (Username, Password, Name, LastName, Role, Email, Blocked) VALUES (@Username, @Password, @Name, @LastName, @Role, @Email, @Blocked)", connection);
                {
                    command.Parameters.AddWithValue("@Username", NewUser.Username);
                    command.Parameters.AddWithValue("@Password", NewUser.Password);
                    command.Parameters.AddWithValue("@Name", NewUser.Name);
                    command.Parameters.AddWithValue("@LastName", NewUser.LastName);
                    command.Parameters.AddWithValue("@Role", "User");  // Устанавливаем значение "User" для поля Role
                    command.Parameters.AddWithValue("@Email", NewUser.Email);
                    command.Parameters.AddWithValue("@Blocked", "NO");

                    int rowsbd = command.ExecuteNonQuery();
                    if (rowsbd > 0)
                    {
                        MessageBox.Show("Пользователь успешно добавлен в базу данных.");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить пользователя в базу данных.");
                    }
                }
            }

        }

        public void EditUser(UserModel NewUser)
        {
            App.Current.Windows[0].Close();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE [User] SET Name = @Name, LastName = @LastName, Password = @Password WHERE Username = @Username", connection);
                try
                {
                    command.Parameters.AddWithValue("@Username", NewUser.Username);
                    command.Parameters.AddWithValue("@Name", NewUser.Name);
                    command.Parameters.AddWithValue("@LastName", NewUser.LastName);

                    command.Parameters.AddWithValue("@Password", NewUser.Password);

                    int rowBD = command.ExecuteNonQuery();

                    if (rowBD > 0)
                    {
                        MessageBox.Show("Редактирование завершено");
                    }
                }
                catch
                {
                    MessageBox.Show("Пользователь не изменён");
                }
                connection.Close();
            }
        }

      
        public DataTable LoadDataBase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string load = "SELECT * FROM [User]";

                using (SqlCommand command = new SqlCommand(load, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        public void DeleteUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False"))
            {
                connection.Open();

                string query = $"DELETE FROM [User] WHERE Username = '{user.Username}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        public void BlockedUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False"))
            {
                connection.Open();
                // Создаем SQL-запрос для обновления статуса блокировки пользователя
                string query = $"UPDATE [User] SET Blocked = 'YES' WHERE Username = '{user.Username}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Выполняем запрос
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UnBlocked(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=dbs.mssql.app.biik.ru;Initial Catalog=NewVariantLogDB;Integrated Security=True;Encrypt=False"))
            {
                connection.Open();
                // Создаем SQL-запрос для обновления статуса блокировки пользователя
                string query = $"UPDATE [User] SET Blocked = 'NO' WHERE Username = '{user.Username}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Выполняем запрос
                    command.ExecuteNonQuery();
                }
            }
        }




    }
}
