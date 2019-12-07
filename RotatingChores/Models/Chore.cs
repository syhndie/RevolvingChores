using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RotatingChores.Helpers;

namespace RotatingChores.Models
{
    public class Chore
    {
        public int ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateLastCompleted { get; set; }

        public int FrequencyValue { get; set; }

        public TimeIntervals FrequencyUnits { get; set; }
        
        //booleans default to false, and that is what we want here
        public bool IsHighPriority { get; set; }

        public string Notes { get; set; }

        public DateTime DueDate
        {
            get
            {
                switch(FrequencyUnits)
                {
                    case TimeIntervals.days:
                        return DateLastCompleted.AddDays(FrequencyValue);
                    case TimeIntervals.weeks:
                        return DateLastCompleted.AddDays(FrequencyValue * 7);
                    case TimeIntervals.months:
                        return DateLastCompleted.AddMonths(FrequencyValue);
                    case TimeIntervals.years:
                        return DateLastCompleted.AddYears(FrequencyValue);
                    default:
                        throw new ArgumentException("Chore has invalid Frequency Unit");
                }
            }
        }    
    }
}