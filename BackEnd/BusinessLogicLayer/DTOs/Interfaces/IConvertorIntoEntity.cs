using DatabaseAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs.Interfaces
{
    public interface IConvertorIntoEntity<T> where T : Entity
    {
        T ConvertToEntity();
    }
}
