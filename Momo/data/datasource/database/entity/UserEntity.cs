using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.datasource.database.entity
{
    class UserEntity
    {
        private int id;
        private String name;
        private String email;
        private String password;

        public int Id { get => id; }
        public string Name { get => name; }
        public string Email { get => email; }
        public string Password { get => password; }

        public UserEntity(int id, string name, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
        }
    }
}
