using BusinessLogicLayer.DTOs.Interfaces;
using DatabaseAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class PageDTO : IConvertorIntoEntity<Page>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Order { get; set; }  

        public byte[] Image { get; set; }

        public PageDTO() { }

        public PageDTO(Guid id, int order, byte[] image) : this()
        {
            Id = id;
            Order = order;
            Image = image;
        }

        public PageDTO(Page page) : this(page.Id, page.Order, page.Image)
        {}

        public Page ConvertToEntity() => new Page
        {
            Id = this.Id,
            Order = this.Order,
            Image = this.Image
        };
    }
}
