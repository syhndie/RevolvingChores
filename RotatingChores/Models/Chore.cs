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
        [Display(Name = "Chore")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Repeat every")]
        public decimal? DaysToRepeat { get; set; }

        [Display(Name = "Last completed")]
        [DataType(DataType.Date)]
        public DateTime? DateLastCompleted { get; set; }

        public Priority? Priority { get; set; }

    }
}
