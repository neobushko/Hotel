﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RegistrationModelDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
