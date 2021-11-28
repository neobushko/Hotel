using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface ICompareThreeDates
    {
        public DateTime LowestData(DateTime first, DateTime second, DateTime third);
        public DateTime BiggestData(DateTime first, DateTime second, DateTime third);
        /// <summary>
        /// Checks does the 'other' interval of time(from otherStart to otherEnd) intersects with the 'main' interval of time
        /// </summary>
        /// <param name="mainStart"></param>
        /// <param name="mainEnd"></param>
        /// <param name="otherStart"></param>
        /// <param name="otherEnd"></param>
        /// <returns> true - if 'other' intersects 'main', and vice versa</returns>
        bool InInterval(DateTime mainStart, DateTime mainEnd, DateTime otherStart, DateTime otherEnd);
    }
}
