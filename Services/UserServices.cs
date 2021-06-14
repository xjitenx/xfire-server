using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xfire_server.Models;

namespace xfire_server.Services
{
    public class UserServices
    {        
        public List<User> getUsers()
        {
            List<User> usersList = new List<User>();
            using (var connection = new NpgsqlConnection("Server = 127.0.0.1; Port = 5432; Database = xfire; User Id = postgres; Password = manager;"))
            {
                //use connection here
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    //use command here
                    command.CommandText = "select * from public.tbl_users";
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.UserId = reader.GetGuid(reader.GetOrdinal("user_id"));
                            user.UserAlias = reader.GetString(reader.GetOrdinal("user_alias"));
                            user.UserFirstName = reader.GetString(reader.GetOrdinal("user_first_name"));
                            user.UserLastName = reader.GetString(reader.GetOrdinal("user_last_name"));
                            user.UserEmailId = reader.GetString(reader.GetOrdinal("user_email_id"));
                            user.UserPassword = reader.GetString(reader.GetOrdinal("user_password"));
                            usersList.Add(user);
                        }
                    }
                }
                connection.Close();
            }            
            return usersList;
        }

        public int registerUser(User user)
        {
            int numberOfUpdatedRows = 0;
            try
            {
                using (var connection = new NpgsqlConnection("Server = 127.0.0.1; Port = 5432; Database = xfire; User Id = postgres; Password = manager;"))
                {
                    //use connection here
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        //use command here
                        command.CommandText = "insert into public.tbl_users (user_alias, user_first_name, user_last_name, user_email_id, user_password) values (@userAlias, @userFirstName, @userLastName, @userEmailId, @userPassword)";
                        command.Parameters.AddWithValue("@userAlias", user.UserAlias);
                        command.Parameters.AddWithValue("@userFirstName", user.UserFirstName);
                        command.Parameters.AddWithValue("@userLastName", user.UserLastName);
                        command.Parameters.AddWithValue("@userEmailId", user.UserEmailId);
                        command.Parameters.AddWithValue("@userPassword", user.UserPassword);

                        numberOfUpdatedRows = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(numberOfUpdatedRows);
            return numberOfUpdatedRows;
        }
    }
}
