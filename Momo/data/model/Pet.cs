using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.model
{
    class Pet
    {
        String id;
        String name;
        PetType type;
        String imageUrl;

        Pet(string id, string name, PetType type, string imageUrl)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.imageUrl = imageUrl;
        }
    }
}
