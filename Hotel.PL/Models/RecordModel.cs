using Hotel.PL.RequestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class RecordModel
    {
        [Display(Name = "Record id")]
        public Guid id { get; set; }
        [Display(Name = "Room id")]
        public Guid RoomId { get; set; }
        public RoomModel Room { get; set; }
        [Display(Name = "Room number")]
        public int RoomNumber { get; set; }
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
        [Display(Name = "User id")]
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        [Display(Name = "User contact phone")]
        public string UserPhoneNumber { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Startpont for record")]
        public DateTime CheckIn { get; set; }
        [Display(Name = "Endpoint for record")]
        public DateTime CheckOut { get; set; }
        [Display(Name = "Benefit from record")]
        public decimal Benefit { get; set; }
        [Display(Name = "Price per one night")]
        public decimal Price { get; set; }
        public void CopyTo(RecordRequestModel record)
        {
            record.id = this.id;
            record.RoomId = this.RoomId;
            record.RoomNumber = this.RoomNumber;
            record.CategoryName = this.CategoryName;
            record.UserId = this.UserId;
            record.UserPhoneNumber = this.UserPhoneNumber;
            record.UserName = this.UserName;
            record.Benefit = this.Benefit;
            record.Price = this.Price;
            record.CheckIn = this.CheckIn;
            record.CheckOut = this.CheckOut;
            record.User.id = this.User.id;
            record.User.Name = this.User.Name;
            record.User.PhoneNumber = this.User.PhoneNumber;
            record.User.Email = this.User.Email;
            record.User.Surname = this.User.Surname;
            record.Room.id = this.Room.id;
            record.Room.CategoryId = this.Room.CategoryId;
            record.Room.Description = this.Room.Description;
            record.Room.IsActive = this.Room.IsActive;
            record.Room.Number = this.Room.Number;
            record.Room.Category.id = this.Room.Category.id;
            record.Room.Category.Description = this.Room.Category.Description;
            record.Room.Category.Name = this.Room.Category.Name;
            record.Room.Category.Price = this.Room.Category.Price;
        }
        public void Clone(RecordRequestModel record)
        {
            record.id = this.id;
            record.RoomId = this.RoomId;
            record.RoomNumber = this.RoomNumber;
            record.CategoryName = this.CategoryName;
            record.UserId = this.UserId;
            record.UserPhoneNumber = this.UserPhoneNumber;
            record.UserName = this.UserName;
            record.Benefit = this.Benefit;
            record.Price = this.Price;
            record.CheckIn = this.CheckIn;
            record.CheckOut = this.CheckOut;
            record.User = this.User;
            record.Room = this.Room;
        }
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
