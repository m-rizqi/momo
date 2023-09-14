using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.model
{
    class User
    {
        private String id;
        private String name;
        private String email;
        private String password;

        public string Id { get => id; }
        public string Name { get => name; }
        public string Email { get => email; }
        public string Password { get => password; }

        public User(string id, string name, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
        }

    }
}
