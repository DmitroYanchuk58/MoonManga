using DatabaseAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class PageDTO : DTO<Page>
    {
        public int Order { get; set; }  

        public byte[] Image { get; set; }

        public PageDTO() : base() { }

        public PageDTO(Guid id, int order, byte[] image) : this()
        {
            Id = id;
            Order = order;
            Image = image;
        }

        public PageDTO(Page page) : base(page) 
        {
            this.Id = page.Id;
            this.Image = page.Image;
        }
    }
}
