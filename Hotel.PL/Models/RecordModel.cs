using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class RecordModel
    {
        public Guid id { get; set; }
        public Guid RoomId { get; set; }
        public RoomModel Room { get; set; }
        public int RoomNumber { get; set; }
        public string CategoryName { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal Benefit { get; set; }
        public decimal Price { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is RecordModel)
            {
                var thatObj = obj as RecordModel;
                return this.id == thatObj.id
                    && this.RoomId == thatObj.RoomId
                    && this.UserId == thatObj.UserId
                    && this.CheckIn == thatObj.CheckIn
                    && this.CheckOut == thatObj.CheckOut;
            }
            else return base.Equals(obj);
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (CheckIn > CheckOut)
            {
                errors.Add(new ValidationResult("Дата выезда должна быть раньше, нежели дата въезда :)"));
            }
            if (CheckOut < DateTime.Now.Date || CheckIn < DateTime.Now.Date)
            {
                errors.Add(new ValidationResult("Нельзя указывать даты из прошлого"));
            }

            return errors;
        }
    }
}
