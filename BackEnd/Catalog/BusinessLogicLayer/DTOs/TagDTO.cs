using DatabaseAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class TagDTO : DTO<Tag>
    {
        public string Name { get; set; }

        public TagDTO() : base() { }

        public TagDTO(Guid id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public TagDTO(Tag tag) : base(tag)
        {
            this.Id = tag.Id;
            this.Name = tag.Name;
        }
    }
}
