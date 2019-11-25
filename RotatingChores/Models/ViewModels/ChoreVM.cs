using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RotatingChores.Models.ViewModels
{
    public class ChoreVM
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? DaysToRepeat { get; set; }

        public DateTime? DateLastCompleted { get; set; }

        public Priority? Priority { get; set; }
    }
}
