using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Momo.data.datasource.database.entity;
using System.Collections.Generic;

namespace Momo.data.datasource.database
{
    internal class DatabaseService
    {
        private string connectionString = "Host=40.76.116.47;Port=5432;Username=postgres;Password=postgres;Database=momo";
        private NpgsqlConnection _connection;

        public DatabaseService()
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        //#region USER

        public List<UserEntity> GetAllUsers()
        {
            List<UserEntity> users = new List<UserEntity>();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM \"user\"";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserEntity user = new UserEntity(
                                id: reader["id"].ToString(),
                                name: reader["name"].ToString(),
                                email: reader["email"].ToString(),
                                password: reader["password"].ToString()
                            );
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                    command.Parameters[0].Value = id;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserEntity user = new UserEntity(
                                id: reader["id"].ToString(),
                                name: reader["name"].ToString(),
                                email: reader["email"].ToString(),
                                password: reader["password"].ToString()
                            );

                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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

                string sql = "INSERT INTO \"user\" (id, name, email, password) VALUES (@id, @name, @email, @password)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
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
                        Console.WriteLine("User created successfully.");
                    }
                    else
                    {
                        string message = "Create user failed.";
                        Console.WriteLine(message);
                        throw new Exception(message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
            List<TimerEntity> timers = new List<TimerEntity>();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM timer";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimerEntity timer = new TimerEntity(
                                id: reader["id"].ToString(),
                                countdown: (long)reader["countdown"]
                            );
                            timers.Add(timer);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                    command.Parameters[0].Value = id;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TimerEntity(
                                id: reader["id"].ToString(),
                                countdown: (long)reader["countdown"]
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                _connection.Close();
            }
        }

        //#endregion TIMER

        //#region TASK

        public List<TaskEntity> GetAllTasks()
        {
            List<TaskEntity> tasks = new List<TaskEntity>();

            try
            {
                _connection.Open();
                string sql = "SELECT * FROM task";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskEntity task = new TaskEntity
                            {
                                Id = reader["id"].ToString(),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                IsCompleted = (bool)reader["iscompleted"]
                            };
                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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

                string sql = "INSERT INTO task (id, name, description, iscompleted) VALUES (@id, @name, @description, @iscompleted)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                    command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                    command.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Varchar));
                    command.Parameters.Add(new NpgsqlParameter("iscompleted", NpgsqlDbType.Boolean));

                    command.Parameters[0].Value = task.Id;
                    command.Parameters[1].Value = task.Name;
                    command.Parameters[2].Value = task.Description;
                    command.Parameters[3].Value = task.IsCompleted;

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                    command.Parameters[0].Value = id;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TaskEntity
                            {
                                Id = reader["id"].ToString(),
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                    command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar));
                    command.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Varchar));
                    command.Parameters.Add(new NpgsqlParameter("iscompleted", NpgsqlDbType.Boolean));

                    command.Parameters[0].Value = task.Id;
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteTask(string id)
        {
            try
            {
                _connection.Open();

                string sql = "DELETE FROM task WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar));
                    command.Parameters[0].Value = id;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Task deleted successfully.");
                    }
                    else
                    {
                        string message = "Delete task failed. Task not found.";
                        Console.WriteLine(message);
                        throw new Exception(message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                _connection.Close();
            }
        }

        //#endregion TASK

    }
}
