using Momo.data.datasource.database;
using Momo.data.datasource.database.entity;
using System;
using System.Collections.Generic;

namespace Momo.data.repository
{
    public abstract class UserRepository
    {
        public abstract void Create(UserEntity user);
        public abstract UserEntity GetById(int id);
        public abstract List<UserEntity> GetAll();
        public abstract void Update(UserEntity user);
        public abstract void Delete(int id);
    }

    public class UserRepositoryImpl : UserRepository
    {
        private readonly DatabaseService _databaseService;

        public UserRepositoryImpl(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override void Create(UserEntity user)
        {
            _databaseService.CreateUser(user);
        }

        public override UserEntity GetById(int id)
        {
            return _databaseService.GetUser(id);
        }

        public override List<UserEntity> GetAll()
        {
            return _databaseService.GetAllUsers();
        }

        public override void Update(UserEntity user)
        {
            _databaseService.UpdateUser(user);
        }

        public override void Delete(int id)
        {
            _databaseService.UpdateUser(id);
        }
    }
}
