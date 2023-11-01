using System;
using System.ComponentModel.DataAnnotations;

namespace Momo.data.datasource.database.entity
{
    internal class PetEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public string ImageUrl { get; set; }

        public PetEntity() { }

        public PetEntity(int id, string name, string type, string imageUrl)
        {
            Id = id;
            Name = name;
            Type = type;
            ImageUrl = imageUrl;
        }
    }
}
