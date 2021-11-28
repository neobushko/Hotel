﻿using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO Get(Guid id);
        void Create(CategoryDTO item);
        void Update(CategoryDTO item);
        void Delete(Guid id);
        IEnumerable<CategoryDTO> GetAllByPartName(string Name);

    }
}
