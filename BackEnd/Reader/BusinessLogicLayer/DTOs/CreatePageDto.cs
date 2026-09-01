using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public record CreatePageDto(
            Guid ChapterId,
            int Order,
            Stream FileStream,
            string FileName,
            string ContentType
        );
}
