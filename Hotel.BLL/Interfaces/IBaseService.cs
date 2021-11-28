using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IBaseService : ICheckRooms, ICompareThreeDates
    {
        public BenefitPeriod BenefitForPeriod(DateTime startDate, DateTime endDate);
        public decimal BenefitForRecord(RecordDTO record);
    }
}
