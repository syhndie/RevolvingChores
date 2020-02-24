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
        public string RotatingChoresUserID { get; set; }

        [Required(ErrorMessage = "Name is required")]        
        [Display(Name = "Chore")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last completed")]
        public DateTime? DateLastCompleted { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "This must be a positive integer") ]
        [Display(Name = "Do once every:")]
        public int FrequencyValue { get; set; }

        [Required(ErrorMessage = "Units are required")]
        public TimeIntervals FrequencyUnits { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        //booleans default to false, and that is what we want here
        [Display(Name = "High priority?")]
        public bool IsHighPriority { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate
        {
            get
            {
                DateTime lastDate = DateLastCompleted != null ? (DateTime)DateLastCompleted : DateCreated;
                return FrequencyUnits switch
                {
                    TimeIntervals.days => lastDate.AddDays(FrequencyValue),
                    TimeIntervals.weeks => lastDate.AddDays(FrequencyValue * 7),
                    TimeIntervals.months => lastDate.AddMonths(FrequencyValue),
                    TimeIntervals.years => lastDate.AddYears(FrequencyValue),
                    _ => throw new ArgumentException("Chore has invalid Frequency Unit"),
                };
            }
        }    
    }
}