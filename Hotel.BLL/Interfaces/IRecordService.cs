using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IRecordService
    {
        IEnumerable<RecordDTO> GetAll();
        RecordDTO Get(Guid id);
        void Create(RecordDTO item);
        void Update(RecordDTO item);
        void Delete(Guid id);
    }
}
