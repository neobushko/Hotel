using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Enteties
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public IEnumerable<Record> Records { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                var thatObj = obj as User;
                return this.Id == thatObj.Id
                && this.Name == thatObj.Name;
            }
            else return base.Equals(obj);
        }
    }
}
