using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class BenefitModel
    {
        [Display(Name = "Benfit for period")]
        public decimal Benefit { get; set; }
        [Display(Name = "start of period")]
        public DateTime StartPeriod { get; set; }
        [Display(Name = "end of period")]
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
