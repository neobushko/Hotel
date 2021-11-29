using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class UserDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is UserDTO)
            {
                var thatObj = obj as UserDTO;
                return this.id == thatObj.id
                    && this.Name == thatObj.Name
                    && this.PhoneNumber == thatObj.PhoneNumber
                    && this.Email == thatObj.Email;
            }
            else return base.Equals(obj);
        }
    }
}
