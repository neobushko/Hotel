using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class BenefitPeriod
    {
        public BenefitPeriod()
        {
            StartPeriod = DateTime.MinValue;
            EndPeriod = DateTime.MaxValue;
        }
        public BenefitPeriod(DateTime startPeriod, DateTime endPeriod, IEnumerable<RecordDTO> records)
        {
            this.StartPeriod = startPeriod;
            this.EndPeriod = endPeriod;
            this.Records = records;
        }
        public decimal Benefit { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public IEnumerable<RecordDTO> Records { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is BenefitPeriod)
            {
                var that = obj as BenefitPeriod;
                return this.Benefit == that.Benefit
                    && this.StartPeriod == that.StartPeriod
                    && this.EndPeriod == that.EndPeriod
                    && this.Records.Count() == that.Records.Count();

            }
            else
                return base.Equals(obj);
        }
    }
}
