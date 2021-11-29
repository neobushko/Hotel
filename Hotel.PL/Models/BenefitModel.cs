using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class BenefitModel
    {
        public decimal Benefit { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public IEnumerable<RecordModel> Records { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is BenefitModel)
            {
                var that = obj as BenefitModel;
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
