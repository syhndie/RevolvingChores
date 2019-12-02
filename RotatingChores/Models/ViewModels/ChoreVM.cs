using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RotatingChores.Helpers;

namespace RotatingChores.Models.ViewModels
{
    public class ChoreVM
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? DaysToRepeat { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateLastCompleted { get; set; }

        public Priority? Priority { get; set; }
    }
}
