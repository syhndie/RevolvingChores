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

        [Required(ErrorMessage = "Name is required")]        
        [Display(Name = "Name of chore")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Day the chore was last done")]
        public DateTime DateLastCompleted { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be positive")]
        [Display(Name = "The chore should be done once every:")]
        public int FrequencyValue { get; set; }

        [Required(ErrorMessage = "Units are required")]
        [Display(Name = "")]
        public TimeIntervals FrequencyUnits { get; set; }

        [Display(Name = "Notes about the chore")]
        public string Notes { get; set; }

        //booleans default to false, and that is what we want here
        [Display(Name = "High priority")]
        public bool IsHighPriority { get; set; }

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