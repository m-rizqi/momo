using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Momo.data.datasource.database.entity;
using System.Collections.Generic;
using System.Windows;

namespace Momo.data.datasource.database
{
    public class DatabaseService
    {
       // private readonly string connectionString = "Host=172.188.65.129;Port=5432;Username=postgres;Password=postgres;Database=momo";
       // private readonly string connectionString = "Host=pom.db.elephantsql.com;Port=5432;Username=ttgtnlzi;Password=YO8MtrqDwrdzGGws1ibDUnwwsDAHXX56;Database=ttgtnlzi";
       private readonly string connectionString = "Host=192.168.18.36;Port=5432;Username=nathan;Password=190902;Database=momo";

        private readonly NpgsqlConnection _connection;

        public DatabaseService()
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        //#region USER

        public List<UserEntity> GetAllUsers()
        {
            List<UserEntity> users = new();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM \"user\"";
                using NpgsqlCommand command = new(sql, _connection);
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserEntity user = new(id: (int)reader["id"], name: reader["name"].ToString(), email: reader["email"].ToString(), password: reader["password"].ToString());
                    users.Add(user);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }

            return users;
        }


        public UserEntity GetUser(int id)
        {
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM \"user\" WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserEntity user = new(id: (int)reader["id"], name: reader["name"].ToString(), email: reader["email"].ToString(), password: reader["password"].ToString());

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void CreateUser(UserEntity userEntity)
        {
            try
            {
                _connection.Open();

                string sql = "INSERT INTO \"users\" (name, email, password) VALUES (@name, @email, @password)";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("password", NpgsqlDbType.Varchar));

                command.Parameters[0].Value = userEntity.Name;
                command.Parameters[1].Value = userEntity.Email;
                command.Parameters[2].Value = userEntity.Password;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("User created successfully.");
                }
                else
                {
                    string message = "Create user failed.";
                    MessageBox.Show(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
               string errorMessage = e.Message.ToString();
               MessageBox.Show(errorMessage);                
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateUser(UserEntity userEntity)
        {
            try
            {
                _connection.Open();

                string sql = "UPDATE \"user\" SET name = @name, email = @email, password = @password WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("password", NpgsqlDbType.Varchar));

                command.Parameters[0].Value = userEntity.Id;
                command.Parameters[1].Value = userEntity.Name;
                command.Parameters[2].Value = userEntity.Email;
                command.Parameters[3].Value = userEntity.Password;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("User updated successfully.");
                }
                else
                {
                    string message = "Update user failed. User not found or no changes made.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _connection.Open();

                string sql = "DELETE FROM \"user\" WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("User deleted successfully.");
                }
                else
                {
                    string message = "Delete user failed. User not found.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                _connection.Open();

                string sql = "SELECT user_id FROM users WHERE email = @email AND password = @password";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("password", NpgsqlDbType.Varchar));

                command.Parameters[0].Value = email;
                command.Parameters[1].Value = password;

                object result = command.ExecuteScalar();
                string userId = result.ToString();

                if (result != null)
                {                   
                   MessageBox.Show("Login successful.");
                   return userId;                   
                }
                else
                {
                    MessageBox.Show("Invalid email or password");
                    return "";
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);                
            }
            finally
            {
                _connection.Close();
            }
        }

        //#endregion USER

        //#region TIMER

        public List<TimerEntity> GetAllTimers()
        {
            List<TimerEntity> timers = new();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM timer";
                using NpgsqlCommand command = new(sql, _connection);
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TimerEntity timer = new(id: (int)reader["id"], countdown: (long)reader["countdown"]);
                    timers.Add(timer);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }

            return timers;
        }


        public void CreateTimer(TimerEntity timer)
        {
            try
            {
                _connection.Open();

                string sql = "INSERT INTO timer (id, countdown) VALUES (@id, @countdown)";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("countdown", NpgsqlDbType.Bigint));

                command.Parameters[0].Value = timer.Id;
                command.Parameters[1].Value = timer.Countdown;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Timer created successfully.");
                }
                else
                {
                    string message = "Create time failed.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public TimerEntity GetTimer(string id)
        {
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM timer WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters[0].Value = id;

                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new TimerEntity(
                        id: (int)reader["id"],
                        countdown: (long)reader["countdown"]
                    );
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateTimer(TimerEntity timer)
        {
            try
            {
                _connection.Open();

                string sql = "UPDATE timer SET countdown = @countdown WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("countdown", NpgsqlDbType.Bigint));

                command.Parameters[0].Value = timer.Id;
                command.Parameters[1].Value = timer.Countdown;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Timer updated successfully.");
                }
                else
                {
                    string message = "Update timer failed. Timer not found or no changes made.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteTimer(string id)
        {
            try
            {
                _connection.Open();

                string sql = "DELETE FROM timer WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters[0].Value = id;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Timer deleted successfully.");
                }
                else
                {
                    string message = "Delete timer failed. Timer not found.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        //#endregion TIMER

        //#region TASK

        public List<TaskEntity> GetAllTasks(string userId)
        {
            List<TaskEntity> tasks = new();        

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM task WHERE user_id = @userId";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Varchar));
                command.Parameters[0].Value = userId;
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TaskEntity task = new()
                    {
                        UserId = reader["user_id"].ToString(),
                        TaskId = (int)reader["id_task"],
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        IsCompleted = (bool)reader["is_completed"]
                    };
                    tasks.Add(task);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
            }
            finally
            {
                _connection.Close();
            }

            return tasks;
        }

        public void CreateTask(TaskEntity task)
        {
            try
            {
                _connection.Open();

                string sql = "INSERT INTO task (name, description, is_completed, user_id) VALUES (@name, @description, @is_completed, @user_id)";
                using NpgsqlCommand command = new(sql, _connection);
                //command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("is_completed", NpgsqlDbType.Boolean));
                command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Varchar));

                command.Parameters[0].Value = task.Name;
                command.Parameters[1].Value = task.Description;
                command.Parameters[2].Value = task.IsCompleted;
                command.Parameters[3].Value = task.UserId;
                //command.Parameters[3].Value = task.IsCompleted;
                //MessageBox.Show(command.Parameters[2].Value.ToString());

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Task created successfully.");
                }
                else
                {
                    string message = "Create task failed.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public TaskEntity GetTask(string id)
        {
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM task WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters[0].Value = id;

                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new TaskEntity
                    {
                        TaskId = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        IsCompleted = (bool)reader["iscompleted"]
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateTask(TaskEntity task)
        {
            try
            {
                _connection.Open();

                string sql = "UPDATE task SET name = @name, description = @description, iscompleted = @iscompleted WHERE id = @id";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("iscompleted", NpgsqlDbType.Boolean));

                command.Parameters[0].Value = task.TaskId;
                command.Parameters[1].Value = task.Name;
                command.Parameters[2].Value = task.Description;
                command.Parameters[3].Value = task.IsCompleted;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Task updated successfully.");
                }
                else
                {
                    string message = "Update task failed. Task not found or no changes made.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool DeleteAllTask(string userId)
        {
            try
            {
                _connection.Open();

                string sql = "DELETE FROM task WHERE user_id = @userId and is_completed = @isCompleted";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("isCompleted", NpgsqlDbType.Boolean));
                command.Parameters[0].Value = userId;
                command.Parameters[1].Value = true;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Task deleted successfully.");
                    return true;
                }
                else
                {
                    string message = "Delete task failed. Task not found.";
                    Console.WriteLine(message);                    
                    return false;
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);                
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        //#endregion TASK

        //#region PET
        public List<PetEntity> GetAllPets()
        {
            List<PetEntity> pets = new();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM pet";
                using NpgsqlCommand command = new(sql, _connection);
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PetEntity pet = new()
                    {
                        Id = (int)reader["id_pet"],
                        Name = reader["name"].ToString(),
                        Type = reader["type"].ToString(),
                        ImageUrl = reader["image_url"].ToString()
                    };
                    pets.Add(pet);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }

            return pets;
        }

        public void CreatePet(PetEntity pet)
        {
            try
            {
                _connection.Open();

                string sql = "INSERT INTO pet (id_pet, name, type, image_url) VALUES (@id_pet, @name, @type, @image_url)";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_pet", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("type", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("image_url", NpgsqlDbType.Text));

                command.Parameters[0].Value = pet.Id;
                command.Parameters[1].Value = pet.Name;
                command.Parameters[2].Value = pet.Type;
                command.Parameters[3].Value = pet.ImageUrl;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Pet created successfully.");
                }
                else
                {
                    string message = "Create pet failed.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public PetEntity GetPet(int id)
        {
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM pet WHERE id_pet = @id_pet";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_pet", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new PetEntity
                    {
                        Id = (int)reader["id_pet"],
                        Name = reader["name"].ToString(),
                        Type = reader["type"].ToString(),
                        ImageUrl = reader["image_url"].ToString()
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdatePet(PetEntity pet)
        {
            try
            {
                _connection.Open();

                string sql = "UPDATE pet SET name = @name, type = @type, image_url = @image_url WHERE id_pet = @id_pet";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_pet", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("type", NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("image_url", NpgsqlDbType.Text));

                command.Parameters[0].Value = pet.Id;
                command.Parameters[1].Value = pet.Name;
                command.Parameters[2].Value = pet.Type;
                command.Parameters[3].Value = pet.ImageUrl;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Pet updated successfully.");
                }
                else
                {
                    string message = "Update pet failed. Pet not found or no changes made.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeletePet(int id)
        {
            try
            {
                _connection.Open();

                string sql = "DELETE FROM pet WHERE id_pet = @id_pet";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_pet", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Pet deleted successfully.");
                }
                else
                {
                    string message = "Delete pet failed. Pet not found.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        //#region PET

        //#region SESSION

        public List<SessionEntity> GetAllSessions()
        {
            List<SessionEntity> sessions = new();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM session";
                using NpgsqlCommand command = new(sql, _connection);
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SessionEntity session = new()
                    {
                        Id = (int)reader["id_session"],
                        TaskId = (int)reader["id_task"],
                        TimeId = (int)reader["id_time"],
                        UserId = (int)reader["id_user"]
                    };
                    sessions.Add(session);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }

            return sessions;
        }

        public void CreateSession(SessionEntity session)
        {
            try
            {
                _connection.Open();

                string sql = "INSERT INTO session (id_session, id_task, id_time, id_user) VALUES (@id_session, @id_task, @id_time, @id_user)";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_session", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("id_task", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("id_time", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("id_user", NpgsqlDbType.Integer));

                command.Parameters[0].Value = session.Id;
                command.Parameters[1].Value = session.TaskId;
                command.Parameters[2].Value = session.TimeId;
                command.Parameters[3].Value = session.UserId;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Session created successfully.");
                }
                else
                {
                    string message = "Create session failed.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public SessionEntity GetSession(int id)
        {
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM session WHERE id_session = @id_session";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_session", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new SessionEntity
                    {
                        Id = (int)reader["id_session"],
                        TaskId = (int)reader["id_task"],
                        TimeId = (int)reader["id_time"],
                        UserId = (int)reader["id_user"]
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateSession(SessionEntity session)
        {
            try
            {
                _connection.Open();

                string sql = "UPDATE session SET id_task = @id_task, id_time = @id_time, id_user = @id_user WHERE id_session = @id_session";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_session", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("id_task", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("id_time", NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("id_user", NpgsqlDbType.Integer));

                command.Parameters[0].Value = session.Id;
                command.Parameters[1].Value = session.TaskId;
                command.Parameters[2].Value = session.TimeId;
                command.Parameters[3].Value = session.UserId;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Session updated successfully.");
                }
                else
                {
                    string message = "Update session failed. Session not found or no changes made.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteSession(int id)
        {
            try
            {
                _connection.Open();

                string sql = "DELETE FROM session WHERE id_session = @id_session";
                using NpgsqlCommand command = new(sql, _connection);
                command.Parameters.Add(new NpgsqlParameter("id_session", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Session deleted successfully.");
                }
                else
                {
                    string message = "Delete session failed. Session not found.";
                    Console.WriteLine(message);
                    throw new Exception(message);
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString();
                MessageBox.Show(errorMessage);
                throw new Exception(errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        //#endregion SESSION

    }
}
