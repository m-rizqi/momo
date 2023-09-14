using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.model
{
    class Pet
    {
        private String id;
        private String name;
        private PetType type;
        private String imageUrl;

        public string Id { get => id; }
        public string Name { get => name;}
        public PetType Type { get => type; }
        public String ImageUrl { get => imageUrl; }

        public Pet(string id, string name, PetType type, string imageUrl)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.imageUrl = imageUrl;
        }

        
    }
}
