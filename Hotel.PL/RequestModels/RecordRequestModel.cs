using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.RequestModels
{
    public class RecordRequestModel
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
        public IEnumerable<RoomDTO> rooms  { get; set; }
        public IEnumerable<UserDTO> users { get; set; }
    }
}
